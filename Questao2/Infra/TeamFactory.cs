using Questao2.Contract;

namespace Questao2.Infra;

public class TeamFactory
{
    private readonly HttpClient client;

    public TeamFactory(HttpClient client)
    {
        this.client = client;
    }

    public ITeam Create(string teamName)
    {
        return new Team(teamName, client);
    }
}
