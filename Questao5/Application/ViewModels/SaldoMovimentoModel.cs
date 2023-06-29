namespace Questao5.Application.ViewModels;
public class SaldoMovimentoModel
{
    public SaldoMovimentoModel(int numeroConta, string nomeTitula, string dataHoraConsulta, decimal saldo)
    {
        NumeroConta = numeroConta;
        NomeTitular = nomeTitula;
        DataHoraConsulta = dataHoraConsulta;
        Saldo = saldo;

    }
    public int NumeroConta { get; set; }
    public string NomeTitular { get; set; }
    public string DataHoraConsulta { get; set; }
    public decimal Saldo { get; set; }
}
