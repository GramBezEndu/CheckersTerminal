using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public class WhiteMan : ManPawn
    {
        public WhiteMan(Square[][] sq) : base(sq)
        {
        }

        public override bool CanMove(BrownSquare end)
        {
            if (base.CanMove(end) == false)
                return false;
            else
            {
                return true;
            }
        }
    }
}
