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
                List<Line> output = new List<Line>();

                List<Line> actualBoardRow = new List<Line>() { new Line(), new Line(), new Line(), new Line() };
                for (int j = 0; j < board.squares[i].Length; j++)
                {
                    actualBoardRow.Polacz(board.squares[i][j].CreateString());
                }
                output.AddRange(actualBoardRow);

                foreach (var o in output)
                    o.DrawLine();
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

        public static List<Line> CreateString(this Square s)
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
                        return CreateWhiteManSquare();
                    else
                        return CreateBlackManSquare();
                }
                else
                {
                    if (square.Pawn.IsWhite)
                        return CreateWhiteDameSquare();
                    else
                        return CreateBlackDameSquare();
                }
            }
            else
                return CreateWhiteSquare();
        }

        public static List<Line> CreateWhiteSquare(/*this Square s*/)
        {
            List<Line> lines = new List<Line>();
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Black, 6) }));
            lines.Add(new Line("-    -", new List<C>() { new C(ConsoleColor.Black, 6) }));
            lines.Add(new Line("-    -", new List<C>() { new C(ConsoleColor.Black, 6) }));
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Black, 6) }));
            return lines;
        }

        public static List<Line> CreateEmptySquare(/*this Square s*/)
        {
            List<Line> lines = new List<Line>();
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("-    -", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("-    -", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            return lines;
        }

        public static List<Line> CreateBlackManSquare()
        {
            List<Line> lines = new List<Line>();
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("-xxxx-", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("-xxxx-", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            return lines;
        }
        public static List<Line> CreateWhiteManSquare()
        {
            List<Line> lines = new List<Line>();
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("-yyyy-", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("-yyyy-", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            return lines;
        }
        public static List<Line> CreateWhiteDameSquare()
        {
            List<Line> lines = new List<Line>();
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("-YYYY-", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("-YYYY-", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            return lines;
        }
        public static List<Line> CreateBlackDameSquare()
        {
            List<Line> lines = new List<Line>();
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("-XXXX-", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("-XXXX-", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            lines.Add(new Line("+----+", new List<C>() { new C(ConsoleColor.Yellow, 6) }));
            return lines;
        }


        public static void Polacz(this List<Line> l1, List<Line> l2)
        {
            if (l1.Count != l2.Count)
                throw new Exception("..");
            for (int i = 0; i < l1.Count; i++)
            {
                l1[i].msg += l2[i].msg;
                l1[i].colors.AddRange(l2[i].colors);
                //msg += line.msg;
                //colors.AddRange(line.colors);
            }
        }
    }
}
    
  