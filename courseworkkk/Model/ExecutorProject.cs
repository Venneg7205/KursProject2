using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace courseworkkk.Model
{
    public class ExecutorProject : INotifyPropertyChanged
    {
        [Key]
        public int id_ExecutorProject { get; set; }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        private string secondName;
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        private string passport;
        public string Passport
        {
            get { return passport; }
            set
            {
                passport = value;
                OnPropertyChanged("Passport");
            }
        }
        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        private string dateBirth;
        public string DateBirth
        {
            get { return dateBirth; }
            set
            {
                dateBirth = value;
                OnPropertyChanged("DateBirth");
            }
        }
        public void Insert()
        {
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO ExecutorProject (FirstName, SecondName,LastName,dateBirth,Passport,Address) VALUES ('{FirstName}', '{SecondName}','{LastName}','{DateBirth}','{Passport}','{Address}')";
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
                command.CommandText = $"Delete from ExecutorProject where id_ExecutorProject={id}";
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
                command.CommandText = $"UPDATE ExecutorProject SET FirstName='{FirstName}', SecondName='{SecondName}',LastName='{LastName}',dateBirth='{DateBirth}'," +
                    $"Passport='{Passport}',Address='{Address}' where id_ExecutorProject={id}";
                command.ExecuteNonQuery();
            }
        }
        public static ObservableCollection<ExecutorProject> AllExecutorProject()
        {
            ObservableCollection<ExecutorProject> ExecutorsProjectList = new ObservableCollection<ExecutorProject>();
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
            return ExecutorsProjectList;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

