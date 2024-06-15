using courseworkkk.Model;
using courseworkkk.View;
using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Xml;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using XAct.Users;

namespace courseworkkk.ViewModel
{
    class ProjectViewModel : INotifyPropertyChanged
    {
        [Key]
        private ProjectWindow window;
        private Project selectedProject;
        ModelContext db = new ModelContext();
        private string ImageFileName { get; set; }
        public ObservableCollection<Project> ProjectList { get; set; }
        public ObservableCollection<Client> ClientList { get; set; }
        public ObservableCollection<ExecutorProject> ExecutorProjectList { get; set; }
        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }
        public ProjectViewModel(ProjectWindow window)
        {
            this.window = window;
            ProjectList = new ObservableCollection<Project>();
            db.Database.EnsureCreated();
            db.Project.Load();
            db.Client.Load();
            db.ExecutorProject.Load();
            ClientList = db.Client.Local.ToObservableCollection();
            ProjectList = db.Project.Local.ToObservableCollection();
            ExecutorProjectList = db.ExecutorProject.Local.ToObservableCollection();
        }
        private RelayCommand loadCommand;
        public RelayCommand LoadCommand
        {
            get
            {
                return loadCommand ??
                  (loadCommand = new RelayCommand(obj =>
                  {
                      OpenFileDialog ofd = new OpenFileDialog();
                      ofd.Filter = "*.jpg|*.jpg|*.bmp|*.bmp";
                      if (ofd.ShowDialog() == true)
                      {
                          BitmapImage myBitmapImage = new BitmapImage();
                          ImageFileName = ofd.FileName;
                          myBitmapImage.BeginInit();
                          myBitmapImage.UriSource = new Uri(ofd.FileName);
                          myBitmapImage.DecodePixelWidth = 200;
                          myBitmapImage.EndInit();
                          window.StateImage.Source = myBitmapImage;
                      }
                  }));
            }
        }
        private RelayCommand insertCommand;
        public RelayCommand InsertCommand
        {
            get
            {
                return insertCommand ??
                  (insertCommand = new RelayCommand(obj =>
                  {
                      Project project = new Project();
                      project.id_Client = (window.cbClient.SelectedItem as Client).id_Client;
                      project.id_ExecutorProject = (window.cbExecutorProject.SelectedItem as ExecutorProject).id_ExecutorProject;
                      project.NameProject = window.nameProject.Text;
                      project.PriceProject = window.priceProject.Text;
                      project.DeadlineProject = window.deadlineProject.Text;
                      project.StateProject = window.stateProject.Text;
                      project.ImageState = Convert.ToBase64String(File.ReadAllBytes(ImageFileName));
                      db.Project.Add(project);
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
                      Project project = obj as Project;
                      ProjectList.Remove(project);
                      if (project == null) return;
                      db.Project.Remove(project);
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
                      Project project = selectedProject as Project;
                      if (project == null) return;
                      project.NameProject = window.nameProject.Text;
                      project.PriceProject = window.priceProject.Text;
                      project.DeadlineProject = window.deadlineProject.Text;
                      project.StateProject = window.stateProject.Text;
                      project.ImageState = Convert.ToBase64String(File.ReadAllBytes(ImageFileName));
                      db.Entry(project).State = EntityState.Modified;
                      db.SaveChanges();
                  }));
            }
        }

    
        
        //private RelayCommand removeCommand;
        //public RelayCommand RemoveCommand
        //{
        //    get
        //    {
        //        return removeCommand ??
        //          (removeCommand = new RelayCommand(obj =>
        //          {
        //              Project project = obj as Project;
        //              ProjectList.Remove(project);
        //              if (project == null) return;
        //              db.Project.Remove(project);
        //              db.SaveChanges();
        //          }));
        //    }
        //}
        //private RelayCommand updateCommand;
        //public RelayCommand UpdateCommand;
        //public ProjectViewModel(ProjectWindow window)
        //{
        //    this.window = window;
        //    ProjectList = Project.AllProjects();
        //    using (ModelContext db = new ModelContext())
        //    {
        //        db.Database.EnsureCreated();
        //        db.Client.Load();
        //        db.ExecutorProject.Load();
        //        db.Project.Load();
        //        ClientList = db.Client.Local.ToObservableCollection();
        //        ExecutorProjectList = db.ExecutorProject.Local.ToObservableCollection();
        //        ProjectList = db.Project.Local.ToObservableCollection();
        //    }
        //}


        //private RelayCommand addCommand;
        //public RelayCommand AddCommand
        //{
        //    get
        //    {
        //        return addCommand ??
        //          (addCommand = new RelayCommand(obj =>
        //          {
        //              Project project = new Project();
        //              project.id_Client = (window.cbClient.SelectedItem as Client).id_Client;
        //              project.id_ExecutorProject = (window.cbExecutorProject.SelectedItem as ExecutorProject).id_ExecutorProject;
        //              project.NameProject = window.nameProject.Text;
        //              project.PriceProject = window.priceProject.Text;
        //              project.DeadlineProject = window.deadlineProject.Text;
        //              project.StateProject = window.stateProject.Text;
        //              db.Project.Add(project);
        //              db.SaveChanges();
        //          }));
        //    }
        //}
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}