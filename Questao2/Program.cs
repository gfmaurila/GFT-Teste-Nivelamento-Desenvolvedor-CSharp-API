using Questao2.Infra;

public class Program
{
    static readonly HttpClient client = new HttpClient();

    public static async Task Main()
    {
        var teamFactory = new TeamFactory(client);

        string teamName = "Paris Saint-Germain";
        var team = teamFactory.Create(teamName);
        int year = 2013;
        int totalGoals = await team.GetTotalScoredGoals(year);
        Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");

        teamName = "Chelsea";
        team = teamFactory.Create(teamName);
        year = 2014;
        totalGoals = await team.GetTotalScoredGoals(year);
        Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");
    }
}