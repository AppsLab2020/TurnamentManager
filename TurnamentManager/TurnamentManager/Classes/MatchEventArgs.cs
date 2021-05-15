using System;
using System.Collections.Generic;
using System.Text;

namespace TurnamentManager.Classes
{
    public class MatchEventArgs : EventArgs
    {
        private readonly string leftSide;
        private readonly string rightSide;

        public MatchEventArgs(string left, string right)
        {
            this.leftSide = left;
            this.rightSide = right;
        }

        public string LeftSide
        {
            get
            {
                return this.leftSide;

            }
        }

        public string RightSide
        {
            get
            {
                return this.rightSide;

            }
        }
    }
}
