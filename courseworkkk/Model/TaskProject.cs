using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace courseworkkk.Model
{
    public class TaskProject : INotifyPropertyChanged
    {
        [Key]
        public int id_TaskProject { get; set; }
        public int id_Project { get; set; }
        public int id_ExecutorProject { get; set; }
        private string nameTaskProject;
        public string NameTaskProject
        {
            get { return nameTaskProject; }
            set
            {
                nameTaskProject = value;
                OnPropertyChanged("NameTaskProject");
            }
        }
        private string stateTaskProject;
        public string StateTaskProject
        {
            get { return stateTaskProject; }
            set
            {
                stateTaskProject = value;
                OnPropertyChanged("StateTaskProject");
            }
        }
        public void Insert()
        {
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO TaskProject (NameTaskProject,StateTaskProject) VALUES ('{NameTaskProject}', '{StateTaskProject}')";
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"Delete from TaskProject where id_TaskProject={id}";
                command.ExecuteNonQuery();
            }
        }
        public void Update(int id)
        {
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE TaskProject SET NameTaskProject='{NameTaskProject}', StateTaskProject='{StateTaskProject}' where id_TaskProject={id}";
                command.ExecuteNonQuery();
            }
        }
        public static ObservableCollection<TaskProject> AllTasksProject()
        {
            ObservableCollection<TaskProject> TasksProject = new ObservableCollection<TaskProject>();
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM TaskProject";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())
                        {
                            TaskProject taskProject = new TaskProject();
                            taskProject.id_Project = reader.GetInt32(1);
                            taskProject.id_ExecutorProject = reader.GetInt32(2);
                            taskProject.NameTaskProject = reader.GetString(3);
                            taskProject.StateTaskProject = reader.GetString(4);
                            TasksProject.Add(taskProject);
                        }
                    }
                }
            }
            return TasksProject;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
