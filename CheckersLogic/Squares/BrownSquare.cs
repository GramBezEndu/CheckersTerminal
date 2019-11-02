using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public class BrownSquare : Square
    {
        private Pawn _pawn;

        public Pawn Pawn
        {
            get => _pawn;
            set
            {
                _pawn = value;
                if(_pawn != null)
                    _pawn.Position = this;
            }
        }
        public BrownSquare(int x, int y) : base(x, y)
        {
        }
    }
}
