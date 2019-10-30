using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersTerminal.Draw
{
    public class Line
    {
        public String msg;
        public List<C> colors = new List<C>();

        /// <summary>
        /// new empty line
        /// </summary>
        public Line()
        {
            msg = "";
        }

        public Line(string message, List<C> c)
        {
            msg = message;
            colors = c;
        }

        public void Add(Line line)
        {
            msg += line.msg;
            colors.AddRange(line.colors);
        }


        public void DrawLine()
        {
            int actualIndex = 0;
            foreach(var c in colors)
            {
                Console.BackgroundColor = c.backgroundColor;
                Console.ForegroundColor = c.foregroundColor;
                Console.Write(msg.Substring(actualIndex, c.howManyCharacters));
                actualIndex += c.howManyCharacters;
            }
            //reset color
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
    }
}
