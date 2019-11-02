using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic.States
{
    public abstract class GameState : State
    {
        public Board board;
        public GameState()
        {
            board = new Board();
        }
    }
}
