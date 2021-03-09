using System.Collections.Generic;

namespace TurnamentManager.Classes.Tournament
{
    public class Tournament
    {
        public bool IsTeamBased;
        public string Style;
        public string Format;

        public readonly List<Team> Teams;

        public Tournament()
        {
            Teams = new List<Team>();
        }
        
        
    }
}