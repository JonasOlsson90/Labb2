using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Customer
    {
        public string Name { get; private protected set; }
        public string Password { get; private protected set; }
        public List<Item> Cart { get; private protected set; }
        // Fixa!
        public string PreferedCurrency { get; private protected set; }

        public Customer(string name, string password)
        {
            // Set name and password
            Name = name;
            Password = password;
            Cart = new List<Item>();
            PreferedCurrency = "SEK";
        }

        public void AddToCart(Item item)
        {
            // Add item to chart
            if (!Cart.Any(x => x.Name == item.Name))
            {
                Cart.Add(item);
                Console.Clear();
                Console.Write($"{item.Name} Has been added to your cart!\nPress enter to continue shoping...");
                Console.ReadLine();
            }
            else
            {
                for (int i = 0; i < Cart.Count; i++)
                    if (Cart[i].Name == item.Name)
                    {
                        Cart[i].ChangeAmount(1);
                        Console.WriteLine($"One more {Cart[i].Name} has been added to your cart.\nCart now contains {Cart[i].Amount} {Cart[i].Name}s!\nPress enter to continue shoping...");
                        Console.ReadLine();
                    }
            }
        }



        public override string ToString()
        {
            return base.ToString();
        }
    }
}
