
namespace Labb2
{
    class SilverCustomer : Customer, IDiscount
    {
        internal SilverCustomer(string name, string password) : base(name, password)
        {
        }

        public double AddDiscount(double originalPrice)
        {
            // Returnerar pris med 10% avdraget.
            return originalPrice * 0.90;
        }

        internal override string GetCustomerType()
        {
            return "Silver";
        }
    }
}
