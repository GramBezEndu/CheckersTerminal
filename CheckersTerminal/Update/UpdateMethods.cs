using System;
using System.Diagnostics;
using CheckersLogic;
using CheckersLogic.States;
using CheckersTerminal;

namespace CheckersTerminal.Update
{
    public static class UpdateMethods
    {
        /// <summary>
        /// Add mouse events
        /// </summary>
        /// <param name="s"></param>
        public static void Init(this State s)
        {
            if(s is GameState)
            {
                (s as GameState).Init();
            }
        }
        
        public static void Init(this GameState gameState)
        {
            ConsoleListener.MouseEvent += (r) => BoardMouseHandling(gameState.board, r, 0, 2);
        }

        private static void BoardMouseHandling(Board board, NativeMethods.MOUSE_EVENT_RECORD r, int startIndexX, int startIndexY)
        {
            //Check for mouse input (hard coded texture width and height)
            for (int i = 0; i < board.squares.Length; i++)
            {
                for (int j = 0; j < board.squares[i].Length; j++)
                {
                    if(r.dwMousePosition.X >= (startIndexX + j *6) && r.dwMousePosition.X <= (startIndexX + j * 6) + 6
                        && r.dwMousePosition.Y >= (startIndexY + i * 3) && r.dwMousePosition.Y <= (startIndexX + i * 3) + 3)
                    {
                        if(r.dwButtonState == NativeMethods.MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
                        {
                            if (board.squares[i][j] is BrownSquare)
                            {
                                board.OnInteraction(board.squares[i][j] as BrownSquare);
                                Program.NeedToRedraw = true;
                            }
                        }
                    }
                }
            }
            //Debug.WriteLine(r.dwMousePosition.X + " " + r.dwMousePosition.Y);
            if (r.dwMousePosition.X >= 0 && r.dwMousePosition.X <= (0 + 10) && r.dwMousePosition.Y == 26 
                && r.dwButtonState == NativeMethods.MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
            {
                board.AcceptMove();
                Program.NeedToRedraw = true;
            }
            if (r.dwMousePosition.X >= 38 && r.dwMousePosition.X <= (38 + 9) && r.dwMousePosition.Y == 26
                && r.dwButtonState == NativeMethods.MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
            {
                board.ResetMove();
                Program.NeedToRedraw = true;
            }
        }

        public static void Update(this MenuState menuState)
        {
            ConsoleKeyInfo c = Console.ReadKey();
            //Arrow down
            if (c.Key == ConsoleKey.DownArrow)
            {
                if (menuState.index == menuState.options.Count - 1)
                {
                    menuState.index = 0;
                }
                else { menuState.index++; }
                Program.NeedToRedraw = true;
            }
            //Arrow up
            else if (c.Key == ConsoleKey.UpArrow)
            {
                if (menuState.index <= 0)
                {
                    menuState.index = menuState.options.Count - 1;
                }
                else { menuState.index--; }
                Program.NeedToRedraw = true;
            }
            //Enter
            else if (c.Key == ConsoleKey.Enter)
            {
                Program.ChangeState(menuState.options[menuState.index].OptionState);
                Program.NeedToRedraw = true;
            }
        }

        public static void Update(this State State)
        {
            if(State is MenuState)
            {
                (State as MenuState).Update();
            }
            if (State is GameState)
            {
                (State as GameState).Update();
            }
        }

        public static void Update(this GameState pvp)
        {
            //ConsoleKeyInfo c = Console.ReadKey();
            ////A
            //if (c.Key == ConsoleKey.A)
            //    pvp.board.AcceptMove();
            //else if (c.Key == ConsoleKey.R)
            //    pvp.board.ResetMove();
        }
    }
}
