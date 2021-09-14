using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    interface IDiscount
    {
        public abstract double AddDiscount(double originalPrice);
    }
}
