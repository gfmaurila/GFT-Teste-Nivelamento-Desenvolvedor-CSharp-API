using Questao5.Domain.Entities;

namespace Questao5.Domain.Contracts;
public interface IIdempotenciaRepository
{
    Task<Idempotencia> GetByChaveIdempotenciaAsync(string chave);
    Task AddAsync(Idempotencia entity);
}
