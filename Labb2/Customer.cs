using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Customer
    {
        internal static int indexOfLoggedInUser = -1;

        internal string Name { get; private protected set; }
        private protected string Password { get; set; }
        public List<Item> Cart { get; private protected set; }
        public string PreferedCurrency { get; private protected set; }

        // För att addera valuta, lägg bara till en valuta i denna dictionary med förkortning som string först och kurs mot svenska kronor som double sist.
        public Dictionary<string, double> CurrencyNameValue = new() { { "SEK", 1.0 }, { "USD", 8.58 }, {"GP", 4.0 } };        

        internal Customer(string name, string password)
        {
            // Set name and password
            Name = name;
            Password = password;
            Cart = new List<Item>();
            // Sätt valuta
            PreferedCurrency = "SEK";
        }

        internal void AddToCart(Item item)
        {
            // Add item to cart
            if (!Cart.Any(product => product.Name == item.Name))
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

        internal double CalculateTotalSumOfCart()
        {
            return ConvertPrice(this.Cart.Sum(item => item.Price * item.Amount));
        }

        internal double ConvertPrice(double price)
        {
            return Math.Round(price / this.CurrencyNameValue[PreferedCurrency], 2);
        }

        internal bool ValidatePassword(string password)
        {
            return password == this.Password;
        }

        internal void ChangeCurrency(string currency)
        {
            PreferedCurrency = currency;
        }

        public override string ToString()
        {
            string customerInfo = $"Name: {Name}\nPassword: {Password}\n\nCart:\n\n";

            foreach (var item in Cart)
                customerInfo += $"Item: {item.Name}\nPrice: {Math.Round(item.Price / CurrencyNameValue[PreferedCurrency], 2)} {PreferedCurrency}\nQty: {item.Amount}\n\n";

            return customerInfo;
        }
    }
}
