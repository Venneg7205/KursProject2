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
using System.Xml;

namespace courseworkkk.Model
{
    public class Client : INotifyPropertyChanged
    {
        [Key]
        public int id_Client { get; set; }
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
                command.CommandText = $"INSERT INTO Client (FirstName, SecondName,LastName,dateBirth,Passport,Address) VALUES ('{FirstName}', '{SecondName}','{LastName}','{DateBirth}','{Passport}','{Address}')";
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
                command.CommandText = $"Delete from Client where id_Client={id}";
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
                command.CommandText = $"UPDATE Client SET FirstName='{FirstName}', SecondName='{SecondName}',LastName='{LastName}',dateBirth='{DateBirth}'," +
                    $"Passport='{Passport}',Address='{Address}' where id_Client={id}";
                command.ExecuteNonQuery();
            }
        }
        public static ObservableCollection<Client> AllClients()
        {
            ObservableCollection<Client> ClientsList = new ObservableCollection<Client>();
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Client";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())
                        {
                            Client client = new Client();
                            client.id_Client = reader.GetInt32(0);
                            client.FirstName = reader.GetString(1);
                            client.SecondName = reader.GetString(2);
                            client.LastName = reader.GetString(3);
                            client.dateBirth = reader.GetString(4);
                            client.Passport = reader.GetString(5);
                            client.Address = reader.GetString(6);
                            ClientsList.Add(client);
                        }
                    }
                }
            }
            return ClientsList;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
