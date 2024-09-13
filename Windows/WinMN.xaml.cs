using System;
using System.Runtime.Remoting.Messaging;
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
        public SeriesCollection Presentes { get; set; }
        public SeriesCollection Ausentes { get; set; }

        public WinMN()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para que o Binding funcione
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            using (MySqlConnection conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Consulta SQL para buscar presentes e ausentes para cada sala
                    string query = @"
                        SELECT 
                            (SELECT COUNT(*) FROM alunos WHERE sala = 1 AND status = 'presente') AS Sala1Presentes,
                            (SELECT COUNT(*) FROM alunos WHERE sala = 1 AND status = 'ausente') AS Sala1Ausentes,
                            (SELECT COUNT(*) FROM alunos WHERE sala = 2 AND status = 'presente') AS Sala2Presentes,
                            (SELECT COUNT(*) FROM alunos WHERE sala = 2 AND status = 'ausente') AS Sala2Ausentes,
                            (SELECT COUNT(*) FROM alunos WHERE sala = 3 AND status = 'presente') AS Sala3Presentes,
                            (SELECT COUNT(*) FROM alunos WHERE sala = 3 AND status = 'ausente') AS Sala3Ausentes";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int sala1Presentes = reader.GetInt32("Sala1Presentes");
                        int sala2Presentes = reader.GetInt32("Sala2Presentes");
                        int sala3Presentes = reader.GetInt32("Sala3Presentes");

                        UpdateChart(sala1Presentes, sala2Presentes, sala3Presentes);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}");
                }
            }
        }

        private void UpdateChart(int sala1Presentes, int sala2Presentes, int sala3Presentes)
        {
            // Supondo que o valor máximo para o gauge de presença seja 40
            int maxPresenca = 40;

            // Se o valor de presentes for maior que 40, limite o valor
            Sala1Presenca = sala1Presentes > maxPresenca ? maxPresenca : sala1Presentes;
            Sala2Presenca = sala2Presentes > maxPresenca ? maxPresenca : sala2Presentes;
            Sala3Presenca = sala3Presentes > maxPresenca ? maxPresenca : sala3Presentes;

            // Atualizar o gráfico de doughnut
            Presentes = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Sala 1 Presentes",
                    Values = new ChartValues<int> { sala1Presentes },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Sala 2 Presentes",
                    Values = new ChartValues<int> { sala2Presentes },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Sala 3 Presentes",
                    Values = new ChartValues<int> { sala3Presentes },
                    DataLabels = true
                }
            };

            Ausentes = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Sala 1 Ausentes",
                    Values = new ChartValues<int> { reader.GetInt32("Sala1Ausentes") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Sala 2 Ausentes",
                    Values = new ChartValues<int> { reader.GetInt32("Sala2Ausentes") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Sala 3 Ausentes",
                    Values = new ChartValues<int> { reader.GetInt32("Sala3Ausentes") },
                    DataLabels = true
                }
            };

            // Atualizar o DataContext para refletir as mudanças
            DataContext = null;
            DataContext = this;
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            WinMenu winMenu = new WinMenu();
            winMenu.Show();
            this.Close();
        }
    }
}
