using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb2
{
    class Customer
    {
        // För att addera valuta, lägg bara till en valuta i denna dictionary med förkortning som string först och kurs mot svenska kronor som double sist.
        internal static readonly Dictionary<string, double> CurrencyNameValue = new() { { "SEK", 1.0 }, { "USD", 8.58 }, {"GP", 4.0 } };  

        // Hanterar om en användare är inloggad och i så fall vilken genom hela programmet.
        internal static int indexOfLoggedInUser = -1;

        internal string Name { get; private protected set; }
        internal string Password { get; private protected set; }
        internal List<Item> Cart { get; private protected set; }
        internal string PreferedCurrency { get; private protected set; }

      

        internal Customer(string name, string password)
        {
            Name = name;
            Password = password;
            Cart = new List<Item>();
            // Valutan defaultar alltid till SEK.
            PreferedCurrency = "SEK";
        }

        internal void AddToCart(Item item)
        {
            // Om varan inte lagts till tidigare, lägg till vara i listan (varukorgen)
            if (!Cart.Any(product => product.Name == item.Name))
            {
                Cart.Add(item);
                Console.Clear();
                Console.Write($"{item.Name} Has been added to your cart!\nPress any key to continue shoping...");
                Console.ReadKey(true);
            }
            // Om varan redan ligger i korgen, ändra endast antal av varan.
            else
            {
                for (int i = 0; i < Cart.Count; i++)
                    if (Cart[i].Name == item.Name)
                    {
                        Cart[i].ChangeAmount(1);
                        Console.WriteLine($"One more {Cart[i].Name} has been added to your cart.\nCart now contains {Cart[i].Amount} {Cart[i].Name}s!\nPress any key to continue shoping...");
                        Console.ReadKey(true);
                    }
            }
        }

        internal double CalculateTotalSumOfCart()
        {
            return ConvertPrice(Cart.Sum(item => item.Price * item.Amount));
        }

        internal double ConvertPrice(double price)
        {
            return Math.Round(price / CurrencyNameValue[PreferedCurrency], 2);
        }

        internal bool ValidatePassword(string password)
        {
            return password == Password;
        }

        internal void ChangeCurrency(string currency)
        {
            PreferedCurrency = currency;
        }

        internal virtual string GetCustomerType()
        {
            return "Regular";
        }

        public override string ToString()
        {
            string customerInfo = $"Name: {Name}\nPassword: {Password}\nCustomer Type: {GetCustomerType()}\n\nCart:\n\n";

            if (Cart.Count == 0)
                customerInfo += "Your cart is empty!";

            else
            {
                foreach (var item in Cart)
                    customerInfo += $"Item: {item.Name}\n" +
                        $"Price: {Math.Round(item.Price / CurrencyNameValue[PreferedCurrency], 2)} {PreferedCurrency}\n" +
                        $"Qty: {item.Amount}\n\n";
            }
            return customerInfo;
        }
    }
}
