using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public abstract class Pawn : DrawableComponent
    {
        /// <summary>
        /// reference to square where pawn is
        /// </summary>
        public Square position;
        /// <summary>
        /// reference to squares
        /// </summary>
        public Square[][] squares;
        public Pawn(Square[][] sq)
        {
            squares = sq;
        }
        public bool CanMove(BrownSquare end)
        {
            if (IsRegularMove(end))
                return true;
            else if (IsTakedownMove(end))
                return true;
            else
                return false;
        }
        protected abstract bool IsRegularMove(BrownSquare end);
        public abstract bool IsTakedownMove(BrownSquare end);
    }
}
