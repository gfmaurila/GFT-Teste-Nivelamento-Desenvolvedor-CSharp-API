using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;
using Questao5.Domain.View;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories;
public class MovimentoRepository : IMovimentoRepository
{
    private readonly DatabaseConfig databaseConfig;

    public MovimentoRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public async Task<SaldoContaCorrenteView> GetSaldoAsync(int numero)
    {
        using (var connection = new SqliteConnection(databaseConfig.Name))
        {
            await connection.OpenAsync();
            var query = @"SELECT c.idcontacorrente as IdContacorrente,
                               c.numero as NumeroConta,
                               c.nome as NomeTitular,
                               c.ativo as Ativo,
                               SUM(CASE WHEN m.tipomovimento = 'C' THEN m.valor ELSE 0 END) AS SomaCredito,
                               SUM(CASE WHEN m.tipomovimento = 'D' THEN m.valor ELSE 0 END) AS SomaDebito,
                               (SUM(CASE WHEN m.tipomovimento = 'C' THEN m.valor ELSE 0 END) - 
                                SUM(CASE WHEN m.tipomovimento = 'D' THEN m.valor ELSE 0 END)) AS Saldo
                        FROM contacorrente c
                        LEFT JOIN movimento as m ON c.idcontacorrente = m.idcontacorrente
                        WHERE c.numero = @numero
                        GROUP BY c.idcontacorrente";
            var result = await connection.QueryFirstOrDefaultAsync<SaldoContaCorrenteView>(query, new { numero });
            return result;
        }
    }

    public async Task<string> AddAsync(Movimento movimento)
    {
        using (var connection = new SqliteConnection(databaseConfig.Name))
        {
            var sql = @"
                    INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                    VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)";

            await connection.ExecuteAsync(sql, movimento);

            return movimento.IdMovimento;
        }
    }

}
