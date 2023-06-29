namespace Questao5.Domain.View;
public class SaldoContaCorrenteView
{
    public string IdContacorrente { get; set; }
    public int NumeroConta { get; set; }
    public int Ativo { get; set; }
    public string NomeTitular { get; set; }
    public decimal SomaCredito { get; set; }
    public decimal SomaDebito { get; set; }
    public decimal Saldo { get; set; }
}
