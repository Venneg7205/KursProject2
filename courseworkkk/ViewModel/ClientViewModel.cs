using courseworkkk.Model;
using courseworkkk.View;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace courseworkkk.ViewModel
{
    [Keyless]
    class ClientViewModel : INotifyPropertyChanged
    {
        private ClientWindow window;
        private Client selectedClient;
        public ObservableCollection<Client> ClientsList { get; set; }
        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
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
                      Client client = new Client();
                      client.FirstName = window.FirstName.Text;
                      client.SecondName = window.SecondName.Text;
                      client.LastName = window.LastName.Text;
                      client.DateBirth = window.DateBirth.Text;
                      client.Passport = window.Passport.Text;
                      client.Address = window.Address.Text;
                      client.Insert();
                      ClientsList.Add(client);
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
                      Client client = obj as Client;
                      client.Delete(client.id_Client);
                      ClientsList.Remove(client);
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
                      Client client = obj as Client;
                      client.FirstName = window.FirstName.Text;
                      client.SecondName = window.SecondName.Text;
                      client.LastName = window.LastName.Text;
                      client.DateBirth = window.DateBirth.Text;
                      client.Passport = window.Passport.Text;
                      client.Address = window.Address.Text;
                      client.Update(client.id_Client);
                  }));
            }
        }
        public ClientViewModel(ClientWindow window)
        {
            this.window = window;
            ClientsList = new ObservableCollection<Client>();
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
                            client.FirstName = reader.GetValue(1).ToString();
                            client.SecondName = reader.GetValue(2).ToString();
                            client.LastName = reader.GetValue(3).ToString();
                            client.DateBirth = reader.GetValue(4).ToString();
                            client.Passport = reader.GetValue(5).ToString();
                            client.Address = reader.GetValue(6).ToString();
                            ClientsList.Add(client);
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