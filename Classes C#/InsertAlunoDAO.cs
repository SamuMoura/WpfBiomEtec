using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBiomEtec
{
    internal class InsertAlunoDAO
    {
        public static void InserirAluno(CadastroAluno cadAluno)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                connection.Open();

                string comandoSQL = "INSERT INTO tab_alunos (RM, nome, biometria, email, cpf, telefone)" +
                    "VALUES (@RM, @nome, @biometria, @email, @cpf, @telefone)";

                MySqlCommand comandoINSERT = new MySqlCommand(comandoSQL, connection);

                comandoINSERT.Parameters.AddWithValue("@nome", cadAluno.Nome);
                comandoINSERT.Parameters.AddWithValue("@biometria", cadAluno.IdBiometria);
                comandoINSERT.Parameters.AddWithValue("@email", cadAluno.Email);
                comandoINSERT.Parameters.AddWithValue("@cpf", cadAluno.CPF);
                comandoINSERT.Parameters.AddWithValue("@telefone", cadAluno.Telefone);
                comandoINSERT.Parameters.AddWithValue("@RM", cadAluno.RM);

                comandoINSERT.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
