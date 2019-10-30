using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersTerminal.Draw
{
    public class Line
    {
        string msg;
        List<C> colors = new List<C>();

        public Line(string message, List<C> c)
        {
            msg = message;
            colors = c;
        }

        public void DrawLine()
        {
            int actualIndex = 0;
            foreach(var c in colors)
            {
                Console.BackgroundColor = c.color;
                Console.Write(msg.Substring(actualIndex, c.howManyCharacters));
                actualIndex += c.howManyCharacters;
            }
            //reset color
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
    }
}
