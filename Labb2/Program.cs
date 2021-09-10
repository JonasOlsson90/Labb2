using System;
using System.Collections.Generic;

namespace Labb2
{
    class Program
    {
        static void Main(string[] args)
        {
            var customers = new List<Customer>();
            Customer loggedInCustomer;
            while (true)
            {
                loggedInCustomer = Menu.MainMenu(customers);
                if (!customers.Contains(loggedInCustomer))
                    customers.Add(loggedInCustomer);
                Console.WriteLine($"{loggedInCustomer.Name} is logged in");
            }
        }
    }
}
