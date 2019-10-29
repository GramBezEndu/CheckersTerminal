using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public class BlackDame : Dame
    {
        public BlackDame(Square[][] sq) : base(sq)
        {
        }

        public override bool IsTakedownMove(BrownSquare end)
        {
            int x = position.xIndex;
            int y = position.yIndex;
            int xDistance;
            int yDistance;
            int absDistance;

            if (end.Pawn == null)
            {
                xDistance = end.xIndex - x;
                yDistance = end.yIndex - y;
                absDistance = Math.Abs(xDistance);
                if (absDistance == Math.Abs(yDistance))
                {
                    for(int i = 0; i < absDistance - 1; i++)
                    {
                        Pawn target = (squares[x + xDistance / absDistance * i][y + yDistance / absDistance * i] as BrownSquare).Pawn;
                        if (target != null)
                        {
                            if (i == absDistance - 2 && (target is WhiteMan || target is WhiteDame))
                            {
                                takedown.Add(target);
                                return true;
                            } else
                            {
                                return false;
                            }
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
            int xDistance;
            int yDistance;
            int absDistance;

            if (end.Pawn == null)
            {
                xDistance = end.xIndex - x;
                yDistance = end.yIndex - y;
                absDistance = Math.Abs(xDistance);
                if (absDistance == Math.Abs(yDistance))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
