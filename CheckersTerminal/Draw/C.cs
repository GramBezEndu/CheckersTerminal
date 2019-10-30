using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersTerminal.Draw
{
    public class C
    {
        public int howManyCharacters;
        public ConsoleColor color;
        public C(ConsoleColor c, int x)
        {
            color = c;
            howManyCharacters = x;
        }
    }
}
