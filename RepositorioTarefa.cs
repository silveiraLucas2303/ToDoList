using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Globalization;
using System.IO.Pipelines;

public class RepositorioTarefa
{
    public static List<Tarefa> lerTarefas()
    {
        List<Tarefa> tarefas = new List<Tarefa> { };

        string pastaDados = @"C:\code\ToDoList\dados";
        string caminhoJson = Path.Combine(pastaDados, "tarefas.json");
        string json = File.ReadAllText(caminhoJson);

        tarefas =
        JsonSerializer.Deserialize<List<Tarefa>>(json)
        ?? new List<Tarefa>();

        return tarefas;
    }

    public static void gravarTarefas(List<Tarefa> tarefas)
    {
        string pastaDados = @"C:\code\ToDoList\dados";
        string caminhoJson = Path.Combine(pastaDados, "tarefas.json");

        var opcoes = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(tarefas, opcoes);
        File.WriteAllText(caminhoJson, json);
    }


    public static void criarLista()
    {
        List<Tarefa> tarefas = new List<Tarefa> { };

        string pastaDados = @"C:\code\ToDoList\dados";
        string caminhoJson = Path.Combine(pastaDados, "tarefas.json");

        var opcoes = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string json = JsonSerializer.Serialize(tarefas, opcoes);
        File.WriteAllText(caminhoJson, json);
    }


    public static bool procuraLista()
    {
        bool hasJson = true;
        List<Tarefa> tarefas = new List<Tarefa> { };

        string pastaDados = @"C:\code\ToDoList\dados";
        string caminhoJson = Path.Combine(pastaDados, "tarefas.json");
        Directory.CreateDirectory(pastaDados);

        hasJson = File.Exists(caminhoJson);

        return hasJson;
    }

}