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
    /// Interação lógica para WinCadResp.xaml
    /// </summary>
    public partial class WinCadResp : Window
    {

        public WinCadResp()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox != null && txtBox.Text == txtBox.Tag.ToString())
            {
                txtBox.Text = string.Empty;
                txtBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox != null && string.IsNullOrWhiteSpace(txtBox.Text))
            {
                txtBox.Text = txtBox.Tag.ToString();
                txtBox.Foreground = Brushes.Gray;
            }
        }


    }
}