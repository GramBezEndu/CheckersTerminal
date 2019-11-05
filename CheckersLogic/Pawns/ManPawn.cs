using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public class ManPawn : Pawn
    {
        public ManPawn(bool isWhite) : base(isWhite) { }

        public override bool IsTakedownMove(BrownSquare end, Square[][] squares, Boolean shouldTakeDown, BrownSquare beg = null)
        {
            int x, y;
            if (beg == null)
            {
                x = Position.xIndex;
                y = Position.yIndex;
            }
            else
            {
                x = beg.xIndex;
                y = beg.yIndex;
            }
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
                        if (IsDifferentColor(target))
                        {
                            if (shouldTakeDown)
                                TakedownList.Add(target);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public override bool IsRegularMove(BrownSquare end, Square[][] squares)
        {
            int x = Position.xIndex;
            int y = Position.yIndex;
            int xDistance = end.xIndex - x;
            int yDistance = end.yIndex - y;
            if (Math.Abs(xDistance) == 1 && end.Pawn == null)
            {
                if (IsWhite && yDistance == -1)
                    return true;
                if (!IsWhite && yDistance == 1)
                    return true;
            }
            return false;
        }
    }
}
