using System;
using System.Windows;

namespace WpfBiomEtec
{
    /// <summary>
    /// Lógica interna para WinCadResp.xaml
    /// </summary>
    public partial class WinCadResp : Window
    {
        public WinCadResp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Limpa os campos do formulário.
        /// </summary>
        public void LimparForm()
        {
            txtCPF.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtIDbiometria.Clear();
            txtRelacionamentocAluno.Clear();
            txtTelefone.Clear();
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validações básicas
                if (string.IsNullOrWhiteSpace(txtCPF.Text) ||
                    string.IsNullOrWhiteSpace(txtNome.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtIDbiometria.Text) ||
                    string.IsNullOrWhiteSpace(txtRelacionamentocAluno.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefone.Text) ||
                    string.IsNullOrWhiteSpace(txtRM.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Convertendo RM para inteiro
                if (!int.TryParse(txtRM.Text, out int rm))
                {
                    MessageBox.Show("RM deve ser um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Criando o objeto CadastroResp
                CadastroResp Respcadastrar = new CadastroResp
                {
                    IdBiometria = txtIDbiometria.Text,
                    Nome = txtNome.Text,
                    CPF = txtCPF.Text,
                    Email = txtEmail.Text,
                    Telefone = txtTelefone.Text,
                    RelacionamentoAluno = txtRelacionamentocAluno.Text,
                    RM = rm
                };

                // Inserindo no banco
                InsertRespDAO.InserirResp(Respcadastrar);

                MessageBox.Show("Responsável cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                LimparForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao cadastrar o responsável: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            WinMenu winMenu = new WinMenu();
            winMenu.Show();
            this.Close();
        }
    }
}
