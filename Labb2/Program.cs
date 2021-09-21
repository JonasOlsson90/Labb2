using System;
using System.Collections.Generic;

namespace Labb2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            // Filen skapas med tre fördefinierade kunder enligt beskrivningen om den inte redan finns. Se IO.cs.
            List<Customer> customers = IO.ReadFromFile();

            // När man startar programmet är indexIfLoggedInUser alltid -1, vilket betyder att ingen är inloggad.
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
