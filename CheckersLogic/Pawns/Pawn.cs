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
        protected bool isWhite;
        protected Square position;
        protected List<Pawn> takedownList;

        public bool IsWhite { get => isWhite; set => isWhite = value; }
        public Square Position { get => position; set => position = value; }
        public List<Pawn> TakedownList { get => takedownList; set => takedownList = value; }

        protected Pawn (bool isWhite)
        {
            this.isWhite = isWhite;
            this.takedownList = new List<Pawn>();
        }

        public bool IsDifferentColor(Pawn pawn)
        {
            if (this.IsWhite == pawn.IsWhite)
                return false;
            return true;
        }

        public bool IsValidMove(BrownSquare end, Square[][] squares)
        {
            if (IsRegularMove(end, squares))
                return true;
            if (IsTakedownMove(end, squares))
                return true;
            return false;
        }
        public abstract bool IsRegularMove(BrownSquare end, Square[][] squares);
        public abstract bool IsTakedownMove(BrownSquare end, Square[][] squares);
    }
}
