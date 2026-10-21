string? readResult; //documentar a razão dessa interrogação
string menuSelection = "";

do
{
    Console.Clear();
    printMenu();

    readResult = Console.ReadLine();
    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }


    switch (menuSelection)
    {
        case "1":
            Console.WriteLine("Trabalhando nisso...");

            Console.WriteLine("Selecione enter para continuar");
            readResult = Console.ReadLine();
            break;
        case "2":
            Console.WriteLine("Trabalhando nisso...");

            Console.WriteLine("Selecione enter para continuar");
            readResult = Console.ReadLine();
            break;
        case "3":
            Console.WriteLine("Trabalhando nisso...");

            Console.WriteLine("Selecione enter para continuar");
            readResult = Console.ReadLine();
            break;

        case "4":
            Console.WriteLine("Trabalhando nisso...");

            Console.WriteLine("Selecione enter para continuar");
            readResult = Console.ReadLine();
            break;

        default:
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






