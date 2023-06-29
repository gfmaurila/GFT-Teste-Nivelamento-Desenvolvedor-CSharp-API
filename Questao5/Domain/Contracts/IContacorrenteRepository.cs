using Questao5.Domain.Entities;

namespace Questao5.Domain.Contracts;
public interface IContacorrenteRepository
{
    Task<Contacorrente> GetByNumeroAsync(int numero);
}
