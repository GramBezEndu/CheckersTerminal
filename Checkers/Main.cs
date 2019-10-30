using System;
using System.Collections.Generic;
using Checkers.Draw;
using Checkers.Update;
using CheckersLogic.States;

namespace CheckersTerminal
{
    class Program
    {
        static State currentState;
        static State nextState;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            currentState = new MenuState();
            while (true)
            {
                if (nextState != null)
                {
                    currentState = nextState;
                    nextState = null;
                }
                currentState.Update();
                currentState.Draw();
            }
        }

        public static void ChangeState(State newState)
        {
            nextState = newState;
        }
    }
}








