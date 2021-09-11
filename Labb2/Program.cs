using System;
using System.Collections.Generic;

namespace Labb2
{
    class Program
    {
        static void Main(string[] args)
        {
            var customers = new List<Customer>();
            int indexOfLoggedInUser = -1;
            customers.Add(new Customer("Knatte", "123"));
            customers.Add(new Customer("Fnatte", "321"));
            customers.Add(new Customer("Tjatte", "213"));

            while (true)
            {
                if (indexOfLoggedInUser == -1)
                    Menu.MainMenu(ref customers, ref indexOfLoggedInUser);

                else
                    Menu.Shop(ref customers, ref indexOfLoggedInUser);
            }
        }
    }
}
