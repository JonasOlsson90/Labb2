using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class SilverCustomer : Customer, IDiscount
    {
        internal SilverCustomer(string name, string password) : base(name, password)
        {
        }

        public double AddDiscount(double originalPrice)
        {
            // return price with 10% discount
            return originalPrice * 0.90;
        }

        internal override string GetCustomerType()
        {
            return "Silver";
        }
    }
}
