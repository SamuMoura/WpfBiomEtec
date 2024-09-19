using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace WpfBiomEtec
{
    /// <summary>
    /// Interação lógica para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtAdmin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pwdSenha_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void btnAcesso_Click(object sender, RoutedEventArgs e)
        {
            string userAdm = txtAdmin.Text;
            string userSenha = pwdSenha.Password;

            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }


                    if (VerificarUsuario(userAdm, userSenha))
                    {
                        connection.Close();
                        WinMenu winMenu = new WinMenu();
                        winMenu.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha incorretos!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}");
                    connection.Close();

                }
                connection.Close();

            }


        }

        private bool VerificarUsuario(string userAdm, string userSenha)
        {
            int count;

            using (var connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }


                var query = "SELECT COUNT(*) FROM usuario WHERE usuario = @userAdm AND senha = @userSenha";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userAdm", userAdm);
                    cmd.Parameters.AddWithValue("@userSenha", userSenha);

                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }

                connection.Close();

            }

            return count > 0;
        }
    }
}
