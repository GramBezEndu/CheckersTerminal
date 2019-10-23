using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public abstract class Pawn : DrawableComponent
    {
        public Square position;
        public abstract bool Move(Square end);
    }
}
