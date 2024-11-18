using System.Collections.Generic;
using System.Windows;

namespace WpfBiomEtec
{
    public partial class WinPassagem : Window
    {
        public List<CadastroResp> ListaCadastro { get; set; }

        public WinPassagem()
        {
            InitializeComponent();

            // Inicializando dados fictícios
            ListaCadastro = new List<CadastroResp>
            {
                new CadastroResp("BIO001", "João Silva", "123.456.789-00", "joao@gmail.com", "11987654321"),
                new CadastroResp("BIO002", "Maria Oliveira", "987.654.321-00", "maria@gmail.com", "11912345678")
            };

            dgCadastroResp.ItemsSource = ListaCadastro;
        }

        private void DgCadastroResp_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgCadastroResp.SelectedItem is CadastroResp selecionado)
            {
                // Preencher os campos de edição
                txtIdBiometria.Text = selecionado.IdBiometria;
                txtNome.Text = selecionado.Nome;
                txtCPF.Text = selecionado.CPF;
                txtEmail.Text = selecionado.Email;
                txtTelefone.Text = selecionado.Telefone;
                txtRelacionamento.Text = selecionado.RelacionamentocAluno;
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (dgCadastroResp.SelectedItem is CadastroResp selecionado)
            {
                // Atualizar objeto selecionado
                selecionado.Nome = txtNome.Text;
                selecionado.CPF = txtCPF.Text;
                selecionado.Email = txtEmail.Text;
                selecionado.Telefone = txtTelefone.Text;
                selecionado.RelacionamentocAluno = txtRelacionamento.Text;

                // Atualizar DataGrid
                dgCadastroResp.Items.Refresh();
                MessageBox.Show("Alterações salvas com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Selecione um registro para salvar as alterações.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnNovoRegistro_Click(object sender, RoutedEventArgs e)
        {
            // Criar novo registro
            var novoCadastro = new CadastroResp
            {
                IdBiometria = "NOVO",
                Nome = txtNome.Text,
                CPF = txtCPF.Text,
                Email = txtEmail.Text,
                Telefone = txtTelefone.Text,
                RelacionamentocAluno = txtRelacionamento.Text
            };

            ListaCadastro.Add(novoCadastro);

            // Atualizar DataGrid
            dgCadastroResp.ItemsSource = null;
            dgCadastroResp.ItemsSource = ListaCadastro;

            MessageBox.Show("Novo registro adicionado!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
