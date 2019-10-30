using System;
using CheckersLogic.States;
using CheckersTerminal;

namespace Checkers.Update
{
    public static class UpdateMethods
    {
       public static void Update(this MenuState menuState)
        {

            ConsoleKeyInfo ckey = Console.ReadKey();

            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (menuState.index == menuState.options.Count - 1)
                {
                    menuState.index = 0;
                }
                else { menuState.index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (menuState.index <= 0)
                {
                    menuState.index = menuState.options.Count - 1;
                }
                else { menuState.index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                Program.ChangeState(menuState.options[menuState.index].OptionState);
            }
        }

        public static void Update(this State State)
        {
            if(State is MenuState)
            {
                (State as MenuState).Update();
            }
            if (State is PlayerVsPlayer)
            {
                (State as PlayerVsPlayer).Update();
            }
        }

        public static void Update(this PlayerVsPlayer pvp)
        {
            Console.WriteLine("PVP");
        }
    }
}
