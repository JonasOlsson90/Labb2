using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Item
    {
        public string Name { get; private protected set; }
        public double Price { get; private protected set; }
        public int Amount { get; private protected set; }

        internal Item(string name, double price)
        {
            Name = name;
            Price = price;
            // När varan instansieras ska det alltid bara vara en.
            // Om fler av varan läggs till ökar värdet genom metoden nedan.
            Amount = 1;
        }

        internal void ChangeAmount(int amount)
        {
            // Genom denna metod kan man ändra antalet av varan åt båda håll.
            // Programmet stödjer dock inte att ta bort varor ur korgen utan att köpa dem då man tjänar mer pengar så.
            Amount += amount;
        }
    }
}
