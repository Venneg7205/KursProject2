using courseworkkk.Model;
using courseworkkk.View;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace courseworkkk.ViewModel
{
    [Keyless]
    class ExecutorProjectViewModel : INotifyPropertyChanged
    {
        private ExecutorProjectWindow window;
        private ExecutorProject selectedExecutorProject;
        public ObservableCollection<ExecutorProject> ExecutorsProjectList { get; set; }
        public ExecutorProject SelectedExecutorProject
        {
            get { return selectedExecutorProject; }
            set
            {
                selectedExecutorProject = value;
                OnPropertyChanged("SelectedExecutorProject");
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      ExecutorProject executorProject = new ExecutorProject();
                      executorProject.FirstName = window.FirstName.Text;
                      executorProject.SecondName = window.SecondName.Text;
                      executorProject.LastName = window.LastName.Text;
                      executorProject.DateBirth = window.DateBirth.Text;
                      executorProject.Passport = window.Passport.Text;
                      executorProject.Address = window.Address.Text;
                      executorProject.Insert();
                      ExecutorsProjectList.Add(executorProject);
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
                      ExecutorProject executorProject = obj as ExecutorProject;
                      executorProject.Delete(executorProject.id_ExecutorProject);
                      ExecutorsProjectList.Remove(executorProject);
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
                      ExecutorProject executorProject = obj as ExecutorProject;
                      executorProject.FirstName = window.FirstName.Text;
                      executorProject.SecondName = window.SecondName.Text;
                      executorProject.LastName = window.LastName.Text;
                      executorProject.DateBirth = window.DateBirth.Text;
                      executorProject.Passport = window.Passport.Text;
                      executorProject.Address = window.Address.Text;
                      executorProject.Update(executorProject.id_ExecutorProject);
                  }));
            }
        }
        public ExecutorProjectViewModel(ExecutorProjectWindow window)
        {
            this.window = window;
            ExecutorsProjectList = new ObservableCollection<ExecutorProject>();
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM ExecutorProject";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())
                        {
                            ExecutorProject executorProject = new ExecutorProject();
                            executorProject.FirstName = reader.GetValue(0).ToString();
                            executorProject.SecondName = reader.GetValue(1).ToString();
                            executorProject.LastName = reader.GetValue(2).ToString();
                            executorProject.DateBirth = reader.GetValue(3).ToString();
                            executorProject.Passport = reader.GetValue(4).ToString();
                            executorProject.Address = reader.GetValue(5).ToString();
                            ExecutorsProjectList.Add(executorProject);
                        }
                    }
                }
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
