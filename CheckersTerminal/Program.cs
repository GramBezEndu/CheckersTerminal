using System;
using System.Collections.Generic;
using CheckersTerminal.Draw;
using CheckersTerminal.Update;
using CheckersLogic.States;

namespace CheckersTerminal
{
    class Program
    {
        static State currentState;
        static State nextState;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            currentState = new MenuState();
            while (true)
            {
                if (nextState != null)
                {
                    currentState = nextState;
                    nextState = null;
                }
                currentState.Draw();
                currentState.Update();
            }
        }

        public static void ChangeState(State newState)
        {
            nextState = newState;
        }
    }
}