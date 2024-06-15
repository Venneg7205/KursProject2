using courseworkkk.Model;
using courseworkkk.View;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace courseworkkk.ViewModel
{
    class TaskProjectViewModel : INotifyPropertyChanged
    {
        [Key]
        private TaskProjectWindow window;
        private TaskProject selectedTaskProject;
        ModelContext db = new ModelContext();
        private string ImageFileName { get; set; }
        public ObservableCollection<Project> ProjectList { get; set; }
        public ObservableCollection<TaskProject> TaskProjectList { get; set; }
        public ObservableCollection<ExecutorProject> ExecutorProjectList { get; set; }
        public TaskProject SelectedTaskProject
        {
            get { return selectedTaskProject; }
            set
            {
                selectedTaskProject = value;
                OnPropertyChanged("SelectedTaskProject");
            }
        }
        public TaskProjectViewModel(TaskProjectWindow window)
        {
            this.window = window;
            TaskProjectList = new ObservableCollection<TaskProject>();
            db.Database.EnsureCreated();
            db.Project.Load();
            db.TaskProject.Load();
            db.ExecutorProject.Load();
            TaskProjectList = db.TaskProject.Local.ToObservableCollection();
            ProjectList = db.Project.Local.ToObservableCollection();
            ExecutorProjectList = db.ExecutorProject.Local.ToObservableCollection();
        }
        private RelayCommand insertCommand;
        public RelayCommand InsertCommand
        {
            get
            {
                return insertCommand ??
                  (insertCommand = new RelayCommand(obj =>
                  {
                      TaskProject taskProject = new TaskProject();
                      taskProject.id_Project = (window.cbProject.SelectedItem as Project).id_Project;
                      taskProject.id_ExecutorProject = (window.cbExecutorProject.SelectedItem as ExecutorProject).id_ExecutorProject;
                      taskProject.NameTaskProject = window.nameTaskProject.Text;
                      taskProject.StateTaskProject = window.stateTaskProject.Text;
                      db.TaskProject.Add(taskProject);
                      db.SaveChanges();
                  }));
            }
        }
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      TaskProject taskProject = obj as TaskProject;
                      TaskProjectList.Remove(taskProject);
                      if (taskProject == null) return;
                      db.TaskProject.Remove(taskProject);
                      db.SaveChanges();
                  }));
            }
        }
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                  (updateCommand = new RelayCommand(obj =>
                  {
                      TaskProject taskProject = selectedTaskProject as TaskProject;
                      if (taskProject == null) return;
                      taskProject.id_Project = (window.cbProject.SelectedItem as Project).id_Project;
                      taskProject.id_ExecutorProject = (window.cbExecutorProject.SelectedItem as ExecutorProject).id_ExecutorProject;
                      taskProject.NameTaskProject = window.nameTaskProject.Text;
                      taskProject.StateTaskProject = window.stateTaskProject.Text;
                      db.Entry(taskProject).State = EntityState.Modified;
                      db.SaveChanges();
                  }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}