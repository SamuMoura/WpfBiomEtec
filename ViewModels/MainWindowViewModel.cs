using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using WpfBiomEtec.Models;

namespace WpfBiomEtec.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        public MainWindowViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object obj)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open) 
                    {
                        connection.Open();
                    }

                    if (VerifyUser(Username, Password))
                    {
                        connection.Close();
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            var menu = new WinMenu();
                            menu.Show();
                            Application.Current.Windows[0]?.Close();
                        });
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha incorretos!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}");
                }
            }
        }

        private bool VerifyUser(string username, string password)
        {
            int count;

            using (var connection = ConnectionFactory.GetConnection())
            {
                var query = "SELECT COUNT(*) FROM usuario WHERE usuario = @username AND senha = @password";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return count > 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
