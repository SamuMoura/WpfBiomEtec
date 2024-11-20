using LiveCharts.Wpf;
using LiveCharts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WpfBiomEtec.Models;
using System.Linq;

namespace WpfBiomEtec.ViewModels
{
    public class WinDSViewModel : INotifyPropertyChanged
    {
        private readonly PresencaRepository _repository;

        public ObservableCollection<PieSeries> Dados { get; set; } = new ObservableCollection<PieSeries>();
        public int Sala1Presenca { get; set; }
        public int Sala2Presenca { get; set; }
        public int Sala3Presenca { get; set; }
        public int Sala4Presenca { get; set; }
        public int Sala5Presenca { get; set; }
        public int Sala6Presenca { get; set; }

        public ICommand LoadDataCommand { get; }

        public WinDSViewModel()
        {
            _repository = new PresencaRepository();
            LoadDataCommand = new RelayCommand(LoadData);

            // Carrega os dados ao instanciar a ViewModel
            LoadData(null);
        }

        private void LoadData(object obj)
        {
            var presencas = _repository.ObterPresencas();

            // Atribuir valores às propriedades das salas com base na turma
            Sala1Presenca = presencas.FirstOrDefault(p => p.Turma == "1DSA")?.Quantidade ?? 0;
            Sala2Presenca = presencas.FirstOrDefault(p => p.Turma == "2DSA")?.Quantidade ?? 0;
            Sala3Presenca = presencas.FirstOrDefault(p => p.Turma == "3DSA")?.Quantidade ?? 0;
            Sala4Presenca = presencas.FirstOrDefault(p => p.Turma == "1DSB")?.Quantidade ?? 0;
            Sala5Presenca = presencas.FirstOrDefault(p => p.Turma == "2DSB")?.Quantidade ?? 0;
            Sala6Presenca = presencas.FirstOrDefault(p => p.Turma == "3DSB")?.Quantidade ?? 0;

            // Atualizar o gráfico de pizza
            AtualizarGrafico();
            OnPropertyChanged("");
        }

        private void AtualizarGrafico()
        {
            int totalPresentes = Sala1Presenca + Sala2Presenca + Sala3Presenca + Sala4Presenca + Sala5Presenca + Sala6Presenca;
            int totalAlunos = 240; // Supondo 240 como o total de alunos.

            // Garante que o gráfico seja renderizado mesmo com valores zero
            int presentes = totalPresentes > 0 ? totalPresentes : 1;
            int ausentes = totalAlunos - totalPresentes > 0 ? totalAlunos - totalPresentes : 1;

            Dados.Clear();

            Dados.Add(new PieSeries
            {
                Title = "Presentes",
                Values = new ChartValues<int> { presentes },
                DataLabels = true,
                Fill = System.Windows.Media.Brushes.Blue
            });

            Dados.Add(new PieSeries
            {
                Title = "Ausentes",
                Values = new ChartValues<int> { ausentes },
                DataLabels = true,
                Fill = System.Windows.Media.Brushes.DimGray
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
