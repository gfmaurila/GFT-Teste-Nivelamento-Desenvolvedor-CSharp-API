using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories;
public class ContacorrenteRepository : IContacorrenteRepository
{
    private readonly DatabaseConfig databaseConfig;

    public ContacorrenteRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public async Task<Contacorrente> GetByNumeroAsync(int numero)
    {
        using (var connection = new SqliteConnection(databaseConfig.Name))
        {
            await connection.OpenAsync();
            var query = @"SELECT idcontacorrente AS IdContaCorrente, numero AS Numero, nome AS Nome, ativo AS Ativo 
                          FROM contacorrente
                          WHERE numero = @numero ";
            var result = await connection.QueryFirstOrDefaultAsync<Contacorrente>(query, new { numero });
            return result;
        }
    }

}
