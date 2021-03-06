using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb2
{
    class Menu
    {
        // Menyerna refererar ofta till metoden GraphicMenu(string menuName, string[] choices).
        // Denna metod returnerar en integer motsvarande det val användaren gjort.
        // Metoden ligger längst ner bland metoderna i denna fil.
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
                    LogIn(customers);
                    break;
                case 2:
                    // Exit Shop
                    Console.CursorVisible = true;
                    Console.ResetColor();
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
                    Console.Write("Press any key to continue shopping...");
                    Console.ReadKey(true);
                    break;
                case 2:
                    ChangeCurrency(customers);
                    break;
                case 3:
                    CheckOut(customers);
                    break;
                case 4:
                    // Customer info (visas endast som namnet på den inloggade kunden i menyn).
                    Console.WriteLine(customers[Customer.indexOfLoggedInUser].ToString());
                    Console.WriteLine();
                    Console.Write("Press any key to go back...");
                    Console.ReadKey(true);
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
            var customerNameNew = ValidateInput("Enter user name");
            // Strängen blir bara null om användaren tryckt på escape för att komma ur input
            if (string.IsNullOrEmpty(customerNameNew))
                return;

            foreach (var customer in customers)
                if (customer.Name == customerNameNew)
                {
                    Console.Write("User name already excists. Press any key to continue...");
                    Console.ReadKey(true);
                    CreateNewAcount(customers);
                    return;
                }

            var customerPasswordNew = ValidateInput("Enter password");

            // Strängen blir bara null om användaren tryckt på escape för att komma ur input
            if (string.IsNullOrEmpty(customerPasswordNew))
                return;

            customers.Add(new Customer(customerNameNew, customerPasswordNew));

            // Jag väljer att skriva den nyregistrerade kunden till filen direkt. Det har lite nackdelar,
            // men fördelen är att kunden finns kvar om något skulle hända om appen stängs oväntat.
            // De stärsta nackdelarna är att det är lite knepigare att spara kundens valuta och kundvagn,
            // så nu defaultar det bara till SEK och så får man ändra igen när man har loggat in och kundvagnen sparas inte.
            // Självklart sparas valutan och kundvagnen om man bara loggar ut och inte stänger appen.
            IO.WriteToFile(customers[^1]);

            // När man registrerat en ny användare loggas den nya användaren in.
            Customer.indexOfLoggedInUser = customers.Count - 1;
        }

        private static void LogIn(List<Customer> customers)
        {
            // Till Niklas:
            // Jag har medvetet valt att inte särskilja om lösenordet eller användarnamnet är felaktigt.
            // Detta är för att jag har förstått att det anses vara lite av en säkerhetsrisk på hemsidor
            // då man lätt kan få reda på om ett konto existerar och börja pröva lösenord.
            // Vi vill ju inte att stackars Knatte ska hamna hos kronofogden för att björnligan har luskat ut hans lösenord!
            // Jag har däremot valt att separera dem i koden för att visa att jag skulle klara av att göra på det andra sättet.
            // Säg till om du vill att jag ska ändra så att jag följer beskrivningen till fullo.
            // Jag pillar gärna vidare med koden om det skulle vara så.

            var menuName = "User name or password is incorect. Do you wish to try again or register a new user?";
            var choices = new string[] { "Try Again","Regester new user", "Go Back To Main Menu" };
            var choice = -1;
            var customerNameLogIn = ValidateInput("Enter user name");
            if (customerNameLogIn == null)
                return;
            var customerPasswordLogIn = ValidateInput("Enter password");
            if (customerPasswordLogIn == null)
                return;
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

            // Om man vill lägga till fler varor i affären är det bara att skriva in dem med pris i SEK här.
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
                    // Lägg till varan i kundens varukorg.
                    customers[Customer.indexOfLoggedInUser].AddToCart(items[choice]);
                else
                    // Go Back är det enda valet som inte motsvarar en vara, så då återgår man till huvudmenyn.
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

            if (customers[Customer.indexOfLoggedInUser].Cart.Count == 0)
                Console.WriteLine("Your cart is empty!\n");

            else
            {
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
        }

        private static void CheckOut(List<Customer> customers)
        {
            if (customers[Customer.indexOfLoggedInUser].Cart.Count == 0)
            {
                Console.Write("Cart is empty. Press any key to continue shopping...");
                Console.ReadKey(true);
                return;
            }

            ViewCart(customers);
            Console.Write("Press any key to go to payment methods...");
            Console.ReadKey(true);

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
                        Console.Write("No valid Visa/Mastercard. Press any key to go back to payment methods...");
                        Console.ReadKey(true);
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
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
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
            Console.Write("Press any key to go back to store...");
            Console.ReadKey(true);
        }

        private static void LoggOut()
        {
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
                    Customer.indexOfLoggedInUser = -1;
                    break;
                default:
                    break;
            }
        }

        private static void ChangeCurrency(List<Customer> customers)
        {
            var menuName = "Change Currency";
            var choices = Customer.CurrencyNameValue.Keys.ToArray();
            int choice = GraphicMenu(menuName, choices);
            customers[Customer.indexOfLoggedInUser].ChangeCurrency(choices[choice]);
        }

        private static string ValidateInput(string inputInfo)
        {
            Console.Clear();
            string input = "";
            ConsoleKeyInfo keyPress;
            do
            {
                Console.SetCursorPosition(0, 0);
                // Mellanslaget efter {input} gör att man kan radera input visuellt.
                Console.WriteLine($"{inputInfo}: {input} ");
                keyPress = Console.ReadKey(true);

                if (input.Length < 20)
                {
                    // Endast siffror och bokstäver kan skrivas in för att undvika till exempel kommatecken, vilket är en säkerhetsrisk.
                    if ((keyPress.KeyChar >= '0' && keyPress.KeyChar <= '9') ||
                        (keyPress.KeyChar >= 'A' && keyPress.KeyChar <= 'Z') ||
                        (keyPress.KeyChar >= 'a' && keyPress.KeyChar <= 'z'))
                        input += keyPress.KeyChar;
                }

                // Backspace raderar som den ska
                if (keyPress.Key == ConsoleKey.Backspace)
                    if (input.Length > 0)
                        input = input.Substring(0, input.Length - 1);

                // Gör det möjligt att ta sig ur input med escape. Detta returnerar null vilket man får hantera där man kallar på metoden.
                if (keyPress.Key == ConsoleKey.Escape)
                    return null;

            // Loopen går tills man har skrivit in något och trycker på enter om man inte brutit loopen med escape tidigare.
            } while (keyPress.Key != ConsoleKey.Enter || input.Length == 0);

            return input;
        }

        private static int GraphicMenu(string menuName, string[] choices)
        {
            var choice = 0;
            // Linjens längd ändras dynamisk efter längden på menyns namn.
            var line = new string('-', menuName.Length + 2);

            Console.Clear();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(menuName);
                Console.WriteLine(line);

                // Logik för att skriva ut menyn.
                // Det val användaren för närvarande står på skrivs ut i rött medan de andra valen skrivs ut i grönt.
                for (int i = 0; i < choices.Length; i++)
                {
                    if (i == choice)
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(choices[i]);
                    if (i == choice)
                        Console.ForegroundColor = ConsoleColor.Green;
                }

                // Sparar användarens knapptryck utan att skriva ut det.
                var keyPresss = Console.ReadKey(true).Key;

                // Logik för att hantera användarens input.
                // Endast tre knappar påverkar programmet visuellt och funktionellt: uppåtpil, neråtpil och enter.
                // Man kan endast växla mellan de val som skickats in i metoden.
                // Valet görs först när användaren trycker på enter.
                // Då returneras en integer som hanteras i metoden som kallade på denna metod.
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
