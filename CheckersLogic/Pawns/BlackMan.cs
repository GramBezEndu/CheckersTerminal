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

        public override bool IsTakedownMove(BrownSquare end)
        {
            int x = position.xIndex;
            int y = position.yIndex;
            int xDistance;
            int yDistance;
            if (end.Pawn == null)
            {
                xDistance = end.xIndex - x;
                yDistance = end.yIndex - y;
                if (Math.Abs(xDistance) == 2 && Math.Abs(yDistance) == 2)
                {
                    Pawn target = (squares[x + xDistance / 2][y + yDistance / 2] as BrownSquare).Pawn;
                    if (target != null)
                    {
                        if (target is WhiteDame || target is WhiteMan)
                        {
                            takedown.Add(target);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        protected override bool IsRegularMove(BrownSquare end)
        {
            int x = position.xIndex;
            int y = position.yIndex;
            int xDistance = end.xIndex - x;
            int yDistance = end.yIndex - y;
            if(xDistance == -1 && Math.Abs(yDistance) == 1 && end.Pawn == null)
            {
                return true;
            }
            return false;
        }
    }
}
