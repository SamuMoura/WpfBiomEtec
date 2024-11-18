using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBiomEtec
{
    internal class InsertUsuarioDAO
    {
        public static void InserirUsuario(CadastroUsuario cadUsuario)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                connection.Open();

                string comandoSQL = @"INSERT INTO tab_usuario (usuario, senha, permissao)
                                      VALUES (@usuario, @senha, @permissao)"
                ;

                using (MySqlCommand comandoINSERT = new MySqlCommand(comandoSQL, connection))
                {
                    comandoINSERT.Parameters.AddWithValue("@usuario", cadUsuario.Usuario);
                    comandoINSERT.Parameters.AddWithValue("@senha", cadUsuario.Senha);  // Certifique-se de usar hash para a senha
                    comandoINSERT.Parameters.AddWithValue("@permissao", cadUsuario.Permissao);

                    comandoINSERT.ExecuteNonQuery();
                }
            }
        }
    }
}
