using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WpfBiomEtec.Models;

namespace WpfBiomEtec.DAO
{
    internal class InsertRespDAO
    {
        // Método para inserir responsável
        public static void InserirResp(CadastroResp cadResp)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = @"INSERT INTO responsaveis (relacionamentoaluno, nome, biometria, email, cpf, telefone)
                                      VALUES (@relacionamentoaluno, @nome, @biometria, @email, @cpf, @telefone)";

                using (MySqlCommand comandoINSERT = new MySqlCommand(comandoSQL, connection))
                {
                    comandoINSERT.Parameters.AddWithValue("@relacionamentoaluno", cadResp.RelacionamentoAluno);
                    comandoINSERT.Parameters.AddWithValue("@nome", cadResp.Nome);
                    comandoINSERT.Parameters.AddWithValue("@biometria", cadResp.IdBiometria);
                    comandoINSERT.Parameters.AddWithValue("@email", cadResp.Email);
                    comandoINSERT.Parameters.AddWithValue("@cpf", cadResp.CPF);
                    comandoINSERT.Parameters.AddWithValue("@telefone", cadResp.Telefone);

                    comandoINSERT.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        // Método para listar responsáveis
        public static List<CadastroResp> ListarResponsaveis()
        {
            var responsaveis = new List<CadastroResp>();

            using (var connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = "SELECT * FROM responsaveis";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var resp = new CadastroResp
                        {
                            IdBiometria = reader["biometria"].ToString(),
                            Nome = reader["nome"].ToString(),
                            CPF = reader["cpf"].ToString(),
                            Email = reader["email"].ToString(),
                            Telefone = reader["telefone"].ToString(),
                            RelacionamentoAluno = reader["relacionamentoaluno"].ToString(),
                            RM = reader.IsDBNull(reader.GetOrdinal("rm")) ? (int?)null : Convert.ToInt32(reader["rm"])
                        };
                        responsaveis.Add(resp);
                    }
                }
            }

            return responsaveis;
        }

        // Método para excluir responsável
        public static void ExcluirResp(CadastroResp resp)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = "DELETE FROM responsaveis WHERE biometria = @biometria";

                using (var comandoDelete = new MySqlCommand(comandoSQL, connection))
                {
                    comandoDelete.Parameters.AddWithValue("@biometria", resp.IdBiometria);
                    comandoDelete.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        // Método para atualizar responsável
        public static void AtualizarResp(CadastroResp cadResp)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = @"
                    UPDATE responsaveis
                    SET relacionamentoaluno = @relacionamentoaluno, 
                        nome = @nome, 
                        email = @email, 
                        cpf = @cpf, 
                        telefone = @telefone, 
                        rm = @rm
                    WHERE biometria = @biometria";

                using (var comandoUpdate = new MySqlCommand(comandoSQL, connection))
                {
                    comandoUpdate.Parameters.AddWithValue("@relacionamentoaluno", cadResp.RelacionamentoAluno);
                    comandoUpdate.Parameters.AddWithValue("@nome", cadResp.Nome);
                    comandoUpdate.Parameters.AddWithValue("@biometria", cadResp.IdBiometria);
                    comandoUpdate.Parameters.AddWithValue("@email", cadResp.Email);
                    comandoUpdate.Parameters.AddWithValue("@cpf", cadResp.CPF);
                    comandoUpdate.Parameters.AddWithValue("@telefone", cadResp.Telefone);
                    comandoUpdate.Parameters.AddWithValue("@rm", cadResp.RM.HasValue ? (object)cadResp.RM.Value : DBNull.Value);

                    comandoUpdate.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        // Método para verificar se o responsável já existe no banco
        public static bool ResponsavelExistente(string idBiometria)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = "SELECT COUNT(1) FROM responsaveis WHERE biometria = @biometria";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@biometria", idBiometria);
                    var result = cmd.ExecuteScalar();
                    connection.Close();
                    return Convert.ToInt32(result) > 0;
                }
            }
        }
    }
}
