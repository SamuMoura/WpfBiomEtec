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
    /// Lógica interna para WinCadResp.xaml
    /// </summary>
    public partial class WinCadResp : Window
    {
        public void LimparForm()
        {
            txtCPF.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtIDbiometria.Clear();
            txtRelacionamentocAluno.Clear();
            txtTelefone.Clear();
        }
        public WinCadResp()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            CadastroResp Respcadastrar = new CadastroResp(
                txtCPF.Text, 
                txtNome.Text,
                txtEmail.Text, 
                txtNome.Text,
                txtTelefone.Text,
                int.Parse(txtRM.Text),
                txtRelacionamentocAluno.Text
                );

            InsertRespDAO.InserirResp(Respcadastrar);
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
