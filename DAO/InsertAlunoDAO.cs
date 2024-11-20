using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WpfBiomEtec.Models;

namespace WpfBiomEtec.DAO
{
    internal class InsertAlunoDAO
    {
        // Método para inserir aluno
        public static void InserirAluno(CadastroAluno cadAluno)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = @"INSERT INTO alunos (RM, nome, biometria, email, cpf, telefone)
                                      VALUES (@RM, @nome, @biometria, @email, @cpf, @telefone)";

                using (MySqlCommand comandoINSERT = new MySqlCommand(comandoSQL, connection))
                {
                    comandoINSERT.Parameters.AddWithValue("@RM", cadAluno.RM);
                    comandoINSERT.Parameters.AddWithValue("@nome", cadAluno.Nome);
                    comandoINSERT.Parameters.AddWithValue("@biometria", cadAluno.IdBiometria);
                    comandoINSERT.Parameters.AddWithValue("@email", cadAluno.Email);
                    comandoINSERT.Parameters.AddWithValue("@cpf", cadAluno.CPF);
                    comandoINSERT.Parameters.AddWithValue("@telefone", cadAluno.Telefone);

                    comandoINSERT.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        // Método para listar alunos
        public static List<CadastroAluno> ListarAlunos()
        {
            List<CadastroAluno> alunos = new List<CadastroAluno>();

            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = "SELECT RM, nome, biometria, email, cpf, telefone FROM alunos";

                using (MySqlCommand comandoSELECT = new MySqlCommand(comandoSQL, connection))
                {
                    using (MySqlDataReader reader = comandoSELECT.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            alunos.Add(new CadastroAluno
                            {
                                RM = reader.GetInt32("RM"),
                                Nome = reader.GetString("nome"),
                                IdBiometria = reader.GetString("biometria"),
                                Email = reader.GetString("email"),
                                CPF = reader.GetString("cpf"),
                                Telefone = reader.GetString("telefone")
                            });
                        }
                    }
                }
            }

            return alunos;
        }

        // Método para excluir aluno
        public static void ExcluirAluno(int rm)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = @"DELETE FROM alunos WHERE RM = @RM";

                using (MySqlCommand comandoDELETE = new MySqlCommand(comandoSQL, connection))
                {
                    comandoDELETE.Parameters.AddWithValue("@RM", rm);
                    comandoDELETE.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        //Método para atualizar o aluno
        public static void AtualizarAluno(CadastroAluno aluno)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = @"UPDATE alunos 
                              SET nome = @nome, cpf = @cpf, biometria = @biometria, 
                                  email = @email, telefone = @telefone 
                              WHERE RM = @RM";

                using (MySqlCommand comandoUPDATE = new MySqlCommand(comandoSQL, connection))
                {
                    comandoUPDATE.Parameters.AddWithValue("@nome", aluno.Nome);
                    comandoUPDATE.Parameters.AddWithValue("@cpf", aluno.CPF);
                    comandoUPDATE.Parameters.AddWithValue("@biometria", aluno.IdBiometria);
                    comandoUPDATE.Parameters.AddWithValue("@email", aluno.Email);
                    comandoUPDATE.Parameters.AddWithValue("@telefone", aluno.Telefone);
                    comandoUPDATE.Parameters.AddWithValue("@RM", aluno.RM);

                    comandoUPDATE.ExecuteNonQuery();
                }
            }
        }

    }
}
