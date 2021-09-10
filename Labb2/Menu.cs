using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Menu
    {
        internal static void MainMenu()
        {
            var choices = new string[] { "Create New Account", "Log In", "Log Out", "Products", "Exit Shop" };
            var choice = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("================");

                for (int i = 0; i < choices.Length; i++)
                {
                    if (i == choice)
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(choices[i]);
                    if (i == choice)
                        Console.ResetColor();
                }

                var keyPresss = Console.ReadKey().Key;

                switch (keyPresss)
                {
                    case ConsoleKey.DownArrow:
                        choice = (choice + 1) % choices.Length;
                        break;
                    case ConsoleKey.UpArrow:
                        choice = choice == 0 ? choices.Length - 1 : choice - 1;
                        break;
                    case ConsoleKey.Enter:
                        GoToChoice(choice);
                        break;
                    default:
                        break;
                }
            }
        }
        private static void GoToChoice(int choice)
        {
            switch (choice)
            {
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
