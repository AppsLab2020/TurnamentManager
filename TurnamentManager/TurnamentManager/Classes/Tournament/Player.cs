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
        public int ImageId { get; set; }
        public Team Team;

        public PlayerQuality CurrPlayerQuality;
        
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        public Player(string name, int imageId, PlayerQuality currPlayerQuality)
        {
            Name = name;
            ImageId = imageId;
            CurrPlayerQuality = currPlayerQuality;
        }

        public Player()
        {
        }
    }
}