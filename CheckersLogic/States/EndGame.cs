using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic.States
{
    public class EndGame : State
    {
        public bool WhiteWon;
        public EndGame(bool whiteWon)
        {
            whiteWon = whiteWon;
        }
    }
}
