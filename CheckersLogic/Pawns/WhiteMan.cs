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

        public override bool IsTakedownMove(BrownSquare end)
        {
            int x = position.xIndex;
            int y = position.yIndex;
            //Common conditions
            if (end.xIndex == x + 2 && end.Pawn == null)
            {
                //case 1 (left)
                if (end.yIndex == y - 2)
                {
                    Pawn target = (squares[end.xIndex - 1][end.yIndex + 1] as BrownSquare).Pawn;
                    if (target != null)
                    {
                        if (target is BlackDame || target is BlackMan)
                            return true;
                    }
                }
                //case 2 (right)
                else if (end.yIndex == y + 2)
                {
                    Pawn target = (squares[end.xIndex - 1][end.yIndex - 1] as BrownSquare).Pawn;
                    if (target != null)
                    {
                        if (target is BlackDame || target is BlackDame)
                            return true;
                    }
                }
            }
            return false;
        }

        protected override bool IsRegularMove(BrownSquare end)
        {
            int x = position.xIndex;
            int y = position.yIndex;
            if (end.xIndex == x + 1 && (end.yIndex == y - 1 || end.yIndex == y + 1) && end.Pawn == null)
            {
                return true;
            }
            return false;
        }
    }
}
