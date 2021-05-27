using System.Collections.Generic;
using SQLite;

namespace TurnamentManager.Classes.Tournament
{
    public class Tournament
    {
        public bool IsTeamBased { get; set; }
        public string Style { get; set; }
        public string Format { get; set; }
        public string Name { get; set; }
        
        public int Trophy { get; set; }
        
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Ignore]
        public List<int> PlayersID { get; set; }

        public string PlayersIDString { get; set; }

        public string MatchesString { get; set; }
        
        public Tournament()
        {
            PlayersID = new List<int>();
            GetPlayerIDs();
        }

        public void CreatePlayerIDString()
        {
            foreach (var id in PlayersID)
            {
                PlayersIDString += id.ToString() + " ";
            }
        }

        public void GetPlayerIDs()
        {
            if (string.IsNullOrEmpty(PlayersIDString))
                return;

            var splitted = PlayersIDString.Split(' ');
            foreach (var id in splitted)
            {
                PlayersID.Add(int.Parse(id));
            }
        }
    }
}