using System;
using System.Collections.Generic;

namespace Labb2
{
    class Program
    {
        internal static int indexOfLoggedInUser = -1;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var customers = new List<Customer>
            {
                new BronzeCustomer("Knatte", "123"),
                new SilverCustomer("Fnatte", "321"),
                new GoldCustomer("Tjatte", "213")
            };

            while (true)
            {
                if (indexOfLoggedInUser == -1)
                    Menu.MainMenu(customers);
                else
                    Menu.LoggedInMenu(customers);
            }
        }
    }
}
