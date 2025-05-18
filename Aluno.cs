using System;
using Npgsql;

namespace AlunosPostgres
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RA { get; set; }
        public string EmailInstitucional { get; set; }
    }

    public class AlunoDAO
    {
        private string connectionString = "Host=;Username=;Password=;Database="; //colocar dados do banco do postgres

        // cadastrar o aluno
        public void CriarAluno(Aluno aluno)
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO alunos (nome, ra, email_institucional) VALUES (@nome, @ra, @email)";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("nome", aluno.Nome);
            cmd.Parameters.AddWithValue("ra", aluno.RA);
            cmd.Parameters.AddWithValue("email", aluno.EmailInstitucional);

            cmd.ExecuteNonQuery();
        }

        // busca o aluno pelo ra
        public Aluno ObterAlunoPorRA(string ra)
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT id, nome, ra, email_institucional FROM alunos WHERE ra = @ra";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("ra", ra);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Aluno
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    RA = reader.GetString(2),
                    EmailInstitucional = reader.GetString(3)
                };
            }

            return null;
        }

        // atualiza o email do aluno
        public void AtualizarEmail(string ra, string novoEmail)
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE alunos SET email_institucional = @email WHERE ra = @ra";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("email", novoEmail);
            cmd.Parameters.AddWithValue("ra", ra);

            cmd.ExecuteNonQuery();
        }
    }
}
