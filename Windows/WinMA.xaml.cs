using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using WpfBiomEtec.ViewModels;

namespace WpfBiomEtec.Views
{
    public partial class WinMA : Window
    {
        public WinMA()
        {
            InitializeComponent();
            DataContext = new WinMAViewModel();
        }

        // Manipulador de evento DataClick
        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show($"Você clicou em: {chartPoint.SeriesView.Title}, Valor: {chartPoint.Y}");
        }

        private void ExecuteVoltar(object obj)
        {
            var winMenu = new WinMenu();
            winMenu.Show();
            this.Close(); // Fecha somente a janela atual
        }

    }
}
