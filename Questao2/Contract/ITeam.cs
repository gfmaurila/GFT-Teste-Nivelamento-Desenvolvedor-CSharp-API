namespace Questao2.Contract;
public interface ITeam
{
    Task<int> GetTotalScoredGoals(int year);
}
