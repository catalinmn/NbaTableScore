namespace NbaTableScore_Backend.Models
{
    public class Game
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Season { get; set; }
        public string HomeTeam { get; set; }
        public string VisitorTeam { get; set; }
        public string HomeTeamScore { get; set; }
        public string VisitorTeamScore { get; set; }
        public string HomeTeamLogo { get; set; }
        public string VisitorTeamLogo { get; set; }

        public Game(string id, string date, string season, string homeTeam, string visitorTeam, string homeTeamScore, string visitorTeamScore, string homeTeamLogo, string visitorTeamLogo)
        {
            Id = id;
            Date = date;
            Season = season;
            HomeTeam = homeTeam;
            VisitorTeam = visitorTeam;
            HomeTeamScore = homeTeamScore;
            VisitorTeamScore = visitorTeamScore;
            HomeTeamLogo = homeTeamLogo;
            VisitorTeamLogo = visitorTeamLogo;
        }
    }
}
