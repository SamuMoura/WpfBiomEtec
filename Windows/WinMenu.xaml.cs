using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfBiomEtec
{
    /// <summary>
    /// Lógica interna para WinMenu.xaml
    /// </summary>
    public partial class WinMenu : Window
    {
        public WinMenu()
        {
            InitializeComponent();
        }

        private void btnCadrep_Click(object sender, RoutedEventArgs e)
        {
            WinCadResp winCadResp = new WinCadResp();
            winCadResp.Show();
            this.Close();
        }

        private void btnCadaluno_Click(object sender, RoutedEventArgs e)
        {
            WinCadAluno winCadAluno = new WinCadAluno();
            winCadAluno.Show();
            this.Close();
        }

        private void btnDS_Click(object sender, RoutedEventArgs e)
        {
            WinDS winDS = new WinDS();
            winDS.Show();
            this.Close();
        }

        private void btnMA_Click(object sender, RoutedEventArgs e)
        {
            WinMA winMA = new WinMA();
            winMA.Show();
            this.Close();
        }

        private void btnMN_Click(object sender, RoutedEventArgs e)
        {
            WinMN winMN = new WinMN();
            winMN.Show();
            this.Close();
        }

        public void AddNotification(string message)
        {
            TextBlock notification = new TextBlock
            {
                Text = message,
                Margin = new Thickness(0, 5, 0, 5), // Espaçamento entre notificações
                Background = Brushes.LightGray,     // Cor de fundo para destacar a notificação
                Padding = new Thickness(5)          // Espaçamento interno do texto
            };

            NotificationPanel.Children.Add(notification);
        }
    }
}
