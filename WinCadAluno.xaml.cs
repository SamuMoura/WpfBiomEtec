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
    /// Lógica interna para WinCadAluno.xaml
    /// </summary>
    public partial class WinCadAluno : Window
    {
        public WinCadAluno()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            CadastroAluno Alunocadastrar = new CadastroAluno(Convert.ToString(txtNumConta.Text), Convert.ToInt32(txtDigconta.Text),
                Convert.ToDouble(txtSaldo.Text), txtSenha.Text, Convert.ToDouble(txtLimite.Text), txtTitutar.Text, txtCPF.Text);

            InsertAlunoDAO.InserirAluno(Alunocadastrar);
            MessageBox.Show("Aluno cadastrado com sucesso!");
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
