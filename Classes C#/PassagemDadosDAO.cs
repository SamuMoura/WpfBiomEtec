using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace WpfBiomEtec.Models
{
    public class PresencaRepository
    {
        public List<Presenca> ObterTodos()
        {
            var lista = new List<Presenca>();
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "SELECT * FROM relatorio_presencas_turma";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Presenca
                        {
                            Id = reader.GetInt32("Id"),
                            Turma = reader.GetString("Turma"),
                            Presencas = reader.GetInt32("Presencas")
                        });
                    }
                }
            }
            return lista;
        }

        public void Atualizar(Presenca presenca)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "UPDATE relatorio_presencas_turma SET Presencas = @Presencas WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Presencas", presenca.Presencas);
                cmd.Parameters.AddWithValue("@Id", presenca.Id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "DELETE FROM relatorio_presencas_turma WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class Presenca
    {
        public int Id { get; set; }
        public string Turma { get; set; }
        public int Presencas { get; set; }
    }
}