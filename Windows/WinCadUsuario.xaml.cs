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
    /// Lógica interna para WinCadUsuario.xaml
    /// </summary>
    public partial class WinCadUsuario : Window
    {
        public void LimparForm()
        {
            txtUsuario.Clear();
            psdSenha.Clear();
            txtPermissao.Clear();
            txtBiometria.Clear();
;        }
        public WinCadUsuario()
        {
            InitializeComponent();
        }


        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            CadastroUsuario Usercadastrar = new CadastroUsuario(
                txtUsuario.Text,
                psdSenha.Password,
                txtPermissao.Text,
                txtBiometria.Text
                );

            InsertUsuarioDAO.InserirUsuario(Usercadastrar);
            MessageBox.Show("Responsável cadastrado com sucesso!");
            LimparForm();
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            WinMenu winMenu = new WinMenu();
            winMenu.Show();
            this.Close();
        }
    }
}
