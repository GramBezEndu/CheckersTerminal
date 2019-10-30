using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public class Dame : Pawn
    {
        public Dame(bool isWhite) : base(isWhite)
        {
        }
        protected override bool IsRegularMove(BrownSquare end, Square[][] squares)
        {
            int x = Position.xIndex;
            int y = Position.yIndex;
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
                    for (int i = 1; i < absDistance; i++)
                    {
                        Pawn target = (squares[x + xDistance / absDistance * i][y + yDistance / absDistance * i] as BrownSquare).Pawn;
                        if (target != null)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public override bool IsTakedownMove(BrownSquare end, Square[][] squares)
        {
            int x = Position.xIndex;
            int y = Position.yIndex;
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
                    for (int i = 1; i < absDistance; i++)
                    {
                        Pawn target = (squares[x + xDistance / absDistance * i][y + yDistance / absDistance * i] as BrownSquare).Pawn;
                        if (target != null)
                        {
                            if (i == absDistance - 1 && IsDifferentColor(target))
                            {
                                TakedownList.Add(target);
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            return false;
        }
    }
}
