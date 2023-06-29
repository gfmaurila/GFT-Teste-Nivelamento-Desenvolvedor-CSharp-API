using Questao5.Domain.Entities;
using Questao5.Domain.View;

namespace Questao5.Domain.Contracts;
public interface IMovimentoRepository
{
    Task<SaldoContaCorrenteView> GetSaldoAsync(int numero);
    Task<string> AddAsync(Movimento movimento);
}
