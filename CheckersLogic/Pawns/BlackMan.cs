using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public class BlackMan : ManPawn
    {
        public BlackMan(Square[][] sq) : base(sq)
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
