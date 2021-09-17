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
                    CreateNewAcount(customers);
                    break;
                case 1:
                    // Logg In
                    while (!LogIn(customers)) { }
                    break;
                case 2:
                    // Exit Shop
                    Environment.Exit(0);
                    break;
            }
        }

        internal static void LoggedInMenu(List<Customer> customers)
        {
            var menuName = "Main Menu";
            var choices = new string[] { "Shop", "View Cart", "Change Currency", "Check Out", customers[Program.indexOfLoggedInUser].Name, "Logg Out" };
            int choice = GraphicMenu(menuName, choices);

            switch (choice)
            {
                case 0:
                    Shop(customers);
                    break;
                case 1:
                    ViewCart(customers);
                    Console.Write("Press enter to continue shopping...");
                    Console.ReadLine();
                    break;
                case 2:
                    ChangeCurrency(customers);
                    break;
                case 3:
                    // Check Out
                    if (customers[Program.indexOfLoggedInUser].Cart.Count == 0)
                    {
                        Console.Write("Cart is empty. Press enter to continue shopping...");
                        Console.ReadLine();
                        break;
                    }
                    ViewCart(customers);
                    Console.Write("Press enter to go to payment methods...");
                    Console.ReadLine();
                    CheckOut(customers);
                    break;
                case 4:
                    // Customer info (visas endast som namnet på den inloggade kunden i menyn).
                    Console.WriteLine(customers[Program.indexOfLoggedInUser].ToString());
                    Console.WriteLine();
                    Console.Write("Press enter to go back...");
                    Console.ReadLine();
                    break;
                case 5:
                    LoggOut();
                    break;
                default:
                    break;
            }
        }

        private static void CreateNewAcount(List<Customer> customers)
        {
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
                return;
            Console.Write("Enter password: ");
            var customerPasswordNew = Console.ReadLine();
            customers.Add(new Customer(customerNameNew, customerPasswordNew));
            Program.indexOfLoggedInUser = customers.Count - 1;
        }

        private static bool LogIn(List<Customer> customers)
        {
            var menuName = "User name or password is incorect. Do you wish to try again?";
            var choices = new string[] { "Yes", "No" };
            Console.Write("Enter user name: ");
            var customerNameExisting = Console.ReadLine();
            Console.Write("Enter password: ");
            var customerPasswordExisting = Console.ReadLine();
            //bool isNameFound = false;
            for (int i = 0; i < customers.Count; i++)
            {
                if (customerNameExisting == customers[i].Name)
                {
                    //isNameFound = true;
                    if (customerPasswordExisting == customers[i].Password)
                    {
                        Program.indexOfLoggedInUser = i;
                        return true;
                    }

                    else
                        return GraphicMenu(menuName, choices) == 1;
                }
            }
            return GraphicMenu(menuName, choices) == 1;
        }

        private static void Shop(List<Customer> customers)
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
                choices[i] = $"{items[i].Name}\n{ConvertPrice(customers, items[i].Price)} {customers[Program.indexOfLoggedInUser].PreferedCurrency}";
            choices[^1] = "Go Back";

            while (true)
            {
                int choice = GraphicMenu(menuName, choices);

                if (choice < items.Length && choice >= 0)
                    customers[Program.indexOfLoggedInUser].AddToCart(items[choice]);
                else
                    break;
            }
        }

        //ToDO Implementera IDIscount
        private static void ViewCart(List<Customer> customers)
        {
            var totalSumOfCart = 0.0D;
            Console.Clear();
            Console.WriteLine(customers[Program.indexOfLoggedInUser].Name);
            Console.WriteLine(new string('-', customers[Program.indexOfLoggedInUser].Name.Length + 2));
            Console.WriteLine("Cart:\n");
            foreach (var item in customers[Program.indexOfLoggedInUser].Cart)
            {
                Console.WriteLine($"{item.Name}");
                Console.WriteLine($"Price: {ConvertPrice(customers, item.Price)} {customers[Program.indexOfLoggedInUser].PreferedCurrency}");
                Console.WriteLine($"Qty {item.Amount}");
                Console.WriteLine($"Total: {ConvertPrice(customers, item.Price) * item.Amount} {customers[Program.indexOfLoggedInUser].PreferedCurrency}\n");
                totalSumOfCart += ConvertPrice(customers, item.Price) * item.Amount;
            }
            Console.WriteLine($"Total sum: {Math.Round(totalSumOfCart, 2)} {customers[Program.indexOfLoggedInUser].PreferedCurrency} (VAT charges may apply)\n");

            if (customers[Program.indexOfLoggedInUser] is IDiscount discountableCustomer)
                totalSumOfCart = Math.Round(discountableCustomer.AddDiscount(totalSumOfCart), 2);

            Console.WriteLine($"Your price: {totalSumOfCart} {customers[Program.indexOfLoggedInUser].PreferedCurrency}\n");
        }

        private static void CheckOut(List<Customer> customers)
        {
            var menuName = "Payment Methods";
            var choices = new string[] { "Visa/Mastercard", "Pay by Owl", "Continue shopping" };
            int choice = GraphicMenu(menuName, choices);
            switch (choice)
            {
                case 0:
                    // Visa/Mastecard
                    if (ValidateVisa())
                        PaymentSuccessfull(customers);
                    else
                    {
                        Console.Write("No valid Visa/Mastercard. Press enter to go back to payment methods...");
                        Console.ReadLine();
                        CheckOut(customers);
                    }                    
                    break;
                case 1:
                    PayByOwl(customers);
                    break;
                case 2:
                    // Continue shopping
                    break;
                default:
                    break;
            }
        }

        private static bool ValidateVisa()
        {
            Console.Clear();
            Console.Write("Enter card number (16 digits): ");
            string cardNumber = Console.ReadLine();
            Console.Write("Enter expiration date (4 digits): ");
            string cardExpirationDate = Console.ReadLine();
            Console.Write("Enter name of Cardholder: ");
            string cardholder = Console.ReadLine();
            Console.Write("Enter card CVC (3 digits): ");
            string cardCVC = Console.ReadLine();

            if (cardNumber.Length != 16 || cardExpirationDate.Length != 4 || cardCVC.Length != 3)
                return false;
            foreach (var digit in cardNumber)
                if (!char.IsDigit(digit))
                    return false;
            foreach (var digit in cardExpirationDate)
                if (!char.IsDigit(digit))
                    return false;
            foreach (var digit in cardCVC)
                if (!char.IsDigit(digit))
                    return false;
            if (string.IsNullOrEmpty(cardholder))
                return false;

            return true;
        }

        private static void PayByOwl(List<Customer> customers)
        {
            var totalSumOfCart = ConvertPrice(customers, customers[Program.indexOfLoggedInUser].Cart.Sum(item => item.Price * item.Amount));

            if (customers[Program.indexOfLoggedInUser] is IDiscount discountableCustomer)
                totalSumOfCart = Math.Round(discountableCustomer.AddDiscount(totalSumOfCart), 2);

            Console.Clear();
            Console.WriteLine($"Send an owl with {totalSumOfCart} {customers[Program.indexOfLoggedInUser].PreferedCurrency} to:");
            Console.WriteLine("Cloudberry Path 7");
            Console.WriteLine("Brugmansia Forest 777 77");
            Console.WriteLine("Feywild");
            Console.WriteLine();
            Console.WriteLine("The delivery moose will know if payment is on its way or not.");
            Console.WriteLine("Our advice is not to try any tricks, but it's down to you.");
            Console.Write("Press enter to continue...");
            Console.ReadLine();
            PaymentSuccessfull(customers);
        }

        private static void PaymentSuccessfull(List<Customer> customers)
        {
            Console.Clear();
            Console.WriteLine("Payment successfull!");
            Console.WriteLine("Your order will be delivered by moose.");
            Console.WriteLine("The moose will magically know exactly were you are and will arrive within 1-5 business days.");
            customers[Program.indexOfLoggedInUser].Cart.Clear();
            Console.Write("Press enter to go back to store...");
            Console.ReadLine();
        }

        private static void LoggOut()
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

        private static void ChangeCurrency(List<Customer> customers)
        {
            var menuName = "Change Currency";
            var choices = customers[Program.indexOfLoggedInUser].CurrencyNameValue.Keys.ToArray();
            int choice = GraphicMenu(menuName, choices);
            customers[Program.indexOfLoggedInUser].ChangeCurrency(choices[choice]);
        }

        private static double ConvertPrice(List<Customer> customers, double price)
        {
            return Math.Round(price / customers[Program.indexOfLoggedInUser].CurrencyNameValue[(customers[Program.indexOfLoggedInUser].PreferedCurrency)], 2);
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
