using SchemaBuilder.models;
using System.Data.Entity;

namespace SchemaBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
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