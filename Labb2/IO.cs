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
        // Jag är lite osäker på var bästa stället att lägga textfilen är.
        // Jag tänkte först lägga den i mappen man kör programmet från, men ändrade mig efter att ha pratat med anonym klasskamrat.
        private static readonly string pathToFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labb2");
        private static readonly string pathToCustomerTextFile = Path.Combine(pathToFolder, "Customers");

        // Gammal väg för textfil!: AppDomain.CurrentDomain.BaseDirectory

        internal static void WriteToFile(Customer customer)
        {
            // Skriver ner användarinfo i ett format jag tycker är behändigt.
            File.AppendAllText(pathToCustomerTextFile, $"{customer.Name},{customer.Password},{customer.GetCustomerType()}\n");          
        }

        internal static List<Customer> ReadFromFile()
        {
            // Om filen inte finns när programmet startas skapas filen med fördefinierade kunder.
            if (!File.Exists(pathToCustomerTextFile))
                CreateFile();

            // Sedan läses informationen in
            var customers = new List<Customer>();
            var users = File.ReadAllLines(pathToCustomerTextFile);
            var isTextFileTamperedWith = false;

            foreach (var user in users)
            {
                // Alla rader borde vara i rätt format, annars har någon varit och pillat i filen manuellt och det är INTE ok!
                try
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
                    if (temp.Length != 3)
                        isTextFileTamperedWith = true;
                }
                catch(IndexOutOfRangeException)
                {
                    isTextFileTamperedWith = true;
                }

            }

            // Om någon ändå har varit och pillat med textfilen ska de känna rädsla, skuld och skam!
            // Men programmet ska inte krascha för det!
            if (isTextFileTamperedWith)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have tampered with the textfile and thus awakened the great wrath of Cthulhu!!!\n\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Press any key to continue...");
                Console.ReadKey(true);
            }

            return customers;
        }

        internal static void CreateFile()
        {
            Directory.CreateDirectory(pathToFolder);
            using FileStream fs = File.Create(pathToCustomerTextFile);
            fs.Close();
            WriteToFile(new BronzeCustomer("Knatte", "123"));
            WriteToFile(new SilverCustomer("Fnatte", "321"));
            WriteToFile(new GoldCustomer("Tjatte", "213"));
        }
    }
}
