namespace Questao5.Application.ViewModels;
public class IdempotenciaModel
{
    public IdempotenciaModel(string resultado)
    {
        Resultado = resultado;
    }
    public string Resultado { get; set; }
}
