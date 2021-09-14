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

        internal Item(string name, double prize)
        {
            Name = name;
            Price = prize;
            Amount = 0;
        }

        internal void ChangeAmount(int amount)
        {
            Amount += amount;
        }
    }
}
