using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Windows.Input;
using WpfBiomEtec.Models;

namespace WpfBiomEtec.ViewModels
{
    public class WinMNViewModel : INotifyPropertyChanged
    {
        private readonly PresencaRepository _repository;

        // Propriedades para os valores de presença
        public int Sala1Presenca { get; private set; }
        public int Sala2Presenca { get; private set; }
        public int Sala3Presenca { get; private set; }

        // Propriedade para os dados do gráfico de pizza
        public SeriesCollection Dados { get; private set; }

        // Propriedade para formatar os valores nos Gauges
        public Func<double, string> FormatadorUnidades { get; private set; }

        // Comando para o botão "Voltar"
        public ICommand VoltarCommand { get; }

        public WinMNViewModel()
        {
            _repository = new PresencaRepository();
            VoltarCommand = new RelayCommand(Voltar);

            // Formatter para os Gauges
            FormatadorUnidades = value => $"{value:N0}";

            // Carregar os dados ao iniciar
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Obter as presenças de cada turma
                Sala1Presenca = _repository.ObterQuantidadePorTurma("1MN");
                Sala2Presenca = _repository.ObterQuantidadePorTurma("2MN");
                Sala3Presenca = _repository.ObterQuantidadePorTurma("3MN");

                AtualizarGrafico();
                OnPropertyChanged(string.Empty); // Atualiza todas as propriedades
            }
            catch (Exception ex)
            {
                // Log de erro
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
            }
        }

        private void AtualizarGrafico()
        {
            int totalPresentes = Sala1Presenca + Sala2Presenca + Sala3Presenca;
            int totalAlunos = 120; // Total fixo de alunos para o exemplo

            Dados = new SeriesCollection
            {
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
                    Values = new ChartValues<int> { totalAlunos - totalPresentes },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.DimGray
                }
            };
        }

        private void Voltar(object obj)
        {
            var WinMenu = new WinMenu();
            WinMenu.Show();
            App.Current.Windows[0]?.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
