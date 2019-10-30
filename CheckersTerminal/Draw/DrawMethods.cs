using System;
using System.Collections.Generic;
using System.Text;
using CheckersLogic;
using CheckersLogic.States;

namespace CheckersTerminal.Draw
{
    public static class DrawMethods
    {
        public static void Draw(this MenuState menuState)
            {
                //Grafiki
                string terminal_title =
                " ██████╗██╗  ██╗███████╗ ██████╗██╗  ██╗███████╗██████╗ ███████╗\n" +
                "██╔════╝██║  ██║██╔════╝██╔════╝██║ ██╔╝██╔════╝██╔══██╗██╔════╝\n" +
                "██║     ███████║█████╗  ██║     █████╔╝ █████╗  ██████╔╝███████╗\n" +
                "██║     ██╔══██║██╔══╝  ██║     ██╔═██╗ ██╔══╝  ██╔══██╗╚════██║\n" +
                "╚██████╗██║  ██║███████╗╚██████╗██║  ██╗███████╗██║  ██║███████║\n" +
                " ╚═════╝╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚══════╝\n" +
                "\n";

                //string terminal_title =
                //" ▄████▄   ██░ ██ ▓█████  ▄████▄   ██ ▄█▀▓█████  ██▀███    ██████ \n" +
                //"▒██▀ ▀█  ▓██░ ██▒▓█   ▀ ▒██▀ ▀█   ██▄█▒ ▓█   ▀ ▓██ ▒ ██▒▒██    ▒ \n" +
                //"▒▓█    ▄ ▒██▀▀██░▒███   ▒▓█    ▄ ▓███▄░ ▒███   ▓██ ░▄█ ▒░ ▓██▄   \n" +
                //"▒▓▓▄ ▄██▒░▓█ ░██ ▒▓█  ▄ ▒▓▓▄ ▄██▒▓██ █▄ ▒▓█  ▄ ▒██▀▀█▄    ▒   ██▒\n" +
                //"▒ ▓███▀ ░░▓█▒░██▓░▒████▒▒ ▓███▀ ░▒██▒ █▄░▒████▒░██▓ ▒██▒▒██████▒▒\n" +
                //"░ ░▒ ▒  ░ ▒ ░░▒░▒░░ ▒░ ░░ ░▒ ▒  ░▒ ▒▒ ▓▒░░ ▒░ ░░ ▒▓ ░▒▓░▒ ▒▓▒ ▒ ░\n" +
                //"  ░  ▒    ▒ ░▒░ ░ ░ ░  ░  ░  ▒   ░ ░▒ ▒░ ░ ░  ░  ░▒ ░ ▒░░ ░▒  ░ ░\n" +
                //"░         ░  ░░ ░   ░   ░        ░ ░░ ░    ░     ░░   ░ ░  ░  ░  \n" +
                //"░ ░       ░  ░  ░   ░  ░░ ░      ░  ░      ░  ░   ░           ░  \n" +
                //"░                       ░                                        \n";


                string credits = "By Paweł Bąk, Wojciech Mojsiejuk, Jakub Mroczkowski\n";

                //Rysowanie menu

                Console.Clear();
                Console.Write(terminal_title);
                Console.WriteLine(credits);
                string border = " * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *";
                string empty_border = " *                                                                 *";
                string option_border = " *    ";
                Console.WriteLine(border);


                for (int i = 0; i < menuState.options.Count; i++)
                {
                    Console.WriteLine(empty_border);
                    Console.Write(option_border);
                    if (i == menuState.index)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(menuState.options[i].Name);
                    }
                    else
                    {
                        Console.Write(menuState.options[i].Name);
                    }
                    Console.ResetColor();
                    int space = border.Length - option_border.Length - menuState.options[i].Name.Length - 1;
                    for (int j = 0; j < space; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("*\n");
                    Console.WriteLine(empty_border);
                }
                Console.WriteLine(border);
               
            }
        public static void Draw(this State State)
        {
            if (State is MenuState)
            {
                (State as MenuState).Draw();
            }
            else if(State is GameState)
            {
                (State as GameState).Draw();
            }
        }
        public static void Draw(this GameState state)
        {
            Console.Clear();
            Console.WriteLine(state.GetType().Name);
            Console.WriteLine(state.board.TurnMessage);
            state.board.Draw();
        }

        public static void Draw(this Board board)
        {
            //List<string> output = new List<string>();
            for (int i = 0; i < board.squares.Length; i++)
            {
                //List of final (fully constructed) lines
                List<string> output = new List<string>();

                List<string> actualBoardRow = new List<string>() { "", "", "", "" };
                for (int j = 0; j < board.squares[i].Length; j++)
                {
                    actualBoardRow.AddLines(board.squares[i][j].CreateString());
                }
                output.AddRange(actualBoardRow);

                foreach (var o in output)
                    Console.WriteLine(o);
            }
            //output.AddRange(CreateEmptySquare());
            //foreach (var s in output)
            //    Console.WriteLine(s);
            //board.squares.lenght == how many rows on board

            /*
            List<string> output = new List<string>();
            for (int i = 0; i < board.squares.Length; i++)
            {
                int howManySquaresInThisRow = board.squares[i].Length;
                string actualLine = "";
                for(int j=0;j<howManySquaresInThisRow;j++)
                {
                    actualLine += "+---+";
                }
                output.Add(actualLine);
            }
            foreach (var s in output)
                Console.WriteLine(s);
            */
        }

        public static List<string> CreateString(this Square s)
        {
            if(s is BrownSquare)
            {
                var square = (s as BrownSquare);
                if (square.Pawn == null)
                {
                    return CreateEmptySquare();
                }
                else if(square.Pawn is ManPawn)
                {
                    if (square.Pawn.IsWhite)
                        return CreateManSquare();
                    else
                        return CreateManSquare();
                }
                else
                {
                    if (square.Pawn.IsWhite)
                        return CreateDameSquare();
                    else
                        return CreateDameSquare();
                }
            }
            else
                return CreateEmptySquare();
        }

        public static List<string> CreateEmptySquare(/*this Square s*/)
        {
            List<string> lines = new List<string>();
            lines.Add("+----+");
            lines.Add("-    -");
            lines.Add("-    -");
            lines.Add("+----+");
            return lines;
        }
        public static List<string> CreateManSquare()
        {
            List<string> lines = new List<string>();
            lines.Add("+----+");
            lines.Add("-xxxx-");
            lines.Add("-xxxx-");
            lines.Add("+----+");
            return lines;
        }
        public static List<string> CreateDameSquare()
        {
            List<string> lines = new List<string>();
            lines.Add("+----+");
            lines.Add("-yyyy-");
            lines.Add("-yyyy-");
            lines.Add("+----+");
            return lines;
        }

        public static void AddLines(this List<string> l1, List<string> l2)
        {
            if (l1.Count != l2.Count)
                throw new Exception("..");
            for(int i=0;i<l1.Count;i++)
            {
                l1[i] += l2[i];
            }
        }
    }
}
    
  