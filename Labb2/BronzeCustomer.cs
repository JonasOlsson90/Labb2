
namespace Labb2
{
    class BronzeCustomer : Customer, IDiscount
    {
        internal BronzeCustomer(string name, string password) : base(name, password)
        {
        }

        public double AddDiscount(double originalPrice)
        {
            // Returnerar pris med 5% avdraget.
            return originalPrice * 0.95;
        }

        internal override string GetCustomerType()
        {
            return "Bronze";
        }
    }
}
