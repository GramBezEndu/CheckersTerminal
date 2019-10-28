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

        public override bool IsTakedownMove(BrownSquare end)
        {
            throw new NotImplementedException();
        }

        protected override bool IsRegularMove(BrownSquare end)
        {
            throw new NotImplementedException();
        }
    }
}
