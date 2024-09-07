using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBiomEtec
{
    internal class InsertRespDAO
    {
        public void LimparForm()
        {

        }
        public static void InserirResp(CadastroResp cadResp)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                connection.Open();

                string comandoSQL = "INSERT INTO tab_responsaveis (relacionamentoaluno, nome, biometria, email, cpf, telefone)" +
                    "VALUES (@relacionamentoaluno, @nome, @biometria, @email, @cpf, @telefone)";

                MySqlCommand comandoINSERT = new MySqlCommand(comandoSQL, connection);

                comandoINSERT.Parameters.AddWithValue("@nome", cadResp.Nome);
                comandoINSERT.Parameters.AddWithValue("@biometria", cadResp.IdBiometria);
                comandoINSERT.Parameters.AddWithValue("@email", cadResp.Email);
                comandoINSERT.Parameters.AddWithValue("@cpf", cadResp.CPF);
                comandoINSERT.Parameters.AddWithValue("@telefone", cadResp.Telefone);
                comandoINSERT.Parameters.AddWithValue("@relacionamentoaluno", cadResp.RelacionamentocAluno);

                comandoINSERT.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
