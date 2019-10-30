using System;
using System.Text;
using CheckersLogic.States;

namespace Checkers.Draw
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
                Console.OutputEncoding = System.Text.Encoding.UTF8;
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
        }
    }

}
           
    
  