using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
            if(Program.NeedToRedraw)
            {
                DrawMenu(menuState, terminal_title, credits);
                Program.NeedToRedraw = false;
            }
        }

        private static void DrawMenu(MenuState menuState, string terminal_title, string credits)
        {
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
            if(Program.NeedToRedraw)
            {
                Console.Clear();
                Console.WriteLine(state.GetType().Name);
                Console.WriteLine(state.board.TurnMessage);
                state.board.Draw();
                Console.Write("Accept move");
                for(int i=0;i<27;i++)
                    Console.Write(' ');
                Console.WriteLine("Reset move");
                Program.NeedToRedraw = false;
            }
        }

        public static void Draw(this Board board)
        {
            //List<string> output = new List<string>();
            for (int i = 0; i < board.squares.Length; i++)
            {
                //List of final (fully constructed) lines
                List<Line> output = new List<Line>();

                List<Line> actualBoardRow = new List<Line>() { new Line(), new Line(), new Line() };
                for (int j = 0; j < board.squares[i].Length; j++)
                {
                    if (board.GetSelectedSquareAsStart() == board.squares[i][j])
                        actualBoardRow.Polacz(board.squares[i][j].CreateString(true, false));
                    else if (board.squares[i][j] is BrownSquare && board.selectedSquaresToEnd.Contains(board.squares[i][j] as BrownSquare))
                    {
                        actualBoardRow.Polacz(board.squares[i][j].CreateString(false, true));
                    }
                    else
                        actualBoardRow.Polacz(board.squares[i][j].CreateString(false, false));
                }
                output.AddRange(actualBoardRow);
                //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                //{
                //    //TO DO: ustawić na odpowiednią wartość
                //    Console.SetWindowSize(50, 25);
                //}
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

        public static List<Line> CreateString(this Square s, bool isSelectedAsStart = false, bool isSelectedAsEnd = false)
        {
            if(s is BrownSquare)
            {
                var square = (s as BrownSquare);
                if (square.Pawn == null)
                {
                    return CreateEmptySquare(isSelectedAsEnd);
                }
                else if(square.Pawn is ManPawn)
                {
                    if (square.Pawn.IsWhite)
                        return CreateWhiteManSquare(isSelectedAsStart, isSelectedAsEnd);
                    else
                        return CreateBlackManSquare(isSelectedAsStart, isSelectedAsEnd);
                }
                else
                {
                    if (square.Pawn.IsWhite)
                        return CreateWhiteDameSquare(isSelectedAsStart, isSelectedAsEnd);
                    else
                        return CreateBlackDameSquare(isSelectedAsStart, isSelectedAsEnd);
                }
            }
            else
                return CreateWhiteSquare();
        }
        //TO DO: połączyć metody CreateWhiteSquare i CreateEmptySquare
        public static List<Line> CreateWhiteSquare()
        {
            List<Line> lines = new List<Line>();
            ConsoleColor color = ConsoleColor.DarkGray;
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            return lines;
        }

        public static List<Line> CreateEmptySquare(bool isSelectedAsEnd = false)
        {
            List<Line> lines = new List<Line>();
            ConsoleColor color;
            if (isSelectedAsEnd)
                color = ConsoleColor.Red;
            else
                color = ConsoleColor.Gray;
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            return lines;
        }

        public static List<Line> CreateBlackManSquare(bool isSelectedAsStart = false, bool isSelectedAsEnd = false)
        {
            List<Line> lines = new List<Line>();
            ConsoleColor color;
            if (isSelectedAsStart)
                color = ConsoleColor.Green;
            else if (isSelectedAsEnd)
                color = ConsoleColor.Red;
            else
                color = ConsoleColor.Gray;
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            lines.Add(new Line("  ♟   ", new List<C>() { new C(color, 6, ConsoleColor.Black) }));
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            return lines;
        }
        public static List<Line> CreateWhiteManSquare(bool isSelectedAsStart = false, bool isSelectedAsEnd = false)
        {
            List<Line> lines = new List<Line>();
            ConsoleColor color;
            if (isSelectedAsStart)
                color = ConsoleColor.Green;
            else if (isSelectedAsEnd)
                color = ConsoleColor.Red;
            else
                color = ConsoleColor.Gray;
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            lines.Add(new Line("  ♙   ", new List<C>() { new C(color, 6, ConsoleColor.Black) }));
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            return lines;
        }
        public static List<Line> CreateWhiteDameSquare(bool isSelectedAsStart = false, bool isSelectedAsEnd = false)
        {
            List<Line> lines = new List<Line>();
            ConsoleColor color;
            if (isSelectedAsStart)
                color = ConsoleColor.Green;
            else if (isSelectedAsEnd)
                color = ConsoleColor.Red;
            else
                color = ConsoleColor.Gray;
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            lines.Add(new Line("  ♕   ", new List<C>() { new C(color, 6, ConsoleColor.Black) }));
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            return lines;
        }
        public static List<Line> CreateBlackDameSquare(bool isSelectedAsStart = false, bool isSelectedAsEnd = false)
        {
            List<Line> lines = new List<Line>();
            ConsoleColor color;
            if (isSelectedAsStart)
                color = ConsoleColor.Green;
            else if (isSelectedAsEnd)
                color = ConsoleColor.Red;
            else
                color = ConsoleColor.Gray;
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
            lines.Add(new Line("  ♛   ", new List<C>() { new C(color, 6) }));
            lines.Add(new Line("      ", new List<C>() { new C(color, 6) }));
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
    
  
