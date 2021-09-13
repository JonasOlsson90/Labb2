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
        public List<Item> Chart { get; private protected set; }

        public Customer(string name, string password)
        {
            // Set name and password
            Name = name;
            Password = password;
            Chart = new List<Item>();
        }

        public void AddToChart(Item item)
        {
            // Add item to chart
            Chart.Add(item);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
