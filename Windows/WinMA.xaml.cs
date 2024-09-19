using System;
using System.Data;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;

namespace WpfBiomEtec
{
    public partial class WinMA : Window
    {
        public int Sala1Presenca { get; set; }
        public int Sala2Presenca { get; set; }
        public int Sala3Presenca { get; set; }
        public int Sala4Presenca { get; set; }
        public int Sala5Presenca { get; set; }
        public int Sala6Presenca { get; set; }
        public SeriesCollection Dados { get; set; }

        public WinMA()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext uma vez
            LoadDataFromDatabase();
            pieChart();
        }

        private void LoadDataFromDatabase()
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Consultas SQL para contar presenças por turma individualmente
                    string querySala1Presentes = @"SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '1MAA'";
                    string querySala2Presentes = @"SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '2MAA'";
                    string querySala3Presentes = @"SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '3MAA'";
                    string querySala4Presentes = @"SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '1MAB'";
                    string querySala5Presentes = @"SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '2MAB'";
                    string querySala6Presentes = @"SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '3MAB'";

                    MySqlCommand cmdSala1 = new MySqlCommand(querySala1Presentes, connection);
                    MySqlCommand cmdSala2 = new MySqlCommand(querySala2Presentes, connection);
                    MySqlCommand cmdSala3 = new MySqlCommand(querySala3Presentes, connection);
                    MySqlCommand cmdSala4 = new MySqlCommand(querySala1Presentes, connection);
                    MySqlCommand cmdSala5 = new MySqlCommand(querySala2Presentes, connection);
                    MySqlCommand cmdSala6 = new MySqlCommand(querySala3Presentes, connection);

                    // Executar as consultas
                    Sala1Presenca = Convert.ToInt32(cmdSala1.ExecuteScalar());
                    Sala2Presenca = Convert.ToInt32(cmdSala2.ExecuteScalar());
                    Sala3Presenca = Convert.ToInt32(cmdSala3.ExecuteScalar());
                    Sala4Presenca = Convert.ToInt32(cmdSala1.ExecuteScalar());
                    Sala5Presenca = Convert.ToInt32(cmdSala2.ExecuteScalar());
                    Sala6Presenca = Convert.ToInt32(cmdSala3.ExecuteScalar());

                    // Calcula o total de presentes somando os valores das três salas
                    int totalPresentes = Sala1Presenca + Sala2Presenca + Sala3Presenca + Sala4Presenca + Sala5Presenca + Sala6Presenca;

                    // Atualizar o gráfico de doughnut
                    Dados = new SeriesCollection {
                        new PieSeries
                        {
                            Title = "Presentes",
                            Values = new ChartValues<int> { totalPresentes },
                            DataLabels = true,
                            Fill = System.Windows.Media.Brushes.Blue
                        },
                        new PieSeries
                        {
                            Title = "Ausentes",
                            Values = new ChartValues<int> { 240 - totalPresentes }, // Subtrai o total dos alunos presentes, resultando nos ausentes 
                            DataLabels = true,
                            Fill = System.Windows.Media.Brushes.DimGray
                        }
                    };

                    // Atualizar o DataContext para refletir as mudanças
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

        public void pieChart()
        {
            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            // O gráfico de pie já foi configurado no LoadDataFromDatabase
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void PieChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartPoint.ChartView;
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartPoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
