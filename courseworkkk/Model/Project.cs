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
using System.Xml;

namespace courseworkkk.Model
{
    public class Project : INotifyPropertyChanged
    {
        [Key]
        public int id_Project { get; set; }
        public int id_Client { get; set; }
        public int id_ExecutorProject { get; set; }
        private string nameProject;
        public string NameProject
        {
            get { return nameProject; }
            set
            {
                nameProject = value;
                OnPropertyChanged("NameProject");
            }
        }
        private string priceProject;
        public string PriceProject
        {
            get { return priceProject; }
            set
            {
                priceProject = value;
                OnPropertyChanged("PriceProject");
            }
        }
        private string deadlineProject;
        public string DeadlineProject
        {
            get { return deadlineProject; }
            set
            {
                deadlineProject = value;
                OnPropertyChanged("DeadlineProject");
            }
        }
        private string stateProject;
        public string StateProject
        {
            get { return stateProject; }
            set
            {
                stateProject = value;
                OnPropertyChanged("StateProject");
            }
        }
        private string imageState;
        public string ImageState
        {
            get { return imageState; }
            set
            {
                imageState = value;
                OnPropertyChanged("ImageState");
            }
        }
        public void Delete(int id)
        {
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"Delete from Project where id_Project={id}";
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
                command.CommandText = $"UPDATE Project SET NameProject='{NameProject}', PriceProject='{PriceProject}',DeadlineProject='{DeadlineProject}',StateProject='{StateProject}',ImageState='{ImageState}' where id_Project={id}";
                command.ExecuteNonQuery();
            }
        }
        public void Insert()
        {
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO Project (NameProject,PriceProject,DeadlineProject,StateProject,ImageState) VALUES ('{NameProject}', '{PriceProject}','{DeadlineProject}','{StateProject}','{ImageState}')";
                command.ExecuteNonQuery();
            }
        }
        public static ObservableCollection<Project> AllProjects()
        {
            ObservableCollection<Project> ProjectsList = new ObservableCollection<Project>();
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Project";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())
                        {
                            Project project = new Project();
                            project.id_Project = reader.GetInt32(0);
                            project.id_Client = reader.GetInt32(1);
                            project.id_ExecutorProject = reader.GetInt32(2);
                            project.NameProject = reader.GetString(3);
                            project.PriceProject = reader.GetString(4);
                            project.DeadlineProject = reader.GetString(5);
                            project.StateProject = reader.GetString(6);
                            project.ImageState = reader.GetString(7);
                            ProjectsList.Add(project);
                        }
                    }
                }
            }
            return ProjectsList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
