using System;
using System.Collections.Generic;

namespace Labb2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lös det här med listan. Elementen måste ha olika namn för att man ska kunna avgöra om de redan finns där.
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
