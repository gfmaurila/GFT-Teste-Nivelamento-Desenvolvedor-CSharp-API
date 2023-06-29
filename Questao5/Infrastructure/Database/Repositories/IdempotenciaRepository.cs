using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories;
public class IdempotenciaRepository : IIdempotenciaRepository
{
    private readonly DatabaseConfig databaseConfig;

    public IdempotenciaRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public async Task AddAsync(Idempotencia entity)
    {
        using (var connection = new SqliteConnection(databaseConfig.Name))
        {
            var sql = @"
                    INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) 
                    VALUES (@ChaveIdempotencia, @Requisicao, @Resultado)";
            await connection.ExecuteAsync(sql, entity);
        }
    }

    public async Task<Idempotencia> GetByChaveIdempotenciaAsync(string chave)
    {
        using (var connection = new SqliteConnection(databaseConfig.Name))
        {
            await connection.OpenAsync();
            var query = @"SELECT chave_idempotencia AS ChaveIdempotencia, 
                                   requisicao AS Requisicao,
                                   resultado AS Resultado 
                            FROM idempotencia
                            WHERE chave_idempotencia = @chave ";
            var result = await connection.QueryFirstOrDefaultAsync<Idempotencia>(query, new { chave });
            return result;
        }
    }

}
