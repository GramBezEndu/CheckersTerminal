using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public abstract class Dame : Pawn
    {
        public Dame(Square[][] sq) : base(sq)
        {
        }
    }
}
