using System.Windows;

namespace WpfBiomEtec
{
    public partial class WinDS : Window
    {
        public WinDS()
        {
            InitializeComponent();
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            WinMenu winMenu = new WinMenu();
            winMenu.Show();
            this.Close();
        }
    }
}
