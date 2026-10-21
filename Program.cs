using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Globalization;


string? readResult; //documentar a razão dessa interrogação
string menuSelection = "";

List<Tarefa> tarefas = new List<Tarefa>
{
    new Tarefa
    {
        Id = 1,
        Titulo = "Estudar C#",
        Descricao = "Terminar modulo 1",
        Status = Tarefa.StatusTarefa.Pendente,
        DataRegistro = DateTime.Now,
        Prazo =  new DateTime(2026, 10, 25)
    },
     new Tarefa
    {
        Id = 2,
        Titulo = "Estudar JavaScript",
        Descricao = "Terminar modulo 2",
        Status = Tarefa.StatusTarefa.Pendente,
        DataRegistro = DateTime.Now,
        Prazo =  new DateTime(2026, 10, 25)
    }
};

do
{
    Console.Clear();
    printMenu();

    readResult = Console.ReadLine() ?? "";

    menuSelection = readResult.ToLower();

    switch (menuSelection)
    {
        case "1":
            Console.Clear();
            int cont = 0;
            foreach (var tarefa in tarefas)
            {
                cont++;
                Console.WriteLine($"Tarefa {tarefa.Id} - {tarefa.Status}");
                Console.WriteLine(tarefa.Titulo);
                Console.WriteLine("Prazo: " + tarefa.Prazo.ToString("dd/MM/yyyy"));
                Console.WriteLine(tarefa.Descricao + "\n");
            }
            Console.WriteLine($"Total de tarefas: {cont}");
            Console.WriteLine("Selecione enter para retornar");
            readResult = Console.ReadLine();
            break;

        case "2":
            Console.WriteLine("Trabalhando nisso...");

            Console.WriteLine("Selecione enter para continuar");
            readResult = Console.ReadLine();
            break;

        case "3":
            string newTitle = "";
            string newDescription = "";
            DateTime newPrazo = DateTime.Now;

            do
            {
                Console.WriteLine("Insira o título: ");
                readResult = Console.ReadLine() ?? "";
                if (readResult != "")
                {
                    newTitle = readResult;
                }

            } while (readResult == "");

            do
            {
                Console.WriteLine("Insira a descrição: ");
                readResult = Console.ReadLine() ?? "";
                if (readResult != "")
                {
                    newDescription = readResult;
                }
            } while (readResult == "");

            do
            {
                string formato = "dd/MM/yyyy";

                Console.WriteLine("Insira o prazo no formato dd/MM/yyyy: ");
                readResult = Console.ReadLine() ?? "";
                if (readResult != "")
                {
                    if (DateTime.TryParseExact(readResult, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime novaData))
                    {
                        newPrazo = novaData;
                    }
                    else readResult = "";
                    Console.Write("Data inválida. ");
                }
            } while (readResult == "");

            Tarefa novaTarefa = new Tarefa
            {
                Id = tarefas.Count + 1,
                Titulo = newTitle,
                Descricao = newDescription,
                DataRegistro = DateTime.Now,
                Prazo = newPrazo
            };

            tarefas.Add(novaTarefa);

            Console.WriteLine("\nTarefa Adicionada!\nSelecione enter para continuar");
            readResult = Console.ReadLine();
            break;

        case "4":
            Console.WriteLine("Trabalhando nisso...");

            Console.WriteLine("Selecione enter para continuar");
            readResult = Console.ReadLine();
            break;

        case "exit":
            break;

        default:
            Console.WriteLine("Digite alguma opção do menu!");
            readResult = Console.ReadLine();

            break;
    }

} while (menuSelection != "exit");

void printMenu()
{
    Console.WriteLine("Gerenciador de tarefas!");
    Console.WriteLine("O que faremos hoje?");

    Console.WriteLine("Escolha:");
    Console.WriteLine("1 - Listar todas as tarefas");
    Console.WriteLine("2 - Editar alguma tarefa ");
    Console.WriteLine("3 - Criar nova tarefa");
    Console.WriteLine("4 - Mudar Status");
    Console.WriteLine("Ou digite exit para sair");


}






