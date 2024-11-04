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
    internal class InsertUsuarioDAO
    {
        public static void InserirUsuario(CadastroUsuario cadUsuario)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = @"INSERT INTO usuario (biometria, usuario, senha, permissao)
                                      VALUES (@biometria, @usuario, @senha, @permissao)"
                ;

                using (MySqlCommand comandoINSERT = new MySqlCommand(comandoSQL, connection))
                {
                    comandoINSERT.Parameters.AddWithValue("@biometria", cadUsuario.Biometria);
                    comandoINSERT.Parameters.AddWithValue("@usuario", cadUsuario.Usuario);
                    comandoINSERT.Parameters.AddWithValue("@senha", cadUsuario.Senha);
                    comandoINSERT.Parameters.AddWithValue("@permissao", cadUsuario.Permissao);

                    comandoINSERT.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
    }
}
