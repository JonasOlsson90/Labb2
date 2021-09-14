using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Menu
    {
        internal static void MainMenu(List<Customer> customers)
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
                            Console.Write("User name already excists. Press any key to continue...");
                            Console.ReadLine();
                            isNameTaken = true;
                            break;
                        }
                    if (isNameTaken)
                        break;
                    Console.Write("Enter password: ");
                    var customerPasswordNew = Console.ReadLine();
                    customers.Add(new Customer(customerNameNew, customerPasswordNew));
                    Program.indexOfLoggedInUser = customers.Count - 1;
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
                                Program.indexOfLoggedInUser = i;
                                break;
                            }

                            else
                            {
                                Console.Write("User name or password is incorect. Press enter to continue...");
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

        internal static void LoggedInMenu(List<Customer> customers)
        {
            // Lägg till change currency
            var menuName = "Main Menu";
            var choices = new string[] { "Shop", "View Cart", "Check Out", "Logg Out" };
            int choice = GraphicMenu(menuName, choices);

            switch (choice)
            {
                case 0:
                    Shop(customers);
                    break;
                case 1:
                    ViewCart(customers);
                    break;
                case 2:
                    CheckOut(customers);
                    break;
                case 3:
                    LoggOut();
                    break;
                default:
                    break;
            }
        }

        internal static void Shop(List<Customer> customers)
        {
            var menuName = "Shop";
            var items = new Item[]
            {
                new Item("Potion of Healing", 400),
                new Item("Potion of Climbing", 400),
                new Item("Potion of Animal Friendship", 1600),
                new Item("Potion of Supreme Healing", 16000)
            };

            var choices = new string[items.Length + 1];
            for (int i = 0; i < items.Length; i++)
            {
                // Fixa valutor
                string temp = $"{items[i].Name}\n{items[i].Price} {customers[Program.indexOfLoggedInUser].PreferedCurrency}";
                choices[i] = temp;
            }
            choices[choices.Length - 1] = "Go Back";

            while (true)
            {
                int choice = GraphicMenu(menuName, choices);

                if (choice < items.Length && choice >= 0)
                {
                    // Fixa så att man kan lägga till antal och kolla om det redan finns i varukorgen! 
                    customers[Program.indexOfLoggedInUser].AddToChart(items[choice]);
                    Console.Clear();
                    Console.Write($"{items[choice].Name} Has been added to your chart!\nPress enter to continue shoping...");
                    Console.ReadLine();
                }

                else
                    break;
            }
        }

        internal static void ViewCart(List<Customer> customers)
        {
            // Implementera
        }

        internal static void CheckOut(List<Customer> customers)
        {
            // Implementera
        }

        internal static void LoggOut()
        {
            // Implementera
            var menuName = "Are You Sure?";
            var choices = new string[] { "No", "Yes" };
            int choice = GraphicMenu(menuName, choices);

            switch (choice)
            {
                case 0:
                    // No
                    break;
                case 1:
                    // Yes
                    // Spara ner allt till fil.
                    Program.indexOfLoggedInUser = -1;
                    break;
                default:
                    break;
            }
        }

        private static int GraphicMenu(string menuName, string[] choices)
        {
            var choice = 0;
            var line = new string('-', menuName.Length + 2);
            Console.Clear();
            while (true)
            {
                Console.SetCursorPosition(0, 0);
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
