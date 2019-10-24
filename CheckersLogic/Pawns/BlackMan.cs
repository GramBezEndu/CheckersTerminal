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

        public override bool Move(Square end)
        {
            if (base.Move(end) == false)
                return false;
            else
            {
                return true;
            }
        }
    }
}
