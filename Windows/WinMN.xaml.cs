using System;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;

namespace WpfBiomEtec
{
    public partial class WinMN : Window
    {
        public int Sala1Presenca { get; set; }
        public int Sala2Presenca { get; set; }
        public int Sala3Presenca { get; set; }
        public SeriesCollection Dados { get; set; }

        public WinMN()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para que o Binding funcione
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                try
                {
                    connection.Open();

                    // Consulta SQL para contar presenças em todas as turmas
                    string queryTotalPresentes = @"SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma IN ('1MN', '2MN', '3MN')";

                    // Consulta SQL para contar presenças por turma individualmente
                    string querySala1Presentes = @"SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '1MN'";

                    string querySala2Presentes = @"SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '2MN'";

                    string querySala3Presentes = @"SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '3MN'";

                    MySqlCommand cmdTotal = new MySqlCommand(queryTotalPresentes, connection);
                    MySqlCommand cmdSala1 = new MySqlCommand(querySala1Presentes, connection);
                    MySqlCommand cmdSala2 = new MySqlCommand(querySala2Presentes, connection);
                    MySqlCommand cmdSala3 = new MySqlCommand(querySala3Presentes, connection);

                    // Executar as consultas
                    int totalPresentes = Convert.ToInt32(cmdTotal.ExecuteScalar());
                    int sala1Presentes = Convert.ToInt32(cmdSala1.ExecuteScalar());
                    int sala2Presentes = Convert.ToInt32(cmdSala2.ExecuteScalar());
                    int sala3Presentes = Convert.ToInt32(cmdSala3.ExecuteScalar());

                    // Atualizar o gráfico de doughnut
                    Dados = new SeriesCollection {
                    new PieSeries
                    {
                        Title = "Total de Presentes",
                        Values = new ChartValues<int> { totalPresentes },
                        DataLabels = true,
                        Fill = System.Windows.Media.Brushes.LightGreen
                    },
                    new PieSeries
                    {
                        Title = "Ausentes",
                        Values = new ChartValues<int> { 120 - totalPresentes }, // Subtrai o total dos alunos presentes, resultando nos ausentes 
                        DataLabels = true,
                        Fill = System.Windows.Media.Brushes.OrangeRed
                    }
                };

                    // Atualizar o DataContext para refletir as mudanças
                    Sala1Presenca = sala1Presentes;
                    Sala2Presenca = sala2Presentes;
                    Sala3Presenca = sala3Presentes;

                    DataContext = null;
                    DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}");
            }
        }
    }


        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            WinMenu winMenu = new WinMenu();
            winMenu.Show();
            this.Close();
        }
    }
}
