using System;
using System.Collections.Generic;

namespace Labb2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Gör eventuellt om ref till return av tuple eller returnera användare... Få bort ref!!!
            var customers = new List<Customer>();
            Customer loggedInCustomer = null;
            int indexOfLoggedInUser = -1;
            customers.Add(new BronzeCustomer("Knatte", "123"));
            customers.Add(new SilverCustomer("Fnatte", "321"));
            customers.Add(new GoldCustomer("Tjatte", "213"));

            //loggedInCustomer = customers[0];
            //loggedInCustomer.AddToChart(new Item());
            //loggedInCustomer = null;
            //Console.WriteLine(customers[0].Chart[0]);
            //Console.ReadLine();

            while (true)
            {
                if (indexOfLoggedInUser == -1)
                    Menu.MainMenu(ref customers, ref indexOfLoggedInUser);

                else
                    Menu.LoggedInMenu(ref customers, ref indexOfLoggedInUser);
            }
        }
    }
}
