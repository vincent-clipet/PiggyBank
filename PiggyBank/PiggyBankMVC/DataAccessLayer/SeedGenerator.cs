using Microsoft.EntityFrameworkCore;
using SchemaBuilder;
using SchemaBuilder.models;

namespace PiggyBankMVC.DataAccessLayer
{
    public static class SeedGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PiggyContext(serviceProvider.GetRequiredService<DbContextOptions<PiggyContext>>()))
            {
                // DB is already seeded, GTFO
                if (context.Addresses.Any())
                    return;

                (new List<Address>{
                    new Address {Number="---", Street="---", City="---", Zip="---"},
                    new Address {Number="123", Street="Avenue A", City="Lille", Zip="59000"},
                    new Address {Number="456", Street="Rue B", City="Paris", Zip="75000"},
                    new Address {Number="000", Street="Boulevard Ornstein", City="Anor Londo", Zip="00000"},
                    new Address {Number="3", Street="Rue des Dunes", City="Dunkerque", Zip="59640"},
                    new Address {Number="1", Street="Avenue B", City="Marseille", Zip="13000"},
                    new Address {Number="46", Street="Rue C", City="Lille", Zip="59800"},
                    new Address {Number="89 bis", Street="Rue D", City="Lille", Zip="59800 CEDEX 2"},
                    new Address {Number="Euratechnologies", Street="Rue E", City="Lille", Zip="59000"},
                    new Address {Number="96", Street="Rue F", City="Lille", Zip="59000"},
                    new Address {Number="136a", Street="Rue G", City="Lille", Zip="59000"},
                    new Address {Number="190", Street="Rue H", City="Lille", Zip="59000"},
                    new Address {Number="23", Street="Rue I", City="Lille", Zip="59000"},
                    new Address {Number="39", Street="Rue J", City="Lille", Zip="59000"},
                    new Address {Number="77", Street="Rue K", City="Lille", Zip="59000"},
                    new Address {Number="234", Street="Avenue X", City="Paris", Zip="75001"},
                    new Address {Number="789", Street="Rue Y", City="Lyon", Zip="69000"},
                    new Address {Number="555", Street="Boulevard Z", City="Marseille", Zip="13001"},
                    new Address {Number="12", Street="Rue de la Paix", City="Paris", Zip="75002"},
                    new Address {Number="567", Street="Avenue Molière", City="Brussels", Zip="1000"},
                    new Address {Number="987", Street="Rue de la Liberté", City="Nice", Zip="06000"},
                    new Address {Number="321", Street="Avenue Franklin", City="New York", Zip="10001"},
                    new Address {Number="654", Street="Main Street", City="Los Angeles", Zip="90001"},
                    new Address {Number="111", Street="Rue Lafayette", City="Paris", Zip="75003"},
                    new Address {Number="222", Street="Avenue Montaigne", City="Paris", Zip="75004"},
                    new Address {Number="777", Street="Broadway", City="New York", Zip="10002"},
                    new Address {Number="456", Street="Sunset Boulevard", City="Los Angeles", Zip="90002"},
                    new Address {Number="888", Street="Königstraße", City="Berlin", Zip="10117"},
                    new Address {Number="999", Street="Piazza San Marco", City="Venice", Zip="30124"},
                    new Address {Number="456", Street="Champs-Élysées", City="Paris", Zip="75005"},
                    new Address {Number="567", Street="Rue de la République", City="Lyon", Zip="69001"},
                    new Address {Number="101", Street="Avenue des Champs-Élysées", City="Paris", Zip="75006"},
                    new Address {Number="888", Street="Via Condotti", City="Rome", Zip="00187"},
                    new Address {Number="321", Street="Oxford Street", City="London", Zip="W1D 1BS"},
                    new Address {Number="444", Street="Gran Vía", City="Madrid", Zip="28013"},
                    new Address {Number="777", Street="Fifth Avenue", City="New York", Zip="10003"},
                    new Address {Number="999", Street="Hollywood Boulevard", City="Los Angeles", Zip="90003"},
                    new Address {Number="123", Street="Passeig de Gràcia", City="Barcelona", Zip="08008"},
                    new Address {Number="456", Street="Kurfürstendamm", City="Berlin", Zip="10707"},
                    new Address {Number="555", Street="Nevsky Prospect", City="St. Petersburg", Zip="190000"},
                    new Address {Number="789", Street="Piazza del Duomo", City="Milan", Zip="20121"},
                    new Address {Number="666", Street="Rue du Faubourg Saint-Honoré", City="Paris", Zip="75007"},
                    new Address {Number="888", Street="Av. Paulista", City="São Paulo", Zip="01310-000"},
                    new Address {Number="1010", Street="Nanjing Road", City="Shanghai", Zip="200001"},
                    new Address {Number="555", Street="Ginza", City="Tokyo", Zip="104-0061"}
                }).ForEach(element => context.Addresses.Add(element)); context.SaveChanges();

                (new List<Role>{
                    new Role {Name="Admin"},
                    new Role {Name="Moderator"},
                    new Role {Name="Assist"},
                    new Role {Name="Customer"},
                }).ForEach(element => context.Roles.Add(element)); context.SaveChanges();

                (new List<User>{
                    new User {Firstname="Admin", Lastname="Admin", Email="admin@piggybank.com", Password="Admin", Phone="0000000000", IsActive=true, AddressId=1, RoleId=1},
                    new User {Firstname="Moderator", Lastname="Moderator", Email="moderator@piggybank.com", Password="Moderator", Phone="0000000001", IsActive=true, AddressId=1, RoleId=2},
                    new User {Firstname="Assist", Lastname="Assist", Email="assist@piggybank.com", Password="Assist", Phone="0000000002", IsActive=true, AddressId=1, RoleId=3},
                    new User {Firstname="Jean-Michel", Lastname="Lambda", Email="jean-michel.lambdba@gmail.com", Password="Lambda", Phone="0320597896", IsActive=true, AddressId=4, RoleId=4},
                    new User {Firstname="Azerty", Lastname="Ytreza", Email="azerty@gmail.com", Password="Ytreza", Phone="0328976315", IsActive=true, AddressId=5, RoleId=4},
                    new User {Firstname="Malenia", Lastname="Blade of Miquella", Email="malenia@get-rekt.com", Password="Malenia", Phone="0666666666", IsActive=true, AddressId=6, RoleId=4},
                    new User {Firstname="Andrew", Lastname="Ryan", Email="a.ryan@corporate.org", Password="Ryan", Phone="0564879332", IsActive=true, AddressId=7, RoleId=4},
                    new User {Firstname="George", Lastname="Abitbol", Email="classiest.man@entire.world", Password="Abitbol", Phone="+33678960408", IsActive=true, AddressId=8, RoleId=4},
                    new User { Firstname = "Alice", Lastname = "Johnson", Email = "alice.johnson@example.com", Password = "Johnson", Phone = "123-456-7890", IsActive = true, AddressId = 9, RoleId = 4 },
                    new User { Firstname = "Bob", Lastname = "Smith", Email = "bob.smith@example.com", Password = "Smith", Phone = "987-654-3210", IsActive = true, AddressId = 10, RoleId = 4 },
                    new User { Firstname = "Emily", Lastname = "Davis", Email = "emily.davis@example.com", Password = "Davis", Phone = "555-123-4567", IsActive = true, AddressId = 11, RoleId = 4 },
                    new User { Firstname = "David", Lastname = "Wilson", Email = "david.wilson@example.com", Password = "Wilson", Phone = "777-888-9999", IsActive = true, AddressId = 12, RoleId = 4 },
                    new User { Firstname = "Sophia", Lastname = "Lee", Email = "sophia.lee@example.com", Password = "Lee", Phone = "222-333-4444", IsActive = true, AddressId = 13, RoleId = 4 },
                    new User { Firstname = "Liam", Lastname = "Martin", Email = "liam.martin@example.com", Password = "Martin", Phone = "888-777-6666", IsActive = true, AddressId = 14, RoleId = 4 },
                    new User { Firstname = "Olivia", Lastname = "Thompson", Email = "olivia.thompson@example.com", Password = "Thompson", Phone = "999-555-7777", IsActive = true, AddressId = 15, RoleId = 4 },
                    new User { Firstname = "Noah", Lastname = "Garcia", Email = "noah.garcia@example.com", Password = "Garcia", Phone = "111-222-3333", IsActive = true, AddressId = 16, RoleId = 4 }
                }).ForEach(element => context.Users.Add(element)); context.SaveChanges();



                (new List<Manufacturer>{
                    new Manufacturer {Name="Ikea", AddressId=17},
                    new Manufacturer {Name="Boulanger", AddressId=18},
                    new Manufacturer {Name="Amazon", AddressId=19},
                    new Manufacturer { Name = "Ashley Furniture", AddressId = 20 },
                    new Manufacturer { Name = "Wayfair", AddressId = 21 },
                    new Manufacturer { Name = "Rooms To Go", AddressId = 22 },
                    new Manufacturer { Name = "Crate & Barrel", AddressId = 23 },
                    new Manufacturer { Name = "La-Z-Boy", AddressId = 24 },
                    new Manufacturer { Name = "Pier 1 Imports", AddressId = 25 },
                    new Manufacturer { Name = "Raymour & Flanigan", AddressId = 26 },
                    new Manufacturer { Name = "West Elm", AddressId = 27 },
                    new Manufacturer { Name = "Havertys", AddressId = 28 },
                    new Manufacturer { Name = "Bob's Discount Furniture", AddressId = 29 }
                }).ForEach(element => context.Manufacturers.Add(element)); context.SaveChanges();

                (new List<Product>{
                    new Product { Name="Red Piggybank", ImageUrl=null, Description="Nice red piggy", Height=60, Width=40, Length=80, Weight=1200, Capacity=883, Color="Red", Price=1800, IsActive=true, ManufacturerId=1},
                    new Product { Name="Green Piggybank", ImageUrl=null, Description="Cool green piggy", Height=70, Width=35, Length=90, Weight=1550, Capacity=1150, Color="Green", Price=2500, IsActive=true, ManufacturerId=1},
                    new Product { Name="Red Piggybank (Deluxe Season Pass)", ImageUrl=null, Description="Another red piggy, more deluxe this time", Height=50, Width=35, Length=75, Weight=1200, Capacity=913, Color="Red", Price=9999, IsActive=true, ManufacturerId=1},
                    new Product { Name="Broken Piggybank", ImageUrl=null, Description="It's broken, don't buy it", Height=60, Width=40, Length=80, Weight=800, Capacity=883, Color="Red", Price=1, IsActive=true, ManufacturerId=2},
                    new Product { Name = "Blue Piggybank", ImageUrl = null, Description = "Beautiful blue piggy", Height = 55, Width = 45, Length = 85, Weight = 1350, Capacity = 950, Color = "Blue", Price = 2100, IsActive = true, ManufacturerId = 2 },
                    new Product { Name = "Yellow Piggybank", ImageUrl = null, Description = "Vibrant yellow piggy", Height = 65, Width = 50, Length = 95, Weight = 1450, Capacity = 1030, Color = "Yellow", Price = 2700, IsActive = true, ManufacturerId = 2 },
                    new Product { Name = "Green Piggybank (Limited Edition)", ImageUrl = null, Description = "Exclusive green piggy", Height = 75, Width = 55, Length = 105, Weight = 1650, Capacity = 1200, Color = "Green", Price = 3500, IsActive = true, ManufacturerId = 2 },
                    new Product { Name = "Blue Piggybank (XL)", ImageUrl = null, Description = "Extra-large blue piggy", Height = 80, Width = 60, Length = 110, Weight = 1850, Capacity = 1350, Color = "Blue", Price = 4200, IsActive = true, ManufacturerId = 3 },
                    new Product { Name = "Purple Piggybank", ImageUrl = null, Description = "Elegant purple piggy", Height = 70, Width = 55, Length = 100, Weight = 1550, Capacity = 1100, Color = "Purple", Price = 3100, IsActive = true, ManufacturerId = 3 },
                    new Product { Name = "Pink Piggybank (Glow-in-the-dark)", ImageUrl = null, Description = "Fun pink piggy that glows in the dark", Height = 60, Width = 50, Length = 95, Weight = 1400, Capacity = 980, Color = "Pink", Price = 2900, IsActive = true, ManufacturerId = 3 },
                    new Product { Name = "Silver Piggybank", ImageUrl = null, Description = "Shiny silver piggy", Height = 65, Width = 50, Length = 95, Weight = 1550, Capacity = 1100, Color = "Silver", Price = 3200, IsActive = false, ManufacturerId = 4 },
                    new Product { Name = "Gold Piggybank (Limited Edition)", ImageUrl = null, Description = "Exclusive gold piggy", Height = 75, Width = 55, Length = 105, Weight = 1750, Capacity = 1250, Color = "Gold", Price = 4200, IsActive = true, ManufacturerId = 4 },
                    new Product { Name = "Bronze Piggybank", ImageUrl = null, Description = "Classic bronze piggy", Height = 60, Width = 45, Length = 90, Weight = 1400, Capacity = 1000, Color = "Bronze", Price = 2700, IsActive = true, ManufacturerId = 4 },
                    new Product { Name = "Black Piggybank (Stealth Edition)", ImageUrl = null, Description = "Sleek black piggy", Height = 70, Width = 55, Length = 100, Weight = 1650, Capacity = 1150, Color = "Black", Price = 3400, IsActive = true, ManufacturerId = 5 },
                    new Product { Name = "Orange Piggybank", ImageUrl = null, Description = "Vivid orange piggy", Height = 55, Width = 40, Length = 85, Weight = 1300, Capacity = 900, Color = "Orange", Price = 2300, IsActive = false, ManufacturerId = 5 },
                    new Product { Name = "Pink Piggybank (Floral Design)", ImageUrl = null, Description = "Pretty pink piggy with floral patterns", Height = 65, Width = 50, Length = 95, Weight = 1500, Capacity = 1050, Color = "Pink", Price = 2900, IsActive = true, ManufacturerId = 5 },
                    new Product { Name = "Copper Piggybank", ImageUrl = null, Description = "Classic copper piggy", Height = 60, Width = 45, Length = 90, Weight = 1400, Capacity = 1000, Color = "Copper", Price = 2600, IsActive = false, ManufacturerId = 6 },
                    new Product { Name = "Teal Piggybank", ImageUrl = null, Description = "Stylish teal piggy", Height = 70, Width = 55, Length = 100, Weight = 1650, Capacity = 1150, Color = "Teal", Price = 3200, IsActive = true, ManufacturerId = 6 },
                    new Product { Name = "Red Piggybank (Limited Edition)", ImageUrl = null, Description = "Exclusive red piggy with gold accents", Height = 75, Width = 55, Length = 105, Weight = 1750, Capacity = 1250, Color = "Red", Price = 3900, IsActive = false, ManufacturerId = 6 },
                    new Product { Name = "Green Piggybank (Jungle Adventure)", ImageUrl = null, Description = "Green piggy with jungle-themed decorations", Height = 60, Width = 45, Length = 90, Weight = 1400, Capacity = 1000, Color = "Green", Price = 2800, IsActive = true, ManufacturerId = 7 },
                }).ForEach(element => context.Products.Add(element)); context.SaveChanges();



                (new List<OrderStatus>{
                    new OrderStatus {Name="Received"},
                    new OrderStatus {Name="Preparing"},
                    new OrderStatus {Name="Packaged"},
                    new OrderStatus {Name="Sent"},
                    new OrderStatus {Name="Delivered"},
                }).ForEach(element => context.OrderStatuses.Add(element)); context.SaveChanges();

                (new List<Order>{
                    new Order {CreatedAt=DateTime.Now, UserId=4, AddressId=1, StatusId=1},
                    new Order {CreatedAt=DateTime.Now, UserId=5, AddressId=2, StatusId=5},
                    new Order {CreatedAt=DateTime.Now, UserId=6, AddressId=3, StatusId=2},
                    new Order {CreatedAt=DateTime.Now, UserId=4, AddressId=4, StatusId=3},
                    new Order {CreatedAt=DateTime.Now, UserId=7, AddressId=1, StatusId=4},
                    new Order {CreatedAt=DateTime.Now, UserId=8, AddressId=2, StatusId=1},
                    new Order { CreatedAt = DateTime.Now, UserId = 9, AddressId = 5, StatusId = 2 },
                    new Order { CreatedAt = DateTime.Now, UserId = 10, AddressId = 1, StatusId = 3 },
                    new Order { CreatedAt = DateTime.Now, UserId = 11, AddressId = 6, StatusId = 4 },
                    new Order { CreatedAt = DateTime.Now, UserId = 12, AddressId = 7, StatusId = 1 },
                    new Order { CreatedAt = DateTime.Now, UserId = 13, AddressId = 8, StatusId = 5 },
                    new Order { CreatedAt = DateTime.Now, UserId = 14, AddressId = 9, StatusId = 2 },
                    new Order { CreatedAt = DateTime.Now, UserId = 10, AddressId = 10, StatusId = 3 },
                    new Order { CreatedAt = DateTime.Now, UserId = 5, AddressId = 11, StatusId = 4 }
                }).ForEach(element => context.Orders.Add(element)); context.SaveChanges();

                (new List<OrderDetail>{
                    new OrderDetail {Quantity=1, Price=20000, OrderId=1, ProductId=1},
                    new OrderDetail {Quantity=5, Price=15000, OrderId=1, ProductId=2},
                    new OrderDetail {Quantity=1, Price=21000, OrderId=2, ProductId=1},
                    new OrderDetail {Quantity=1, Price=22000, OrderId=3, ProductId=3},
                    new OrderDetail {Quantity=2, Price=23000, OrderId=4, ProductId=3},
                    new OrderDetail {Quantity=18, Price=95099, OrderId=4, ProductId=1},
                    new OrderDetail {Quantity=1, Price=1800, OrderId=4, ProductId=4},
                    new OrderDetail {Quantity=3, Price=78689, OrderId=5, ProductId=2},
                    new OrderDetail {Quantity=1, Price=78689, OrderId=5, ProductId=5},
                    new OrderDetail {Quantity=1, Price=1234, OrderId=6, ProductId=5},
                    new OrderDetail { Quantity = 3, Price = 25000, OrderId = 6, ProductId = 3 },
                    new OrderDetail { Quantity = 2, Price = 17000, OrderId = 7, ProductId = 4 },
                    new OrderDetail { Quantity = 1, Price = 21000, OrderId = 8, ProductId = 1 },
                    new OrderDetail { Quantity = 4, Price = 26000, OrderId = 8, ProductId = 5 },
                    new OrderDetail { Quantity = 2, Price = 22000, OrderId = 8, ProductId = 2 },
                    new OrderDetail { Quantity = 1, Price = 20000, OrderId = 9, ProductId = 6 },
                    new OrderDetail { Quantity = 3, Price = 23000, OrderId = 9, ProductId = 4 },
                    new OrderDetail { Quantity = 2, Price = 19000, OrderId = 10, ProductId = 7 },
                    new OrderDetail { Quantity = 1, Price = 24000, OrderId = 10, ProductId = 8 },
                    new OrderDetail { Quantity = 1, Price = 28000, OrderId = 10, ProductId = 9 },
                    new OrderDetail { Quantity = 5, Price = 18000, OrderId = 10, ProductId = 10 },
                    new OrderDetail { Quantity = 2, Price = 22000, OrderId = 10, ProductId = 11 },
                    new OrderDetail { Quantity = 3, Price = 26000, OrderId = 11, ProductId = 10 },
                    new OrderDetail { Quantity = 1, Price = 30000, OrderId = 12, ProductId = 12 },
                    new OrderDetail { Quantity = 2, Price = 32000, OrderId = 13, ProductId = 8 }
                }).ForEach(element => context.OrderDetails.Add(element)); context.SaveChanges();



                (new List<ReviewStatus>{
                    new ReviewStatus {Name="Awaiting Moderation"},
                    new ReviewStatus {Name="Approved"},
                    new ReviewStatus {Name="Unapproved"},
                }).ForEach(element => context.ReviewStatuses.Add(element)); context.SaveChanges();

                (new List<Review>{
                    new Review {Score=5, Message="Very good product, would recommend", CreatedAt = DateTime.Now, StatusId=1, UserId=2, ProductId=1},
                    new Review {Score=1, Message="Terrible, don't buy this !!!!!!!!!!!!!!", CreatedAt = DateTime.Now, StatusId=1, UserId=3, ProductId=4},
                    new Review { Score = 4, Message = "Good quality, but a bit pricey", CreatedAt = DateTime.Now, StatusId = 2, UserId = 4, ProductId = 2 },
                    new Review { Score = 3, Message = "Decent product, average performance", CreatedAt = DateTime.Now, StatusId = 3, UserId = 5, ProductId = 3 },
                    new Review { Score = 5, Message = "Excellent product, worth every penny", CreatedAt = DateTime.Now, StatusId = 1, UserId = 6, ProductId = 5 },
                    new Review { Score = 2, Message = "Not what I expected, disappointed", CreatedAt = DateTime.Now, StatusId = 2, UserId = 7, ProductId = 6 },
                    new Review { Score = 4, Message = "Great product, fast delivery", CreatedAt = DateTime.Now, StatusId = 1, UserId = 8, ProductId = 7 },
                    new Review { Score = 5, Message = "Outstanding quality, highly recommended", CreatedAt = DateTime.Now, StatusId = 3, UserId = 9, ProductId = 8 },
                    new Review { Score = 1, Message = "Worst purchase ever, never buying again", CreatedAt = DateTime.Now, StatusId = 1, UserId = 10, ProductId = 9 },
                    new Review { Score = 4, Message = "Satisfied with the product, good value", CreatedAt = DateTime.Now, StatusId = 2, UserId = 11, ProductId = 10 },
                    new Review { Score = 5, Message = "Impressive product, exceeded my expectations", CreatedAt = DateTime.Now, StatusId = 3, UserId = 12, ProductId = 11 },
                    new Review { Score = 3, Message = "Average product, nothing special", CreatedAt = DateTime.Now, StatusId = 1, UserId = 13, ProductId = 12 },
                    new Review { Score = 4, Message = "Reliable product, happy with the purchase", CreatedAt = DateTime.Now, StatusId = 2, UserId = 14, ProductId = 1 },
                    new Review { Score = 5, Message = "Top-notch quality, would buy again", CreatedAt = DateTime.Now, StatusId = 3, UserId = 15, ProductId = 2 },
                    new Review { Score = 2, Message = "Disappointing product, not worth the price", CreatedAt = DateTime.Now, StatusId = 2, UserId = 3, ProductId = 3 },
                    new Review { Score = 4, Message = "Good product, could be better", CreatedAt = DateTime.Now, StatusId = 3, UserId = 4, ProductId = 4 },
                    new Review { Score = 3, Message = "Average performance, nothing special", CreatedAt = DateTime.Now, StatusId = 1, UserId = 5, ProductId = 5 },
                    new Review { Score = 4, Message = "Satisfied with the purchase, decent quality", CreatedAt = DateTime.Now, StatusId = 2, UserId = 6, ProductId = 6 },
                    new Review { Score = 5, Message = "Excellent value for money, highly recommended", CreatedAt = DateTime.Now, StatusId = 1, UserId = 7, ProductId = 7 },
                    new Review { Score = 1, Message = "Avoid this product at all costs, terrible", CreatedAt = DateTime.Now, StatusId = 1, UserId = 8, ProductId = 8 }
                }).ForEach(element => context.Reviews.Add(element)); context.SaveChanges();
            }
        }
    }
}
