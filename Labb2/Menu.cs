using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Menu
    {
        internal static Customer MainMenu(List<Customer> customers)
        {
            var choices = new string[] { "Create New Account", "Log In", "Exit Shop" };
            var menuName = "Main Menu";
            while (true)
            {
                int choice = GraphicMenu(menuName, choices);
                switch (choice)
                {
                    case 0:
                        Console.Write("Enter user name: ");
                        var userNameNew = Console.ReadLine();
                        Console.Write("Enter password: ");
                        var userPasswordNew = Console.ReadLine();
                        return new Customer(userNameNew, userPasswordNew);
                    case 1:
                        // Logg In
                        Console.Write("Enter user name: ");
                        var userNameExisting = Console.ReadLine();
                        Console.Write("Enter password: ");
                        var userPasswordExisting = Console.ReadLine();
                        foreach (var customer in customers)
                        {
                            if (userNameExisting == customer.Name)
                            {
                                if (userPasswordExisting == customer.Password)
                                    return customer;
                                else
                                {
                                    Console.WriteLine("User name or password is incorect. Press enter to continue...");
                                    Console.ReadLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("User name or password is incorect. Press enter to continue...");
                                Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }



        private static int GraphicMenu(string menuName, string[] choices)
        {
            var choice = 0;
            var line = new string('-', menuName.Length + 2);
            while (true)
            {
                Console.Clear();
                Console.WriteLine(menuName);
                Console.WriteLine(line);

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
                        Console.Clear();
                        return choice;
                    default:
                        break;
                }
            }
        }
    }
}
