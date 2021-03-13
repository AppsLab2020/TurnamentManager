using System.Collections.Generic;
using SQLite;

namespace TurnamentManager.Classes.Tournament
{
    public class Team
    {
        public List<Player> Players { get; set; }
        public string Name { get; set; }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Team()
        {
            Players = new List<Player>();
        }
    }
}