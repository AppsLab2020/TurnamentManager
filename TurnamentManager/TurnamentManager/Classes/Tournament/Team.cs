using System.Collections.Generic;

namespace TurnamentManager.Classes.Tournament
{
    public class Team
    {
        public readonly List<Player> Players;

        public Team()
        {
            Players = new List<Player>();
        }
    }
}