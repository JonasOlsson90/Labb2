﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class BronzeCustomer : Customer, IDiscount
    {
        internal BronzeCustomer(string name, string password) : base(name, password)
        {
        }

        public double AddDiscount(double originalPrice)
        {
            // return price with 5% d
            return 0.0;
        }
    }
}
