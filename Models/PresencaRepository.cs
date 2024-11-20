using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace WpfBiomEtec.Models
{
    /// <summary>
    /// Classe responsável por acessar os dados de presença no banco de dados.
    /// </summary>
    public class PresencaRepository
    {
        /// <summary>
        /// Obtém a lista de presenças agrupadas por turma.
        /// </summary>
        /// <returns>Lista de objetos Presenca contendo turma e quantidade de presenças.</returns>
        public List<Presenca> ObterPresencas()
        {
            var presencas = new List<Presenca>();

            using (var connection = ConnectionFactory.GetConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    string query = @"
                        SELECT turma, COUNT(*) AS quantidade 
                        FROM relatorio_presencas_turma 
                        GROUP BY turma";

                    using (var cmd = new MySqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            presencas.Add(new Presenca
                            {
                                Turma = reader.GetString("turma"),
                                Quantidade = reader.GetInt32("quantidade")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log ou tratamento de erro
                    Console.Error.WriteLine($"Erro ao acessar o banco: {ex.Message}");
                    throw; // Propaga a exceção para ser tratada na camada superior, se necessário
                }
            }

            return presencas;
        }

        /// <summary>
        /// Obtém a quantidade de presenças de uma turma específica.
        /// </summary>
        /// <param name="turma">Identificação da turma.</param>
        /// <returns>Quantidade de presenças na turma.</returns>
        public int ObterQuantidadePorTurma(string turma)
        {
            if (string.IsNullOrEmpty(turma))
                throw new ArgumentException("A turma não pode ser nula ou vazia.", nameof(turma));

            using (var connection = ConnectionFactory.GetConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    string query = @"
                        SELECT COUNT(*) 
                        FROM relatorio_presencas_turma 
                        WHERE turma = @turma";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@turma", turma);

                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Log ou tratamento de erro
                    Console.Error.WriteLine($"Erro ao acessar o banco: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Obtém o total de presenças registradas no banco de dados.
        /// </summary>
        /// <returns>Total de presenças.</returns>
        public int ObterTotalDePresencas()
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM relatorio_presencas_turma";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Log ou tratamento de erro
                    Console.Error.WriteLine($"Erro ao acessar o banco: {ex.Message}");
                    throw;
                }
            }
        }
    }

    /// <summary>
    /// Classe que representa uma presença agrupada por turma.
    /// </summary>
    public class Presenca
    {
        public string Turma { get; set; }
        public int Quantidade { get; set; }
    }
}
