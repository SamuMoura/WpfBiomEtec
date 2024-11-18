using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;
using System;

namespace WpfBiomEtec
{
    public partial class WinPassagem : Window
    {
        public List<CadastroResp> ListaCadastro { get; set; }

        public WinPassagem()
        {
            InitializeComponent();
            ListaCadastro = CarregarDadosDoBanco();
            dgCadastroResp.ItemsSource = ListaCadastro;
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            WinMenu winMenu = new WinMenu();
            winMenu.Show();
            this.Close();
        }

        private void DgCadastroResp_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgCadastroResp.SelectedItem is CadastroResp selecionado)
            {
                txtCPF.Text = selecionado.CPF;
                txtNome.Text = selecionado.Nome;
                txtEmail.Text = selecionado.Email;
                txtTelefone.Text = selecionado.Telefone;
                txtBiometria.Text = selecionado.Biometria;
                txtRelacionamento.Text = selecionado.RelacionamentoAluno;
                txtRM.Text = selecionado.RM?.ToString() ?? string.Empty;
            }
        }

        private List<CadastroResp> CarregarDadosDoBanco()
        {
            var lista = new List<CadastroResp>();
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var query = "SELECT * FROM responsaveis";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CadastroResp
                        {
                            CPF = reader["CPF"].ToString(),
                            Nome = reader["nome"].ToString(),
                            Email = reader["email"].ToString(),
                            Telefone = reader["telefone"].ToString(),
                            Biometria = reader["biometria"].ToString(),
                            RelacionamentoAluno = reader["relacionamentoaluno"].ToString(),
                            RM = reader["RM"] != DBNull.Value ? (int?)reader["RM"] : null
                        });
                    }
                }
            }
            return lista;
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (dgCadastroResp.SelectedItem is CadastroResp selecionado)
            {
                selecionado.Nome = txtNome.Text;
                selecionado.Email = txtEmail.Text;
                selecionado.Telefone = txtTelefone.Text;
                selecionado.Biometria = txtBiometria.Text;
                selecionado.RelacionamentoAluno = txtRelacionamento.Text;
                selecionado.RM = int.TryParse(txtRM.Text, out int rm) ? rm : (int?)null;

                AtualizarNoBanco(selecionado);
                dgCadastroResp.Items.Refresh();
                MessageBox.Show("Alterações salvas!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AtualizarNoBanco(CadastroResp cadastro)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var query = "UPDATE responsaveis SET nome=@Nome, email=@Email, telefone=@Telefone, biometria=@Biometria, " +
                            "relacionamentoaluno=@Relacionamento, RM=@RM WHERE CPF=@CPF";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CPF", cadastro.CPF);
                    command.Parameters.AddWithValue("@Nome", cadastro.Nome);
                    command.Parameters.AddWithValue("@Email", cadastro.Email);
                    command.Parameters.AddWithValue("@Telefone", cadastro.Telefone);
                    command.Parameters.AddWithValue("@Biometria", cadastro.Biometria);
                    command.Parameters.AddWithValue("@Relacionamento", cadastro.RelacionamentoAluno);
                    command.Parameters.AddWithValue("@RM", cadastro.RM);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void BtnExcluirRegistro_Click(object sender, RoutedEventArgs e)
        {
            if (dgCadastroResp.SelectedItem is CadastroResp selecionado)
            {
                var result = MessageBox.Show($"Confirma exclusão de {selecionado.Nome}?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    ListaCadastro.Remove(selecionado);
                    ExcluirDoBanco(selecionado.CPF);
                    dgCadastroResp.Items.Refresh();
                    MessageBox.Show("Registro excluído!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void ExcluirDoBanco(string cpf)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var query = "DELETE FROM responsaveis WHERE CPF=@CPF";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CPF", cpf);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void BtnNovoRegistro_Click(object sender, RoutedEventArgs e)
        {
            var novoCadastro = new CadastroResp
            {
                CPF = txtCPF.Text,
                Nome = txtNome.Text,
                Email = txtEmail.Text,
                Telefone = txtTelefone.Text,
                Biometria = txtBiometria.Text,
                RelacionamentoAluno = txtRelacionamento.Text,
                RM = int.TryParse(txtRM.Text, out int rm) ? rm : (int?)null
            };

            ListaCadastro.Add(novoCadastro);
            AdicionarAoBanco(novoCadastro);
            dgCadastroResp.Items.Refresh();
            MessageBox.Show("Novo registro adicionado!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AdicionarAoBanco(CadastroResp cadastro)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var query = "INSERT INTO responsaveis (CPF, nome, email, telefone, biometria, relacionamentoaluno, RM) " +
                            "VALUES (@CPF, @Nome, @Email, @Telefone, @Biometria, @Relacionamento, @RM)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CPF", cadastro.CPF);
                    command.Parameters.AddWithValue("@Nome", cadastro.Nome);
                    command.Parameters.AddWithValue("@Email", cadastro.Email);
                    command.Parameters.AddWithValue("@Telefone", cadastro.Telefone);
                    command.Parameters.AddWithValue("@Biometria", cadastro.Biometria);
                    command.Parameters.AddWithValue("@Relacionamento", cadastro.RelacionamentoAluno);
                    command.Parameters.AddWithValue("@RM", cadastro.RM);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
