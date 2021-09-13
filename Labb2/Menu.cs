using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Menu
    {
        internal static void MainMenu(ref List<Customer> customers, ref int indexOfLoggedInUser)
        {
            var menuName = "Main Menu";
            var choices = new string[] { "Create New Account", "Log In", "Exit Shop" };
            int choice = GraphicMenu(menuName, choices);
            switch (choice)
            {
                case 0:
                    // Create New Account
                    Console.Write("Enter user name: ");
                    var customerNameNew = Console.ReadLine();
                    bool isNameTaken = false;
                    foreach (var customer in customers)
                        if (customer.Name == customerNameNew)
                        {
                            Console.WriteLine("User name already excists");
                            isNameTaken = true;
                            break;
                        }
                    if (isNameTaken)
                        break;
                    Console.Write("Enter password: ");
                    var customerPasswordNew = Console.ReadLine();
                    customers.Add(new Customer(customerNameNew, customerPasswordNew));
                    //indexOfLogggedInUser = customers.Count - 1;
                    break;
                case 1:
                    // Logg In
                    // Fixa så att man får försöka igen om man har skrivit in fel lösenord eller användare.
                    Console.Write("Enter user name: ");
                    var customerNameExisting = Console.ReadLine();
                    Console.Write("Enter password: ");
                    var customerPasswordExisting = Console.ReadLine();
                    bool isNameFound = false;
                    for (int i = 0; i < customers.Count; i++)
                    {
                        if (customerNameExisting == customers[i].Name)
                        {
                            isNameFound = true;
                            if (customerPasswordExisting == customers[i].Password)
                            {
                                indexOfLoggedInUser = i;
                                break;
                            }

                            else
                            {
                                Console.WriteLine("User name or password is incorect. Press enter to continue...");
                                Console.ReadLine();
                                break;
                            }
                        }
                    }
                    if (!isNameFound)
                    {
                        Console.WriteLine("User name or password is incorect. Press enter to continue...");
                        Console.ReadLine();
                    }
                    break;
                case 2:
                    // Exit Shop
                    Environment.Exit(0);
                    break;
            }
        }

        internal static void LoggedInMenu(ref List<Customer> customer, ref int indexOfLoggedInUser)
        {
            var menuName = "Main Menu";
            var choices = new string[] { "Shop", "View Cart", "Check Out", "Logg Out" };
            int choice = GraphicMenu(menuName, choices);

            switch (choice)
            {
                case 0:
                    Shop(ref customer, ref indexOfLoggedInUser);
                    break;
                case 1:
                    ViewCart(ref customer, ref indexOfLoggedInUser);
                    break;
                default:
                    break;
            }
        }

        internal static void Shop(ref List<Customer> customers, ref int indexOfLoggedInUser)
        {
            // Implementera
            Console.Write($"{customers[indexOfLoggedInUser].Name} is logged in. Press enter to logg out...");
            Console.ReadLine();
            indexOfLoggedInUser = -1;
        }

        internal static void ViewCart(ref List<Customer> customers, ref int indexOfLoggedInUser)
        {
            // Implementera
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
