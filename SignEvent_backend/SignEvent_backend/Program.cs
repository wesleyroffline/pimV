//Cadastro de participantes

using SignEvent.Services;
using SignEvent.SignEvent.Models;
using SignEvent_backend.SignEvent.Services;
using System;

class Program
{
    static ParticipanteService _service = new ParticipanteService();

    static void Main(string[] args)
    {
        int opcao = 0;
        while (opcao != 3)
        {
            Console.Clear();
            Console.WriteLine("=== SIGN EVENT - CADASTRO DE PARTICIPANTES ===");
            Console.WriteLine("1 - Cadastrar Participante");
            Console.WriteLine("2 - Listar Cadastrados");
            Console.WriteLine("3 - Sair");
            Console.Write("Opção: ");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                if (opcao == 1) MenuCadastro();
                else if (opcao == 2) MenuLista();
            }
        }
    }

    static void MenuCadastro()
    {
        Console.Clear();
        // Instancia seu modelo - Data_Inscricao já é definida no construtor[cite: 7]
        Participante p = new Participante();

        Console.WriteLine("--- NOVO CADASTRO ---");
        Console.Write("Nome: ");
        p.Nome = Console.ReadLine();

        Console.Write("E-mail: ");
        p.Email = Console.ReadLine();

        Console.Write("CPF: ");
        p.CPF = Console.ReadLine();

        Console.Write("Telefone: ");
        p.Telefone = Console.ReadLine();

        Console.Write("Tipo (Aluno/Profissional): ");
        p.Tipo = Console.ReadLine();

        Console.Write("Possui deficiência auditiva? (S/N): ");
        p.DeficienteAuditivo = Console.ReadLine().ToUpper() == "S";

        _service.Cadastrar(p);

        Console.WriteLine("\nParticipante cadastrado com sucesso!");
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    static void MenuLista()
    {
        Console.Clear();
        Console.WriteLine("--- LISTA DE PARTICIPANTES ---");
        var lista = _service.ObterTodos();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum registro encontrado.");
        }
        else
        {
            foreach (var p in lista)
            {
                // Utiliza o seu método ToString() sobrescrito[cite: 7]
                Console.WriteLine(p.ToString());
                Console.WriteLine($"   CPF: {p.CPF} | Acessibilidade: {(p.DeficienteAuditivo ? "Sim" : "Não")}");
                Console.WriteLine("------------------------------------------------");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}