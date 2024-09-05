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
        public static void InserirAluno(Aluno cadAluno)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                connection.Open();

                string comandoSQL = "INSERT INTO tab_alunos (RM, nome, turma, biometria, IDturma)" +
                    "VALUES ";

                MySqlCommand command = new MySqlCommand(comandoSQL, connection);

                connection.Close();
            }
        }
    }
}
