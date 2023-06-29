﻿namespace Questao5.Domain.Entities;
public class Idempotencia
{
    public Idempotencia(string chaveidempotencia, string requisicao, string resultado)
    {
        ChaveIdempotencia = chaveidempotencia;
        Requisicao = requisicao;
        Resultado = resultado;
    }
    public string ChaveIdempotencia { get; set; }
    public string Requisicao { get; set; }
    public string Resultado { get; set; }
}
