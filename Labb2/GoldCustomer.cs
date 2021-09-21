using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class GoldCustomer : Customer, IDiscount
    {
        internal GoldCustomer(string name, string password) : base(name, password)
        {
        }

        public double AddDiscount(double originalPrice)
        {
            // Returnerar pris med 15% avdraget.
            return originalPrice * 0.85;
        }

        internal override string GetCustomerType()
        {
            return "Gold";
        }
    }
}
