﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public abstract class Dame : Pawn
    {
        public Dame(Square[][] sq) : base(sq)
        {
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
    }
}
