using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Globalization;
using System.IO.Pipelines;



string? readResult; //documentar a razão dessa interrogação
string menuSelection = "";

List<Tarefa> tarefas = new List<Tarefa> { };

bool hasJson = RepositorioTarefa.procuraLista();

if (hasJson)
{
    try
    {

        tarefas = RepositorioTarefa.lerTarefas();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.Write("Selecione enter para fechar");
        readResult = Console.ReadLine();
        return;
    }
}
else
{
    RepositorioTarefa.criarLista();
}

do
{
    Console.CursorVisible = false;
    Console.Clear();

    alerta();


    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Lista de tarefas!");
        Console.WriteLine("Selecione o que deseja fazer: ");
        Console.WriteLine();
        (int consoleX, int consoleY) = Console.GetCursorPosition();

        string[] selecao = { "Listar todas as tarefas", "Editar alguma tarefa ", "Criar nova tarefa", " Mudar Status", "Sair" };
        menuSelection = alternativas(selecao, x: consoleX, y: consoleY);
    }



    //readResult = Console.ReadLine() ?? "";
    //menuSelection = readResult.ToLower();

    switch (menuSelection)
    {
        case "1": //listar
            Console.Clear();
            int cont = 0;
            foreach (var tarefa in tarefas)
            {
                cont++;
                string texto = (tarefa.DataConclusao.HasValue == true) ? $"Concluida em {tarefa.DataConclusao.Value.ToString("dd/MM/yyyy")}" : $"{tarefa.Status}";

                Console.WriteLine($"Tarefa {tarefa.Id} - {texto}");
                Console.WriteLine($"Prazo: {tarefa.Prazo.ToString("dd/MM/yyyy")}");
                Console.WriteLine(tarefa.Titulo);
                Console.WriteLine(tarefa.Descricao + "\n");

            }
            Console.WriteLine($"Total de tarefas: {cont}");
            Console.WriteLine("Selecione enter para retornar");
            readResult = Console.ReadLine();
            break;

        case "2": //editar
            {
                int buscaId = 0; string buscaNome = "x";
                bool find = false;
                string escolha = "";

                Console.CursorVisible = true;
                Console.Clear();
                Console.WriteLine("Editar uma tarefa -");

                do
                {

                    Console.WriteLine("Digite o Id ou Titulo da tarefa: ");
                    readResult = Console.ReadLine() ?? "";
                    if (readResult != null)
                    {
                        if (int.TryParse(readResult, out int idValue))
                        {
                            buscaId = idValue;
                        }
                        else buscaNome = readResult;
                    }
                } while (readResult == "");

                foreach (var tarefa in tarefas)
                {
                    if (tarefa.Titulo == buscaNome || tarefa.Id == buscaId)
                    {

                        //Console.WriteLine("O que deseja editar?");
                        do
                        {
                            Console.CursorVisible = false;
                            Console.Clear();
                            Console.WriteLine($"Tarefa {tarefa.Id} - {tarefa.Status}");
                            Console.WriteLine(tarefa.Titulo);
                            Console.WriteLine("Prazo: " + tarefa.Prazo.ToString("dd/MM/yyyy"));
                            Console.WriteLine(tarefa.Descricao + "\n");
                            find = true;
                            Console.WriteLine("O que deseja editar?");

                            (int consoleX, int consoleY) = Console.GetCursorPosition();
                            string[] selecao2 = { "Titulo", "Prazo", "Descricao", " Nenhum" };
                            escolha = alternativas(selecao2, x: consoleX, y: consoleY);

                            Console.CursorVisible = true;

                            switch (escolha)
                            {
                                case "1":
                                    do
                                    {
                                        Console.WriteLine("Digite o novo titulo: ");
                                        readResult = Console.ReadLine() ?? "";
                                        if (readResult != "")
                                        {
                                            tarefa.Titulo = readResult;
                                        }
                                    } while (readResult == "");
                                    Console.WriteLine("Titulo alterao com sucesso!");
                                    RepositorioTarefa.gravarTarefas(tarefas);
                                    Console.WriteLine("Selecione enter para continuar");
                                    readResult = Console.ReadLine();
                                    Console.Clear();
                                    break;
                                case "2":
                                    do
                                    {
                                        string formato = "dd/MM/yyyy";
                                        Console.WriteLine("Digite o novo prazo: ");
                                        readResult = Console.ReadLine() ?? "";
                                        if (DateTime.TryParseExact(readResult, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime novaData))
                                        {
                                            tarefa.Prazo = novaData;
                                            Console.WriteLine("Prazo alterado com sucesso!");
                                            RepositorioTarefa.gravarTarefas(tarefas);
                                            Console.WriteLine("Selecione enter para continuar");

                                        }
                                        else
                                        {
                                            //Console.Clear();
                                            Console.WriteLine("Insira uma data válida no formato dd/MM/yyyy: ");
                                            readResult = "";
                                        }
                                    } while (readResult == "");

                                    readResult = Console.ReadLine();
                                    Console.Clear();
                                    break;
                                case "3":
                                    do
                                    {
                                        Console.WriteLine("Digite a nova descrição: ");
                                        readResult = Console.ReadLine() ?? "";
                                        if (readResult != "")
                                        {
                                            tarefa.Descricao = readResult;
                                        }
                                    } while (readResult == "");
                                    Console.WriteLine("Descrição alterada com sucesso!");
                                    RepositorioTarefa.gravarTarefas(tarefas);
                                    Console.WriteLine("Selecione enter para continuar");
                                    readResult = Console.ReadLine();
                                    Console.Clear();
                                    break;
                                case "4":
                                    Console.WriteLine("Saindo...");
                                    break;
                                default:
                                    break;
                            }

                        } while (escolha != "4");
                        //salvarTarefas();

                        break;
                    }
                }
                if (!find)
                {
                    Console.WriteLine("Tarefa não encontrada");
                }

                Console.WriteLine("Selecione enter para continuar");
                readResult = Console.ReadLine();
                break;
            }

        case "3": //criar
            {
                Console.CursorVisible = true;
                string newTitle = "";
                string newDescription = "";
                DateTime newPrazo = DateTime.Now;
                Console.Clear();

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
                        else
                        {
                            readResult = "";
                            Console.Write("Data inválida. ");
                        }
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
                RepositorioTarefa.gravarTarefas(tarefas);
                readResult = Console.ReadLine();
                break;
            }

        case "4"://mudar status
            {
                int buscaId = 0;
                string buscaNome = "x";
                bool find = false;
                string escolha = "";
                string texto = "";
                bool isConlcuida = false;
                Tarefa.StatusTarefa novoStatus = new Tarefa.StatusTarefa();

                Console.Clear();

                do
                {

                    Console.WriteLine("Digite o Id ou Titulo da tarefa: ");
                    readResult = Console.ReadLine() ?? "";
                    if (readResult != null)
                    {
                        if (int.TryParse(readResult, out int idValue))
                        {
                            buscaId = idValue;
                        }
                        else buscaNome = readResult;
                    }
                } while (readResult == "");

                foreach (var tarefa in tarefas)
                {
                    if (tarefa.Titulo == buscaNome || tarefa.Id == buscaId)
                    {
                        Console.Clear();
                        switch (tarefa.Status)
                        {
                            case Tarefa.StatusTarefa.Pendente:
                                Console.WriteLine($"Deseja iniciar a Tarefa {tarefa.Id} - {tarefa.Titulo}? ");
                                texto = $"Tarefa iniciada. Prazo: {tarefa.Prazo.ToString("dd/MM/yyyy")} \n";
                                novoStatus = Tarefa.StatusTarefa.EmAndamento;
                                break;

                            case Tarefa.StatusTarefa.EmAndamento:
                                Console.WriteLine($"Deseja concluir a Tarefa {tarefa.Id} - {tarefa.Titulo}? ");
                                texto = "Tarefa concluída!\n";
                                novoStatus = Tarefa.StatusTarefa.Concluida;
                                break;

                            case Tarefa.StatusTarefa.Concluida:
                                isConlcuida = true;
                                break;
                        }
                        (int consoleX, int consoleY) = Console.GetCursorPosition();
                        if (!isConlcuida)
                        {
                            string[] selecao3 = { "Sim", "Não", };
                            escolha = alternativas(selecao3, x: consoleX, y: consoleY);

                            if (escolha == "1")
                            {
                                Console.WriteLine(texto);
                                tarefa.Status = novoStatus;
                                if (tarefa.Status == Tarefa.StatusTarefa.Concluida)
                                    tarefa.DataConclusao = DateTime.Now;
                                RepositorioTarefa.gravarTarefas(tarefas);

                            }
                        }
                        else Console.WriteLine("Tarefa já concluida!");

                        find = true;
                        break;
                    }

                }
                if (!find)
                {
                    Console.WriteLine("Tarefa não encontrada");
                }

                Console.WriteLine("Selecione enter para continuar");
                readResult = Console.ReadLine();
                break;
            }

        case "5":
            Console.Clear();
            Console.WriteLine("Saindo...");
            Thread.Sleep(500);
            Console.Clear();

            break;

        default:

            break;
    }

} while (menuSelection != "5");

/*
void salvarTarefas()
{
    var opcoes = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    string json = JsonSerializer.Serialize(tarefas, opcoes);
    File.WriteAllText(caminhoJson, json);

    // Console.WriteLine("Tarefas Salvas!");
}
*/

string alternativas(string[] frames, bool eraseConsole = false, int x = 0, int y = 0)
{

    bool loop = true;

    int num = 0;
    int i = 0;
    int size = frames.Length;

    foreach (string frame in frames)
    {
        //Console.WriteLine($"[{frame.Trim()}]");
        frames[num] = " " + frames[num].Trim() + " ";
        string b = $"[{frame.Trim()}]";
        frames = frames.Append<string>(b).ToArray();
        num++;
    }

    do
    {
        if (eraseConsole)
        {
            Console.Clear();
        }

        Console.SetCursorPosition(x, y);

        for (int a = 0; a < size; a++)
        {
            Console.Write($"\r{frames[(i == a) ? a + size : a]}\n");
        }

        Console.WriteLine();
        //Console.Write($"\rResposta: {i + 1}\n");

        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.DownArrow:
                i++;
                if (i > size - 1)
                    i = 0;

                break;
            case ConsoleKey.UpArrow:
                if (i == 0)
                    i = size - 1;
                else i--;

                break;
            case ConsoleKey.Enter:
                loop = false;
                break;

            default:

                break;
        }
    } while (loop);
    return $"{i + 1}";
}

void alerta()
{
    Console.SetCursorPosition(0, 9);
    string linha = new string('-', 28);
    Console.WriteLine(linha);
    int atrasadas = 0;
    int venceHoje = 0;

    foreach (var tarefa in tarefas)
    {

        if (tarefa.Prazo.Date < DateTime.Today && tarefa.Status != Tarefa.StatusTarefa.Concluida)
        {
            atrasadas++;
            //Console.Write($"Você tem {atrasadas} tarefas atrasadas!");
        }
        else if (tarefa.Prazo.Date == DateTime.Today && tarefa.Status != Tarefa.StatusTarefa.Concluida)
        {
            venceHoje++;
            // Console.Write($"{venceHoje} vencem hoje!\n");
        }
    }
    if (atrasadas > 0) Console.WriteLine($"Você tem {atrasadas} tarefa(s) atrasada(s)!");
    if (venceHoje > 0) Console.WriteLine($"Você tem {venceHoje} tarefa(s) que vence(m) hoje!\n");
    if (venceHoje == 0 && atrasadas == 0) Console.WriteLine($"Nenhum prazo urgente para hoje.");


    /*
        foreach (var tarefa in tarefas)
        {
            if (tarefa.Prazo < DateTime.Now && tarefa.Status != Tarefa.StatusTarefa.Concluida)
            {
                Console.WriteLine($"A tarefa {tarefa.Id} - {tarefa.Titulo} já venceu! ");
                Console.WriteLine();
            }
            else if (tarefa.Prazo == DateTime.Now && tarefa.Status != Tarefa.StatusTarefa.Concluida)
            {
                Console.WriteLine($"A tarefa {tarefa.Id} - {tarefa.Titulo} vence hoje! ");
                Console.WriteLine();
            }
        }
        */
}



/*

                Console.WriteLine("Alterar Status para: ");
                Console.WriteLine("1-Concluida | 2-Em andamento | 3-Pendente");
                readResult = Console.ReadLine() ?? "";
                if (readResult != null)
                {
                    escolha = readResult;
                }
                switch (escolha)
                {
                    case "1":
                        Console.WriteLine("Mudar status para concluida.");
                        break;
                    case "2":
                        Console.WriteLine("Mudar status para Em andmento.");
                        break;
                    case "3":
                        Console.WriteLine("Mudar status para pendente.");
                        break;
                    default:
                        Console.WriteLine("Escolha alguma as 3 opções!");
                        break;
                        */
