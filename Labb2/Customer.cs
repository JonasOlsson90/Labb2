using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Customer
    {
        private protected string Name { get; set; }
        private protected string Password { get; set; }
        private protected List<Item> Chart { get; set; }

        private protected Customer()
        {

        }

        public Customer(string name, string password)
        {
            // Set name and password
            Chart = new List<Item>();
        }

        public void AddToChart(Item item)
        {
            // Add item to chart
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
