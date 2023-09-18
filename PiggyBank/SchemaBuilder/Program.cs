using Microsoft.EntityFrameworkCore;
using SchemaBuilder.models;
using System.Data.Entity;

namespace SchemaBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            // Build the configuration from an appsettings.json file
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            // Get the custom connection string from the configuration
            string connectionString = config.GetConnectionString("DefaultConnection");

            // Create the DbContextOptions with the custom connection string
            var options = new DbContextOptionsBuilder<PiggyContext>()
                .UseSqlServer(connectionString)
                .Options;
            */


            // using (var db = new PiggyContext(options))
            using (var db = new PiggyContext())
            {
                var address = new Address
                {
                    Number = "123",
                    Street = "Avenue A",
                    City = "Lille",
                    Zip = "59000"
                };

                db.adresses.Add(address);
                db.SaveChanges();

                var query = from a in db.adresses
                            orderby a.City
                            select a;

                foreach (var item in query)
                {
                    Console.WriteLine(item.Number + " " + item.Street);
                }
            }
        }
    }
}