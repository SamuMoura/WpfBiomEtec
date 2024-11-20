using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WpfBiomEtec.Models;

namespace WpfBiomEtec.DAO
{
    internal class InsertUsuarioDAO
    {
        // Método para inserir um usuário
        public static void InserirUsuario(CadastroUsuario cadUsuario)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = @"INSERT INTO usuario (usuario, senha, permissao)
                                      VALUES (@usuario, @senha, @permissao)";

                using (MySqlCommand comandoINSERT = new MySqlCommand(comandoSQL, connection))
                {
                    comandoINSERT.Parameters.AddWithValue("@usuario", cadUsuario.Usuario);
                    comandoINSERT.Parameters.AddWithValue("@senha", cadUsuario.Senha);
                    comandoINSERT.Parameters.AddWithValue("@permissao", cadUsuario.Permissao);

                    comandoINSERT.ExecuteNonQuery();
                }
            }
        }

        // Método para listar todos os usuários do banco de dados
        public static List<CadastroUsuario> ListarUsuarios()
        {
            List<CadastroUsuario> usuarios = new List<CadastroUsuario>();

            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = "SELECT usuario, senha, permissao FROM usuario";

                using (MySqlCommand comandoSELECT = new MySqlCommand(comandoSQL, connection))
                {
                    using (MySqlDataReader reader = comandoSELECT.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string usuario = reader.GetString("usuario");
                            string senha = reader.GetString("senha");
                            string permissao = reader.GetString("permissao");

                            usuarios.Add(new CadastroUsuario(usuario, senha, permissao));
                        }
                    }
                }
            }

            return usuarios;
        }

        // Método para atualizar as informações de um usuário
        public static void AtualizarUsuario(CadastroUsuario usuario)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = @"UPDATE usuario 
                                      SET senha = @senha, permissao = @permissao 
                                      WHERE usuario = @usuario";

                using (MySqlCommand comandoUPDATE = new MySqlCommand(comandoSQL, connection))
                {
                    comandoUPDATE.Parameters.AddWithValue("@usuario", usuario.Usuario);
                    comandoUPDATE.Parameters.AddWithValue("@senha", usuario.Senha); 
                    comandoUPDATE.Parameters.AddWithValue("@permissao", usuario.Permissao);

                    comandoUPDATE.ExecuteNonQuery();
                }
            }
        }

        // Método para excluir um usuário
        public static void ExcluirUsuario(CadastroUsuario usuario)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string comandoSQL = @"DELETE FROM tab_usuario WHERE usuario = @usuario";

                using (MySqlCommand comandoDELETE = new MySqlCommand(comandoSQL, connection))
                {
                    comandoDELETE.Parameters.AddWithValue("@usuario", usuario.Usuario);

                    comandoDELETE.ExecuteNonQuery();
                }
            }
        }
    }
}
