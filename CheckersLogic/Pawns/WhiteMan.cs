﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public class WhiteMan : ManPawn
    {
        public WhiteMan(Square[][] sq) : base(sq)
        {
        }

        public override bool Move(Square end)
        {
            throw new NotImplementedException();
        }
    }
}
