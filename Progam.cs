using System;
using AlunosPostgres;

class Program
{
    static void Main()
    {
        var alunoDAO = new AlunoDAO();

        var novoAluno = new Aluno
        {
            Nome = "Kauan Douglas Fonseca da Silva",
            RA = "R107310",
            EmailInstitucional ="kauan.silva97@alunounip.com"
        };

        alunoDAO.CriarAluno(novoAluno);

        Console.WriteLine("Aluno criado com sucesso!");
    }
}