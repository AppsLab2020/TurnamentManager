using SQLite;

namespace TurnamentManager.Classes.Tournament
{
    public class Player
    {
        public enum PlayerQuality
        {
            Flexible,
            Strategic,
            SharesTheirExpertise,
            RespectfulToOthers,
            ContributeIdeas
        }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public Team Team;

        public PlayerQuality CurrPlayerQuality;
        
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        public Player(string name, string imagePath, PlayerQuality currPlayerQuality)
        {
            Name = name;
            ImagePath = imagePath;
            CurrPlayerQuality = currPlayerQuality;
        }

        public Player()
        {
        }
    }
}