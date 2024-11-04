using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = @"INSERT INTO responsaveis (relacionamentoaluno, nome, biometria, email, cpf, telefone, rm)
                                      VALUES (@relacionamentoaluno, @nome, @biometria, @email, @cpf, @telefone, @RM)";

                using (MySqlCommand comandoINSERT = new MySqlCommand(comandoSQL, connection))
                {
                    comandoINSERT.Parameters.AddWithValue("@relacionamentoaluno", cadResp.RelacionamentocAluno);
                    comandoINSERT.Parameters.AddWithValue("@nome", cadResp.Nome);
                    comandoINSERT.Parameters.AddWithValue("@biometria", cadResp.IdBiometria);
                    comandoINSERT.Parameters.AddWithValue("@email", cadResp.Email);
                    comandoINSERT.Parameters.AddWithValue("@cpf", cadResp.CPF.Trim());
                    comandoINSERT.Parameters.AddWithValue("@telefone", cadResp.Telefone);
                    comandoINSERT.Parameters.AddWithValue("@RM", cadResp.RM);

                    comandoINSERT.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
    }
}
