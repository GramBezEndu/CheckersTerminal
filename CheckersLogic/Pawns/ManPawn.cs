using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public abstract class ManPawn : Pawn
    {
        public ManPawn(Square[][] sq) : base(sq)
        {
        }

        public override bool CanMove(BrownSquare end)
        {
            return true;
            //if(end.xIndex )
        }
    }
}
