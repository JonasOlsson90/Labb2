using System;
using System.Collections.Generic;

namespace Labb2
{
    class Program
    {
        //ToDo Implementera filläsning och filskrivning
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            List<Customer> customers = IO.ReadFromFile();

            while (true)
            {
                if (Customer.indexOfLoggedInUser == -1)
                    Menu.MainMenu(customers);
                else
                    Menu.LoggedInMenu(customers);
            }
        }
    }
}
