using System;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using WpfBiomEtec.Models;

namespace WpfBiomEtec.ViewModels
{
    public class WinMAViewModel : BaseViewModel
    {
        private readonly PresencaRepository _presencaRepository;

        // Propriedades para armazenar os dados das presenças
        private int _sala1Presenca;
        public int Sala1Presenca
        {
            get => _sala1Presenca;
            set
            {
                _sala1Presenca = value;
                OnPropertyChanged(nameof(Sala1Presenca));  // Usando o nome da propriedade
            }
        }

        private int _sala2Presenca;
        public int Sala2Presenca
        {
            get => _sala2Presenca;
            set
            {
                _sala2Presenca = value;
                OnPropertyChanged(nameof(Sala2Presenca));  // Usando o nome da propriedade
            }
        }

        private int _sala3Presenca;
        public int Sala3Presenca
        {
            get => _sala3Presenca;
            set
            {
                _sala3Presenca = value;
                OnPropertyChanged(nameof(Sala3Presenca));  // Usando o nome da propriedade
            }
        }

        private int _sala4Presenca;
        public int Sala4Presenca
        {
            get => _sala4Presenca;
            set
            {
                _sala4Presenca = value;
                OnPropertyChanged(nameof(Sala4Presenca));  // Usando o nome da propriedade
            }
        }

        private int _sala5Presenca;
        public int Sala5Presenca
        {
            get => _sala5Presenca;
            set
            {
                _sala5Presenca = value;
                OnPropertyChanged(nameof(Sala5Presenca));  // Usando o nome da propriedade
            }
        }

        private int _sala6Presenca;
        public int Sala6Presenca
        {
            get => _sala6Presenca;
            set
            {
                _sala6Presenca = value;
                OnPropertyChanged(nameof(Sala6Presenca));  // Usando o nome da propriedade
            }
        }

        // Propriedade para os gráficos
        private SeriesCollection _dados;
        public SeriesCollection Dados
        {
            get => _dados;
            set
            {
                _dados = value;
                OnPropertyChanged(nameof(Dados));  // Usando o nome da propriedade
            }
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        // Comando de navegação
        public RelayCommand VoltarCommand { get; }

        // Construtor público sem parâmetros
        public WinMAViewModel() : this(new PresencaRepository())
        {
        }

        // Construtor com injeção do repositório
        public WinMAViewModel(PresencaRepository presencaRepository)
        {
            _presencaRepository = presencaRepository;
            VoltarCommand = new RelayCommand(ExecuteVoltar);

            // Definindo a formatação do label
            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            LoadData();  // Carrega os dados
        }

        /// <summary>
        /// Carrega os dados das presenças e os processa para exibição na interface.
        /// </summary>
        private void LoadData()
        {
            try
            {
                // Busca as presenças por turma
                Sala1Presenca = _presencaRepository.ObterQuantidadePorTurma("1MAA");
                Sala2Presenca = _presencaRepository.ObterQuantidadePorTurma("2MAA");
                Sala3Presenca = _presencaRepository.ObterQuantidadePorTurma("3MAA");
                Sala4Presenca = _presencaRepository.ObterQuantidadePorTurma("1MAB");
                Sala5Presenca = _presencaRepository.ObterQuantidadePorTurma("2MAB");
                Sala6Presenca = _presencaRepository.ObterQuantidadePorTurma("3MAB");

                // Calcula o total de presenças
                int totalPresentes = Sala1Presenca + Sala2Presenca + Sala3Presenca +
                                     Sala4Presenca + Sala5Presenca + Sala6Presenca;

                // Atualiza os dados do gráfico
                Dados = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Presentes",
                        Values = new ChartValues<int> { totalPresentes },
                        DataLabels = true,
                        Fill = System.Windows.Media.Brushes.Green
                    },
                    new PieSeries
                    {
                        Title = "Ausentes",
                        Values = new ChartValues<int> { 240 - totalPresentes },
                        DataLabels = true,
                        Fill = System.Windows.Media.Brushes.Red
                    }
                };

                // Notifica mudanças para atualizar a interface
                OnPropertyChanged(nameof(Sala1Presenca));
                OnPropertyChanged(nameof(Sala2Presenca));
                OnPropertyChanged(nameof(Sala3Presenca));
                OnPropertyChanged(nameof(Sala4Presenca));
                OnPropertyChanged(nameof(Sala5Presenca));
                OnPropertyChanged(nameof(Sala6Presenca));
                OnPropertyChanged(nameof(Dados));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}");
            }
        }

        /// <summary>
        /// Navega de volta para o menu principal.
        /// </summary>
        private void ExecuteVoltar(object obj)
        {
            var winMenu = new WinMenu();
            winMenu.Show();
            Application.Current.Windows[0]?.Close(); // Fecha apenas a janela atual
        }
    }
}
