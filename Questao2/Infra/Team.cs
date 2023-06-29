using Newtonsoft.Json.Linq;
using Questao2.Contract;

namespace Questao2.Infra;
public class Team : ITeam
{
    private readonly string name;
    private readonly HttpClient client;

    public Team(string name, HttpClient client)
    {
        this.name = name;
        this.client = client;
    }

    public async Task<int> GetTotalScoredGoals(int year)
    {
        int totalGoals = 0;
        int currentPage = 1;

        while (true)
        {
            var responseStringTeam1 = await client.GetStringAsync("https://jsonmock.hackerrank.com/api/football_matches?year=" + year + "&team1=" + this.name + "&page=" + currentPage);
            var responseStringTeam2 = await client.GetStringAsync("https://jsonmock.hackerrank.com/api/football_matches?year=" + year + "&team2=" + this.name + "&page=" + currentPage);
            var matchesJsonTeam1 = JObject.Parse(responseStringTeam1);
            var matchesJsonTeam2 = JObject.Parse(responseStringTeam2);
            var matchesTeam1 = matchesJsonTeam1["data"].ToObject<JArray>();
            var matchesTeam2 = matchesJsonTeam2["data"].ToObject<JArray>();

            foreach (var match in matchesTeam1)
            {
                if (match["team1"].ToString() == this.name)
                {
                    totalGoals += int.Parse(match["team1goals"].ToString());
                }
            }

            foreach (var match in matchesTeam2)
            {
                if (match["team2"].ToString() == this.name)
                {
                    totalGoals += int.Parse(match["team2goals"].ToString());
                }
            }

            if (currentPage >= Math.Max((int)matchesJsonTeam1["total_pages"], (int)matchesJsonTeam2["total_pages"]))
            {
                break;
            }
            currentPage++;
        }

        return totalGoals;
    }
}