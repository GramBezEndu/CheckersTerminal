using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersTerminal.Draw
{
    public class C
    {
        public int howManyCharacters;
        public ConsoleColor backgroundColor;
        public ConsoleColor foregroundColor;
        public C(ConsoleColor c, int x, ConsoleColor foreground = ConsoleColor.Gray)
        {
            backgroundColor = c;
            howManyCharacters = x;
            foregroundColor = foreground;
        }
    }
}
