using System;

public class Tarefa
{

    public enum StatusTarefa
    {
        Concluida,
        EmAndamento,
        Pendente,
    }

    public int id { get; set; }
    public string Titulo { get; set; } = "";
    public string Descricao { get; set; } = "";
    public DateTime DataRegistro { get; set; }
    public DateTime Prazo { get; set; }
    public DateTime DataConclusao { get; set; }

    public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;

}



