using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using CheckersLogic;
using CheckersLogic.States;
using CheckersTerminal;
using NAudio.Wave;
using static CheckersTerminal.ConsoleListener;

namespace CheckersTerminal.Update
{
    public static class UpdateMethods
    {
        public static ConsoleMouseEvent boardHandling;
        /// <summary>
        /// Add mouse events
        /// </summary>
        /// <param name="s"></param>
        public static void Load(this State s)
        {
            if (s is PlayerVsPlayer)
            {
                (s as PlayerVsPlayer).Load();
            }
            else if (s is PlayerVsComputer)
            {
                (s as PlayerVsComputer).Load();
            }
            else if (s is MenuState)
                (s as MenuState).Load();
        }
        
        public static void Unload(this State s)
        {
            if (s is PlayerVsPlayer)
            {
                (s as PlayerVsPlayer).Unload();
            }
            else if (s is PlayerVsComputer)
            {
                (s as PlayerVsComputer).Unload();
            }
            else if (s is MenuState)
                (s as MenuState).Unload();
        }

        public static void Load(this PlayerVsPlayer gameState)
        {
            boardHandling = new ConsoleMouseEvent((r) => BoardMouseHandling(gameState.board, r, 3, 2));
            ConsoleListener.MouseEvent += boardHandling;
            gameState.board.OnWrongMove += PlayWrongMoveSound;
            StartBackgroundSong();
        }

        public static void Load(this PlayerVsComputer gameState)
        {
            boardHandling = new ConsoleMouseEvent((r) => ComputerBoardMouseHandling(gameState.board, r, 3, 2));
            ConsoleListener.MouseEvent += boardHandling;
            gameState.board.OnWrongMove += PlayWrongMoveSound;
            StartBackgroundSong();
        }

        private static void StartBackgroundSong()
        {
            new Thread(() =>
            {
                using (var audioFile = new AudioFileReader(System.AppDomain.CurrentDomain.BaseDirectory + "PlayTheme.wav"))
                {
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState == PlaybackState.Playing && Program.currentState is GameState)
                        {
                            Thread.Sleep(50);
                        }
                    }
                }
            }).Start();
        }

        private static void PlayWrongMoveSound(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                using (var audioFile = new AudioFileReader(System.AppDomain.CurrentDomain.BaseDirectory + "WrongMove.wav"))
                {
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState == PlaybackState.Playing && Program.currentState is GameState)
                        {
                            Thread.Sleep(50);
                        }
                    }
                }
            }).Start();
        }

        public static void Load(this MenuState menuState)
        {
            ConsoleListener.MouseEvent += MenuHandling;
            StartMainMenuSong();
        }

        private static void StartMainMenuSong()
        {
            new Thread(() =>
            {
                using (var audioFile = new AudioFileReader(System.AppDomain.CurrentDomain.BaseDirectory + "MainMenu.wav"))
                {
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState == PlaybackState.Playing && Program.currentState is MenuState)
                        {
                            Thread.Sleep(50);
                        }
                    }
                }
            }).Start();
        }

        public static void Unload(this PlayerVsPlayer gameState)
        {
            //ConsoleListener.MouseEvent -= (r) => BoardMouseHandling(gameState.board, r, 0, 2);
            ConsoleListener.MouseEvent -= boardHandling;
        }

        public static void Unload(this PlayerVsComputer playerVsComputer)
        {
            ConsoleListener.MouseEvent -= boardHandling; 
        }

        public static void Unload(this MenuState menuState)
        {
            ConsoleListener.MouseEvent -= MenuHandling;
        }

        private static void MenuHandling(NativeMethods.MOUSE_EVENT_RECORD r)
        {
            if (r.dwMousePosition.X >= 6 && r.dwMousePosition.X <= (6 + 17) && r.dwMousePosition.Y == 11
                && r.dwButtonState == NativeMethods.MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
            {
                Program.ChangeState(new PlayerVsPlayer());
                Program.NeedToRedraw = true;
            }
            else if(r.dwMousePosition.X >= 6 && r.dwMousePosition.X <= (6 + 19) && r.dwMousePosition.Y == 14
                && r.dwButtonState == NativeMethods.MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
            {
                Program.ChangeState(new PlayerVsComputer());
                Program.NeedToRedraw = true;
            }
        }

        private static void BoardMouseHandling(Board board, NativeMethods.MOUSE_EVENT_RECORD r, int startIndexX, int startIndexY)
        {
            MoveMouseHandling(board, r, startIndexX, startIndexY);
        }

        private static void MoveMouseHandling(Board board, NativeMethods.MOUSE_EVENT_RECORD r, int startIndexX, int startIndexY)
        {
            Debug.WriteLine("R: " + r.dwMousePosition.X + " " + r.dwMousePosition.Y);
            if (r.dwButtonState == NativeMethods.MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
            {
                for (int i = 0; i < board.squares.Length; i++)
                {
                    for (int j = 0; j < board.squares[i].Length; j++)
                    {
                        //if (r.dwMousePosition.X >= (startIndexX + j * 6) && r.dwMousePosition.X <= (startIndexX + j * 6) + 6
                        //&& r.dwMousePosition.Y >= (startIndexY + i * 3) && r.dwMousePosition.Y <= (startIndexY + i * 3) + 3)
                        if (r.dwMousePosition.X >= (startIndexX + j * 6) && r.dwMousePosition.X < (startIndexX + j * 6) + 6
                        && r.dwMousePosition.Y >= (startIndexY + i * 3) && r.dwMousePosition.Y < (startIndexY + i * 3) + 3)
                        {
                            if (r.dwButtonState == NativeMethods.MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
                            {
                                if (board.squares[j][i] is BrownSquare)
                                {
                                    board.OnInteraction(board.squares[j][i] as BrownSquare);
                                    Debug.WriteLine("j: " + j + " i: " + i);
                                    Program.NeedToRedraw = true;
                                }
                                return;
                            }
                        }
                    }
                }
                if (r.dwMousePosition.X >= 0 && r.dwMousePosition.X < (0 + 20) && r.dwMousePosition.Y >= 26 && r.dwMousePosition.Y < 31
                    && r.dwButtonState == NativeMethods.MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
                {
                    board.AcceptMove();
                    Program.NeedToRedraw = true;
                }
                else if (r.dwMousePosition.X >= 21 && r.dwMousePosition.X < (21 + 20) && r.dwMousePosition.Y >= 26 && r.dwMousePosition.Y < 31
                    && r.dwButtonState == NativeMethods.MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
                {
                    board.ResetMove();
                    Program.NeedToRedraw = true;
                }
                else if (r.dwMousePosition.X >= 39 && r.dwMousePosition.X < (39 + 20) && r.dwMousePosition.Y >= 26 && r.dwMousePosition.Y < 31
                    && r.dwButtonState == NativeMethods.MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
                {
                    Program.ChangeState(new MenuState());
                    Program.NeedToRedraw = true;
                }
            }
        }

        private static void ComputerBoardMouseHandling(Board board, NativeMethods.MOUSE_EVENT_RECORD r, int startIndexX, int startIndexY)
        {
            if(board.IsWhiteTurn)
            {
                MoveMouseHandling(board, r, startIndexX, startIndexY);
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                //Ruch komputera
                RandomComputerAgent agent = new RandomComputerAgent(board);
                // Wyszukanie najlepszego rozwiązania
                (Pawn, List<BrownSquare>) move = agent.SearchForBestMove();
                board.SetSelectedSquareAsStart((BrownSquare)move.Item1.Position);
                board.selectedSquaresToEnd = move.Item2;

                board.AcceptMove();
                Program.NeedToRedraw = true;
            }
        }

        public static void Update(this State State)
        {
            if(State is GameState)
            {
                if((State as GameState).board.GameFinished)
                {
                    Program.ChangeState(new EndGame((State as GameState).board.WhiteWon));
                    Program.NeedToRedraw = true;
                }
            }
        }
    }
}
