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

        private string SetName(string name)
        {
            // 
            return "";
        }

        private protected double SetPrice(double price)
        {
            //
            return 0.0;
        }
    }
}
