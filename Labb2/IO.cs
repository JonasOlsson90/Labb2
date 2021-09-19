using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Labb2
{
    class IO
    {
        private static readonly string pathToFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labb2");
        private static readonly string pathToCustomerTextFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labb2", "Customers");
        // Gammal väg för textfil!: AppDomain.CurrentDomain.BaseDirectory

        internal static void WriteToFile(Customer customer)
        {
            File.AppendAllText(pathToCustomerTextFile, $"{customer.Name},{customer.Password},{customer.GetCustomerType()}\n");          
        }

        internal static List<Customer> ReadFromFile()
        {
            //ToDo Implementera
            if (!File.Exists(pathToCustomerTextFile))
            {
                CreateFile();
                WriteToFile(new BronzeCustomer("Knatte", "123"));
                WriteToFile(new SilverCustomer("Fnatte", "321"));
                WriteToFile(new GoldCustomer("Tjatte", "213"));
            }

            var customers = new List<Customer>();
            var users = File.ReadAllLines(pathToCustomerTextFile);

            foreach (var user in users)
            {
                var temp = user.Split(',');
                if (temp[2] == "Regular")
                    customers.Add(new Customer(temp[0], temp[1]));
                if (temp[2] == "Bronze")
                    customers.Add(new BronzeCustomer(temp[0], temp[1]));
                if (temp[2] == "Silver")
                    customers.Add(new SilverCustomer(temp[0], temp[1]));
                if (temp[2] == "Gold")
                    customers.Add(new GoldCustomer(temp[0], temp[1]));
            }

            return customers;
        }

        internal static void CreateFile()
        {
            Directory.CreateDirectory(pathToFolder);
            using FileStream fs = File.Create(pathToCustomerTextFile);
        }
    }
}
