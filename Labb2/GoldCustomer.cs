﻿using System;
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
            // return price with 15% discount
            return 0.0;
        }
    }
}
