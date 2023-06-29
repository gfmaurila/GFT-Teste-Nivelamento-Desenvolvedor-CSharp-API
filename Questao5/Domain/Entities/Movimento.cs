namespace Questao5.Domain.Entities;
public class Movimento
{
    public Movimento(string idContaCorrente, string tipoMovimento, decimal valor)
    {
        IdContaCorrente = idContaCorrente;
        TipoMovimento = tipoMovimento;
        Valor = valor;
        IdMovimento = Guid.NewGuid().ToString();
        DataMovimento = DateTime.Now.ToString("dd/MM/yyyy");
    }

    public string IdMovimento { get; set; }
    public string IdContaCorrente { get; set; }
    public string DataMovimento { get; set; }
    public string TipoMovimento { get; set; }
    public decimal Valor { get; set; }
}

