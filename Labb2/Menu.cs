using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Menu
    {
        //ToDo Implementera filläsning och filskrivning
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
                    LogIn(customers);
                    break;
                case 2:
                    // Exit Shop
                    Console.CursorVisible = true;
                    Environment.Exit(0);
                    break;
            }
        }

        internal static void LoggedInMenu(List<Customer> customers)
        {
            var menuName = "Main Menu";
            var choices = new string[] { "Shop", "View Cart", "Change Currency", "Check Out", customers[Customer.indexOfLoggedInUser].Name, "Logg Out" };
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
                    if (customers[Customer.indexOfLoggedInUser].Cart.Count == 0)
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
                    Console.WriteLine(customers[Customer.indexOfLoggedInUser].ToString());
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
            if (string.IsNullOrEmpty(customerNameNew))
            {
                Console.Write("You have to enter a user name. Press enter to continue...");
                Console.ReadLine();
                return;
            }
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
            //ToDo Förebygg tomt lösenord
            Console.Write("Enter password: ");
            var customerPasswordNew = Console.ReadLine();
            customers.Add(new Customer(customerNameNew, customerPasswordNew));
            IO.WriteToFile(customers[customers.Count - 1]);
            Customer.indexOfLoggedInUser = customers.Count - 1;
        }

        private static void LogIn(List<Customer> customers)
        {
            // Till Niklas:
            // Jag har medvetet valt att inte särskilja om lösenordet eller användarnamnet är felaktigt.
            // Detta är för att jag har förstått att det anses vara lite av en säkerhetsrisk på hemsidor då man lätt kan få reda på om ett konto existerar och börja prova lösenord.
            // Vi vill ju inte att stackars Knatte ska hamna hos kronofogden för att björnligan har luskat ut hans lösenord!
            // Jag har däremot valt att separera dem i koden för att visa att jag skulle klara av att göra på det andra sättet.
            // Säg till om du vill att jag ska ändra så att jag följer beskrivningen till fullo. Jag pillar gärna vidare med koden om det skulle vara så.
            var menuName = "User name or password is incorect. Do you wish to try again or register a new user?";
            var choices = new string[] { "Try Again","Regester new user", "Go Back To Main Menu" };
            var choice = -1;
            Console.Write("Enter user name: ");
            var customerNameLogIn = Console.ReadLine();
            Console.Write("Enter password: ");
            var customerPasswordLogIn = Console.ReadLine();
            var isNameFound = false;
            for (int i = 0; i < customers.Count; i++)
            {
                if (customerNameLogIn == customers[i].Name)
                {
                    isNameFound = true;
                    if (customers[i].ValidatePassword(customerPasswordLogIn))
                    {
                        Customer.indexOfLoggedInUser = i;
                        return;
                    }
                    else
                        choice = GraphicMenu(menuName, choices);
                }
            }
            if (!isNameFound)
                choice = GraphicMenu(menuName, choices);
            switch (choice)
            {
                case 0:
                    // Try Again
                    LogIn(customers);
                    break;
                case 1:
                    // Regester New User
                    CreateNewAcount(customers);
                    break;
                case 2:
                    // Go Back To Main Menu
                    break;
                default:
                    break;
            }
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
                choices[i] = $"{items[i].Name}\n{customers[Customer.indexOfLoggedInUser].ConvertPrice(items[i].Price)} {customers[Customer.indexOfLoggedInUser].PreferedCurrency}";
            choices[^1] = "Go Back";

            while (true)
            {
                int choice = GraphicMenu(menuName, choices);

                if (choice < items.Length && choice >= 0)
                    customers[Customer.indexOfLoggedInUser].AddToCart(items[choice]);
                else
                    break;
            }
        }

        private static void ViewCart(List<Customer> customers) 
        {
            var totalSumOfCart = customers[Customer.indexOfLoggedInUser].CalculateTotalSumOfCart();
            Console.Clear();
            Console.WriteLine(customers[Customer.indexOfLoggedInUser].Name);
            Console.WriteLine(new string('-', customers[Customer.indexOfLoggedInUser].Name.Length + 2));
            Console.WriteLine("Cart:\n");
            foreach (var item in customers[Customer.indexOfLoggedInUser].Cart)
            {
                Console.WriteLine($"{item.Name}");
                Console.WriteLine($"Price: {customers[Customer.indexOfLoggedInUser].ConvertPrice(item.Price)} {customers[Customer.indexOfLoggedInUser].PreferedCurrency}");
                Console.WriteLine($"Qty {item.Amount}");
                Console.WriteLine($"Total: {customers[Customer.indexOfLoggedInUser].ConvertPrice(item.Price * item.Amount)} {customers[Customer.indexOfLoggedInUser].PreferedCurrency}\n");
            }
            Console.WriteLine($"Total sum: {totalSumOfCart} {customers[Customer.indexOfLoggedInUser].PreferedCurrency} (VAT charges may apply)\n");

            if (customers[Customer.indexOfLoggedInUser] is IDiscount discountableCustomer)
            {
                totalSumOfCart = Math.Round(discountableCustomer.AddDiscount(totalSumOfCart), 2);
                Console.WriteLine($"Your price: {totalSumOfCart} {customers[Customer.indexOfLoggedInUser].PreferedCurrency} (VAT charges may apply)\n");
            }
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
            string cardNumber = Console.ReadLine().Trim();
            Console.Write("Enter expiration date (4 digits): ");
            string cardExpirationDate = Console.ReadLine().Trim();
            Console.Write("Enter name of Cardholder: ");
            string cardholder = Console.ReadLine();
            Console.Write("Enter card CVC (3 digits): ");
            string cardCVC = Console.ReadLine().Trim();

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
            var totalSumOfCart = customers[Customer.indexOfLoggedInUser].CalculateTotalSumOfCart();

            if (customers[Customer.indexOfLoggedInUser] is IDiscount discountableCustomer)
                totalSumOfCart = Math.Round(discountableCustomer.AddDiscount(totalSumOfCart), 2);

            Console.Clear();
            Console.WriteLine($"Send an owl with {totalSumOfCart} {customers[Customer.indexOfLoggedInUser].PreferedCurrency} to:");
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
            Console.WriteLine("The moose will magically know exactly were you are and will arrive within 1-5 business days.\n\n");
            Console.WriteLine("Delivery by DHÄlg®\n\n\n");
            customers[Customer.indexOfLoggedInUser].Cart.Clear();
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
                    //ToDo Spara ner allt till fil.
                    Customer.indexOfLoggedInUser = -1;
                    break;
                default:
                    break;
            }
        }

        private static void ChangeCurrency(List<Customer> customers)
        {
            var menuName = "Change Currency";
            var choices = customers[Customer.indexOfLoggedInUser].CurrencyNameValue.Keys.ToArray();
            int choice = GraphicMenu(menuName, choices);
            customers[Customer.indexOfLoggedInUser].ChangeCurrency(choices[choice]);
        }

        private static int GraphicMenu(string menuName, string[] choices)
        {
            var choice = 0;
            var line = new string('-', menuName.Length + 2);
            Console.Clear();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(menuName);
                Console.WriteLine(line);

                for (int i = 0; i < choices.Length; i++)
                {
                    if (i == choice)
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(choices[i]);
                    if (i == choice)
                        Console.ForegroundColor = ConsoleColor.Green;
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
                        Console.ResetColor();
                        return choice;
                    default:
                        break;
                }
            }
        }
    }
}
