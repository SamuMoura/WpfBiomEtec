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
        public void LimparForm()
        {
            txtCPF.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtIDbiometria.Clear();
            txtRM.Clear();
            txtTelefone.Clear();
        }
        public WinCadAluno()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            CadastroAluno Alunocadastrar = new CadastroAluno(
                    txtCPF.Text,
                    txtEmail.Text,
                    txtIDbiometria.Text,
                    txtNome.Text,
                    txtTelefone.Text,
                    int.Parse(txtRM.Text)
                );

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
