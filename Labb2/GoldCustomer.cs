using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class GoldCustomer : Customer
    {
        internal GoldCustomer(string name, string password) : base(name, password)
        {
        }

        private double AddDiscount(double originalPrice)
        {
            // return price with 15% discount
            return 0.0;
        }
    }
}
