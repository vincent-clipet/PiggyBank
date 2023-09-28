using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.Models;
using PiggyBankMVC.Models.Enums;

namespace PiggyBankMVC.DataAccessLayer
{
    public static class SeedGenerator
    {
        private static UserManager<ApplicationUser>? _userManager;
        private static RoleManager<IdentityRole>? _roleManager;
        private static PiggyContext _context;

        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            using (var context = new PiggyContext(serviceProvider.GetRequiredService<DbContextOptions<PiggyContext>>()))
            {
                _context = context;
                _userManager = userManager;
                _roleManager = roleManager;

                // DB is already seeded, GTFO
                if (context.Addresses.Any())
                    return;

                try
                {
                    seedAddresses();
                    await seedRoles();
                    await seedUsers();
                    seedManufacturers();
                    seedProducts();
                    await seedOrders();
                    await seedOrderDetails();
                    await seedReviews();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private static void seedAddresses()
        {
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
            }).ForEach(element => _context.Addresses.Add(element)); _context.SaveChanges();
        }

        private async static Task seedRoles()
        {
            foreach (EnumRoles role in Enum.GetValues(typeof(EnumRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    try
                    {
                        var element = new IdentityRole(role.ToString());
                        await _roleManager.CreateAsync(element);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            };
        }

        private async static Task seedUsers()
        {
            var admin = new ApplicationUser { Firstname = "Admin", Lastname = "Admin", UserName = "Admin", Email = "admin@piggybank.com", PhoneNumber = "0000000000", IsActive = true, AddressId = 1, EmailConfirmed = true };
            var moderator = new ApplicationUser { Firstname = "Moderator", Lastname = "Moderator", UserName = "Moderator", Email = "moderator@piggybank.com", PhoneNumber = "0000000001", IsActive = true, AddressId = 1, EmailConfirmed = true };
            var assist = new ApplicationUser { Firstname = "Assist", Lastname = "Assist", UserName = "Assist", Email = "assist@piggybank.com", PhoneNumber = "0000000002", IsActive = true, AddressId = 1, EmailConfirmed = true };

            var user = await _userManager.FindByEmailAsync(admin.Email);
            if (user == null)
            {
                await _userManager.CreateAsync(admin, admin.Firstname); // Using Firstname as password for testing
                await _userManager.AddToRoleAsync(admin, EnumRoles.Admin.ToString());
                await _userManager.AddToRoleAsync(admin, EnumRoles.Moderator.ToString());
                await _userManager.AddToRoleAsync(admin, EnumRoles.Assist.ToString());
            }

            user = await _userManager.FindByEmailAsync(moderator.Email);
            if (user == null)
            {
                await _userManager.CreateAsync(moderator, moderator.Firstname); // Using Firstname as password for testing
                await _userManager.AddToRoleAsync(moderator, EnumRoles.Moderator.ToString());
                await _userManager.AddToRoleAsync(moderator, EnumRoles.Assist.ToString());
            }

            user = await _userManager.FindByEmailAsync(assist.Email);
            if (user == null)
            {
                await _userManager.CreateAsync(assist, assist.Firstname); // Using Firstname as password for testing
                await _userManager.AddToRoleAsync(assist, EnumRoles.Assist.ToString());
            }


            var newUsers = new List<ApplicationUser>{
                new ApplicationUser { Firstname = "Jean-Michel", Lastname = "Lambda", UserName = "Jean-Michel", Email="jean-michel.lambda@gmail.com", PhoneNumber="0320597896", IsActive = true, EmailConfirmed = true, AddressId = 2 },
                new ApplicationUser { Firstname = "Azerty", Lastname = "Ytreza", UserName = "Azerty", Email="azerty@gmail.com", PhoneNumber="0328976315", IsActive = true, EmailConfirmed = true, AddressId = 3 },
                new ApplicationUser { Firstname = "Malenia", Lastname = "Blade of Miquella", UserName = "Malenia", Email="malenia@get-rekt.com", PhoneNumber="0666666666", IsActive = true, EmailConfirmed = true, AddressId = 4 },
                new ApplicationUser { Firstname = "Andrew", Lastname = "Ryan", UserName = "Andrew", Email="a.ryan@corporate.org", PhoneNumber="0564879332", IsActive = true, EmailConfirmed = true, AddressId = 5 },
                new ApplicationUser { Firstname = "George", Lastname = "Abitbol", UserName = "George", Email="classiest.man@entire.world", PhoneNumber="+33678960408", IsActive = true, EmailConfirmed = true, AddressId = 6 },
                new ApplicationUser { Firstname = "Alice", Lastname = "Johnson", UserName = "Alice", Email = "alice.johnson@example.com", PhoneNumber="123-456-7890", IsActive = true, EmailConfirmed = true, AddressId = 7 },
                new ApplicationUser { Firstname = "Bob", Lastname = "Smith", UserName = "Bob", Email = "bob.smith@example.com", PhoneNumber="987-654-3210", IsActive = true, EmailConfirmed = true, AddressId = 8 },
                new ApplicationUser { Firstname = "Emily", Lastname = "Davis", UserName = "Emily", Email = "emily.davis@example.com", PhoneNumber="555-123-4567", IsActive = true, EmailConfirmed = true, AddressId = 9 },
                new ApplicationUser { Firstname = "David", Lastname = "Wilson", UserName = "David", Email = "david.wilson@example.com", PhoneNumber="777-888-9999", IsActive = true, EmailConfirmed = true, AddressId = 10 },
                new ApplicationUser { Firstname = "Sophia", Lastname = "Lee", UserName = "Sophia", Email = "sophia.lee@example.com", PhoneNumber="222-333-4444", IsActive = true, EmailConfirmed = true, AddressId = 11 },
                new ApplicationUser { Firstname = "Liam", Lastname = "Martin", UserName = "Liam", Email = "liam.martin@example.com", PhoneNumber="888-777-6666", IsActive = true, EmailConfirmed = true, AddressId = 12 },
                new ApplicationUser { Firstname = "Olivia", Lastname = "Thompson", UserName = "Olivia", Email = "olivia.thompson@example.com", PhoneNumber="999-555-7777", IsActive = true, EmailConfirmed = true, AddressId = 13 },
                new ApplicationUser { Firstname = "Noah", Lastname = "Garcia", UserName = "Noah", Email = "noah.garcia@example.com", PhoneNumber="111-222-3333", IsActive = true, EmailConfirmed = true, AddressId = 14 }
            };

            foreach (ApplicationUser u in newUsers)
            {
                user = await _userManager.FindByEmailAsync(u.Email);
                if (user == null)
                {
                    await _userManager.CreateAsync(u, u.Firstname); // Using Firstname as password for testing
                    await _userManager.AddToRoleAsync(u, EnumRoles.Customer.ToString());
                }
            }
        }

        private static void seedManufacturers()
        {
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
            }).ForEach(element => _context.Manufacturers.Add(element)); _context.SaveChanges();
        }

        private static void seedProducts()
        {
            int incr = 1;
            (new List<Product>{
                new Product { Name = "Red Piggybank", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Nice red piggy", Height=60, Width=40, Length=80, Weight=1200, Capacity=883, Color="Red", Price=18.00M, IsActive=true, ManufacturerId=1},
                new Product { Name = "Green Piggybank", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Cool green piggy", Height=70, Width=35, Length=90, Weight=1550, Capacity=1150, Color="Green", Price=25.00M, IsActive=true, ManufacturerId=1},
                new Product { Name = "Red Piggybank (Deluxe Season Pass)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Another red piggy, more deluxe this time", Height=50, Width=35, Length=75, Weight=1200, Capacity=913, Color="Red", Price=99.99M, IsActive=true, ManufacturerId=1},
                new Product { Name = "Broken Piggybank",ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "It's broken, don't buy it", Height=60, Width=40, Length=80, Weight=800, Capacity=883, Color="Red", Price=0.01M, IsActive=true, ManufacturerId=2},
                new Product { Name = "Blue Piggybank", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Beautiful blue piggy", Height = 55, Width = 45, Length = 85, Weight = 1350, Capacity = 950, Color = "Blue", Price = 21.00M, IsActive = true, ManufacturerId = 2 },
                new Product { Name = "Yellow Piggybank", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Vibrant yellow piggy", Height = 65, Width = 50, Length = 95, Weight = 1450, Capacity = 1030, Color = "Yellow", Price = 27.00M, IsActive = true, ManufacturerId = 2 },
                new Product { Name = "Green Piggybank (Limited Edition)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Exclusive green piggy", Height = 75, Width = 55, Length = 105, Weight = 1650, Capacity = 1200, Color = "Green", Price = 35.00M, IsActive = true, ManufacturerId = 2 },
                new Product { Name = "Blue Piggybank (XL)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Extra-large blue piggy", Height = 80, Width = 60, Length = 110, Weight = 1850, Capacity = 1350, Color = "Blue", Price = 42.00M, IsActive = true, ManufacturerId = 3 },
                new Product { Name = "Purple Piggybank", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Elegant purple piggy", Height = 70, Width = 55, Length = 100, Weight = 1550, Capacity = 1100, Color = "Purple", Price = 31.00M, IsActive = true, ManufacturerId = 3 },
                new Product { Name = "Pink Piggybank (Glow-in-the-dark)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Fun pink piggy that glows in the dark", Height = 60, Width = 50, Length = 95, Weight = 1400, Capacity = 980, Color = "Pink", Price = 29.00M, IsActive = true, ManufacturerId = 3 },
                new Product { Name = "Silver Piggybank", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Shiny silver piggy", Height = 65, Width = 50, Length = 95, Weight = 1550, Capacity = 1100, Color = "Silver", Price = 32.00M, IsActive = false, ManufacturerId = 4 },
                new Product { Name = "Gold Piggybank (Limited Edition)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Exclusive gold piggy", Height = 75, Width = 55, Length = 105, Weight = 1750, Capacity = 1250, Color = "Gold", Price = 42.00M, IsActive = true, ManufacturerId = 4 },
                new Product { Name = "Bronze Piggybank", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Classic bronze piggy", Height = 60, Width = 45, Length = 90, Weight = 1400, Capacity = 1000, Color = "Bronze", Price = 27.00M, IsActive = true, ManufacturerId = 4 },
                new Product { Name = "Black Piggybank (Stealth Edition)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Sleek black piggy", Height = 70, Width = 55, Length = 100, Weight = 1650, Capacity = 1150, Color = "Black", Price = 34.00M, IsActive = true, ManufacturerId = 5 },
                new Product { Name = "Orange Piggybank", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Vivid orange piggy", Height = 55, Width = 40, Length = 85, Weight = 1300, Capacity = 900, Color = "Orange", Price = 23.00M, IsActive = false, ManufacturerId = 5 },
                new Product { Name = "Pink Piggybank (Floral Design)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Pretty pink piggy with floral patterns", Height = 65, Width = 50, Length = 95, Weight = 1500, Capacity = 1050, Color = "Pink", Price = 29.00M, IsActive = true, ManufacturerId = 5 },
                new Product { Name = "Copper Piggybank", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Classic copper piggy", Height = 60, Width = 45, Length = 90, Weight = 1400, Capacity = 1000, Color = "Copper", Price = 26.00M, IsActive = false, ManufacturerId = 6 },
                new Product { Name = "Teal Piggybank", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Stylish teal piggy", Height = 70, Width = 55, Length = 100, Weight = 1650, Capacity = 1150, Color = "Teal", Price = 32.00M, IsActive = false, ManufacturerId = 6 },
                new Product { Name = "Red Piggybank (Limited Edition)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Exclusive red piggy with gold accents", Height = 75, Width = 55, Length = 105, Weight = 1750, Capacity = 1250, Color = "Red", Price = 39.00M, IsActive = false, ManufacturerId = 6 },
                new Product { Name = "Green Piggybank (Jungle Adventure)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Green piggy with jungle-themed decorations", Height = 60, Width = 45, Length = 90, Weight = 1400, Capacity = 1000, Color = "Green", Price = 28.00M, IsActive = true, ManufacturerId = 7 },
                new Product { Name = "Purple Piggybank (Royal Edition)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Luxurious purple piggy with gold trim", Height = 70, Width = 55, Length = 100, Weight = 1600, Capacity = 1120, Color = "Purple", Price = 45.00M, IsActive = true, ManufacturerId = 7 },
                new Product { Name = "Pink Piggybank (Princess Collection)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Pink piggy fit for a princess", Height = 65, Width = 50, Length = 95, Weight = 1500, Capacity = 1050, Color = "Pink", Price = 36.00M, IsActive = false, ManufacturerId = 7 },
                new Product { Name = "Gold Piggybank (Treasure Chest)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Gold piggy with hidden treasure compartment", Height = 80, Width = 60, Length = 110, Weight = 1800, Capacity = 1300, Color = "Gold", Price = 54.99M, IsActive = false, ManufacturerId = 8 },
                new Product { Name = "Blue Piggybank (Underwater Adventure)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Blue piggy with underwater-themed decorations", Height = 75, Width = 55, Length = 105, Weight = 1700, Capacity = 1200, Color = "Blue", Price = 48.00M, IsActive = false, ManufacturerId = 8 },
                new Product { Name = "Silver Piggybank (Shimmering Elegance)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Silver piggy with a shimmering finish", Height = 70, Width = 55, Length = 100, Weight = 1650, Capacity = 1150, Color = "Silver", Price = 41.00M, IsActive = true, ManufacturerId = 8 },
                new Product { Name = "Green Piggybank (Enchanted Forest)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Green piggy inspired by an enchanted forest", Height = 65, Width = 50, Length = 95, Weight = 1550, Capacity = 1100, Color = "Green", Price = 37.00M, IsActive = false, ManufacturerId = 9 },
                new Product { Name = "Red Piggybank (Firefighter Edition)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Red piggy with firefighter-themed decals", Height = 60, Width = 45, Length = 90, Weight = 1450, Capacity = 1050, Color = "Red", Price = 33.00M, IsActive = false, ManufacturerId = 9 },
                new Product { Name = "Black Piggybank (Ninja Stealth)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Sleek black piggy inspired by ninja stealth", Height = 70, Width = 55, Length = 100, Weight = 1600, Capacity = 1150, Color = "Black", Price = 39.00M, IsActive = false, ManufacturerId = 9 },
                new Product { Name = "Orange Piggybank (Tropical Paradise)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Orange piggy with tropical paradise motifs", Height = 55, Width = 40, Length = 85, Weight = 1400, Capacity = 1000, Color = "Orange", Price = 29.00M, IsActive = true, ManufacturerId = 10 },
                new Product { Name = "Copper Piggybank (Antique Collection)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Copper piggy with antique design", Height = 60, Width = 45, Length = 90, Weight = 1450, Capacity = 1050, Color = "Copper", Price = 35.00M, IsActive = false, ManufacturerId = 10 },
                new Product { Name = "Teal Piggybank (Ocean Breeze)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Teal piggy inspired by the ocean breeze", Height = 65, Width = 50, Length = 95, Weight = 1550, Capacity = 1100, Color = "Teal", Price = 38.00M, IsActive = true, ManufacturerId = 10 },
                new Product { Name = "Red Piggybank (Cherry Blossom)", ImageUrl = "/images/products/" + (incr++).ToString().PadLeft(4, '0') + ".jpg", Description = "Red piggy adorned with cherry blossom artwork", Height = 70, Width = 55, Length = 100, Weight = 1600, Capacity = 1150, Color = "Red", Price = 40.00M, IsActive = false, ManufacturerId = 11 },
            }).ForEach(element => _context.Products.Add(element)); _context.SaveChanges();
        }

        private static async Task seedOrders()
        {
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Jean-Michel")).Id, AddressId = 10, OrderStatus = EnumOrderStatus.Sent });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Azerty")).Id, AddressId = 2, OrderStatus = EnumOrderStatus.InProcess });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Malenia")).Id, AddressId = 3, OrderStatus = EnumOrderStatus.Ordered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Malenia")).Id, AddressId = 22, OrderStatus = EnumOrderStatus.InProcess });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Malenia")).Id, AddressId = 12, OrderStatus = EnumOrderStatus.Sent });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Malenia")).Id, AddressId = 35, OrderStatus = EnumOrderStatus.Delivered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Malenia")).Id, AddressId = 29, OrderStatus = EnumOrderStatus.Cancelled });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Malenia")).Id, AddressId = 18, OrderStatus = EnumOrderStatus.Ordered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Jean-Michel")).Id, AddressId = 4, OrderStatus = EnumOrderStatus.Delivered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Andrew")).Id, AddressId = 12, OrderStatus = EnumOrderStatus.Ordered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("George")).Id, AddressId = 2, OrderStatus = EnumOrderStatus.Delivered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Alice")).Id, AddressId = 5, OrderStatus = EnumOrderStatus.Sent });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Bob")).Id, AddressId = 10, OrderStatus = EnumOrderStatus.Cancelled });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Emily")).Id, AddressId = 6, OrderStatus = EnumOrderStatus.InProcess });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("David")).Id, AddressId = 7, OrderStatus = EnumOrderStatus.Sent });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Sophia")).Id, AddressId = 8, OrderStatus = EnumOrderStatus.Ordered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Liam")).Id, AddressId = 9, OrderStatus = EnumOrderStatus.Delivered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Olivia")).Id, AddressId = 10, OrderStatus = EnumOrderStatus.Ordered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Azerty")).Id, AddressId = 11, OrderStatus = EnumOrderStatus.Cancelled });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Jean-Michel")).Id, AddressId = 15, OrderStatus = EnumOrderStatus.Sent });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Azerty")).Id, AddressId = 27, OrderStatus = EnumOrderStatus.InProcess });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Malenia")).Id, AddressId = 32, OrderStatus = EnumOrderStatus.Ordered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Jean-Michel")).Id, AddressId = 18, OrderStatus = EnumOrderStatus.Delivered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Andrew")).Id, AddressId = 34, OrderStatus = EnumOrderStatus.Ordered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("George")).Id, AddressId = 11, OrderStatus = EnumOrderStatus.Delivered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Alice")).Id, AddressId = 20, OrderStatus = EnumOrderStatus.Sent });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Bob")).Id, AddressId = 29, OrderStatus = EnumOrderStatus.Cancelled });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Emily")).Id, AddressId = 40, OrderStatus = EnumOrderStatus.InProcess });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("David")).Id, AddressId = 22, OrderStatus = EnumOrderStatus.Sent });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Sophia")).Id, AddressId = 25, OrderStatus = EnumOrderStatus.Ordered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Liam")).Id, AddressId = 13, OrderStatus = EnumOrderStatus.Delivered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Olivia")).Id, AddressId = 19, OrderStatus = EnumOrderStatus.Ordered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Azerty")).Id, AddressId = 6, OrderStatus = EnumOrderStatus.Cancelled });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Jean-Michel")).Id, AddressId = 37, OrderStatus = EnumOrderStatus.Sent });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Malenia")).Id, AddressId = 8, OrderStatus = EnumOrderStatus.Ordered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Andrew")).Id, AddressId = 16, OrderStatus = EnumOrderStatus.Delivered });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("George")).Id, AddressId = 31, OrderStatus = EnumOrderStatus.InProcess });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Alice")).Id, AddressId = 9, OrderStatus = EnumOrderStatus.Sent });
            _context.Orders.Add(new Order { CreatedAt = DateTime.Now, UserId = (await _userManager.FindByNameAsync("Bob")).Id, AddressId = 28, OrderStatus = EnumOrderStatus.Cancelled });
            _context.SaveChanges();
        }

        private async static Task seedOrderDetails()
        {
            (new List<OrderDetail>{
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 1)).Price, OrderId = 1, ProductId = 1 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 2)).Price, OrderId = 1, ProductId = 2 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 1)).Price, OrderId = 2, ProductId = 1 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 3)).Price, OrderId = 3, ProductId = 3 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 4)).Price, OrderId = 1, ProductId = 3 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 1)).Price, OrderId = 4, ProductId = 1 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 5)).Price, OrderId = 2, ProductId = 4 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 6)).Price, OrderId = 3, ProductId = 2 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 7)).Price, OrderId = 5, ProductId = 5 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 8)).Price, OrderId = 4, ProductId = 7 },
                new OrderDetail { Quantity = 6, Price = 6 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 9)).Price, OrderId = 6, ProductId = 8 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 10)).Price, OrderId = 7, ProductId = 9 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 11)).Price, OrderId = 8, ProductId = 10 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 12)).Price, OrderId = 9, ProductId = 11 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 13)).Price, OrderId = 10, ProductId = 12 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 14)).Price, OrderId = 11, ProductId = 13 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 7)).Price, OrderId = 12, ProductId = 14 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 6)).Price, OrderId = 13, ProductId = 6 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 9)).Price, OrderId = 14, ProductId = 9 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 2)).Price, OrderId = 14, ProductId = 7 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 3)).Price, OrderId = 13, ProductId = 3 },
                new OrderDetail { Quantity = 6, Price = 6 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 8)).Price, OrderId = 12, ProductId = 8 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 5)).Price, OrderId = 11, ProductId = 5 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 1)).Price, OrderId = 10, ProductId = 1 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 4)).Price, OrderId = 9, ProductId = 4 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 12)).Price, OrderId = 8, ProductId = 12 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 11)).Price, OrderId = 7, ProductId = 11 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 10)).Price, OrderId = 6, ProductId = 10 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 9)).Price, OrderId = 5, ProductId = 9 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 8)).Price, OrderId = 4, ProductId = 8 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 7)).Price, OrderId = 3, ProductId = 7 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 6)).Price, OrderId = 2, ProductId = 6 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 5)).Price, OrderId = 1, ProductId = 5 },
                new OrderDetail { Quantity = 6, Price = 6 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 4)).Price, OrderId = 14, ProductId = 4 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 3)).Price, OrderId = 13, ProductId = 3 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 2)).Price, OrderId = 12, ProductId = 2 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 1)).Price, OrderId = 11, ProductId = 1 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 14)).Price, OrderId = 10, ProductId = 14 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 13)).Price, OrderId = 9, ProductId = 13 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 12)).Price, OrderId = 8, ProductId = 12 },
                new OrderDetail { Quantity = 7, Price = 7 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 18)).Price, OrderId = 23, ProductId = 18 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 20)).Price, OrderId = 27, ProductId = 20 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 26)).Price, OrderId = 30, ProductId = 26 },
                new OrderDetail { Quantity = 6, Price = 6 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 22)).Price, OrderId = 17, ProductId = 22 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 29)).Price, OrderId = 19, ProductId = 29 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 30)).Price, OrderId = 15, ProductId = 30 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 28)).Price, OrderId = 26, ProductId = 28 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 24)).Price, OrderId = 31, ProductId = 24 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 25)).Price, OrderId = 28, ProductId = 25 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 27)).Price, OrderId = 39, ProductId = 27 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 19)).Price, OrderId = 35, ProductId = 19 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 16)).Price, OrderId = 24, ProductId = 16 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 23)).Price, OrderId = 37, ProductId = 23 },
                new OrderDetail { Quantity = 6, Price = 6 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 21)).Price, OrderId = 20, ProductId = 21 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 17)).Price, OrderId = 33, ProductId = 17 },
                new OrderDetail { Quantity = 7, Price = 7 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 14)).Price, OrderId = 38, ProductId = 14 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 13)).Price, OrderId = 25, ProductId = 13 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 12)).Price, OrderId = 32, ProductId = 12 },
                new OrderDetail { Quantity = 6, Price = 6 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 11)).Price, OrderId = 21, ProductId = 11 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 10)).Price, OrderId = 34, ProductId = 10 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 9)).Price, OrderId = 36, ProductId = 9 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 8)).Price, OrderId = 22, ProductId = 8 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 7)).Price, OrderId = 29, ProductId = 7 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 6)).Price, OrderId = 31, ProductId = 6 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 5)).Price, OrderId = 18, ProductId = 5 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 4)).Price, OrderId = 27, ProductId = 4 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 3)).Price, OrderId = 19, ProductId = 3 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 2)).Price, OrderId = 28, ProductId = 2 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 1)).Price, OrderId = 30, ProductId = 1 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 15)).Price, OrderId = 37, ProductId = 15 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 18)).Price, OrderId = 15, ProductId = 18 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 20)).Price, OrderId = 23, ProductId = 20 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 26)).Price, OrderId = 16, ProductId = 26 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 22)).Price, OrderId = 34, ProductId = 22 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 29)).Price, OrderId = 25, ProductId = 29 },
                new OrderDetail { Quantity = 6, Price = 6 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 30)).Price, OrderId = 31, ProductId = 30 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 28)).Price, OrderId = 27, ProductId = 28 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 24)).Price, OrderId = 26, ProductId = 24 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 25)).Price, OrderId = 33, ProductId = 25 },
                new OrderDetail { Quantity = 5, Price = 5 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 27)).Price, OrderId = 32, ProductId = 27 },
                new OrderDetail { Quantity = 1, Price = 1 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 19)).Price, OrderId = 35, ProductId = 19 },
                new OrderDetail { Quantity = 4, Price = 4 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 16)).Price, OrderId = 30, ProductId = 16 },
                new OrderDetail { Quantity = 2, Price = 2 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 23)).Price, OrderId = 37, ProductId = 23 },
                new OrderDetail { Quantity = 6, Price = 6 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 21)).Price, OrderId = 36, ProductId = 21 },
                new OrderDetail { Quantity = 3, Price = 3 * (await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 17)).Price, OrderId = 39, ProductId = 17 },
            }).ForEach(element => _context.OrderDetails.Add(element)); _context.SaveChanges();
        }

        private static async Task seedReviews()
        {
            //var allCustomers = _userManager.Users.Where(e => e.AddressId != 1).ToArray();
            //_rand.Next(0, allCustomers.Length);
            _context.Reviews.Add(new Review { Score = 5, Message = "Very good product, would recommend", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.NeedReview, UserId = (await _userManager.FindByNameAsync("Jean-Michel")).Id, ProductId = 1 });
            _context.Reviews.Add(new Review { Score = 1, Message = "Terrible, don't buy this !!!!!!!!!!!!!!", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.NeedReview, UserId = (await _userManager.FindByNameAsync("Azerty")).Id, ProductId = 4 });
            _context.Reviews.Add(new Review { Score = 4, Message = "Good quality, but a bit pricey", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Moderated, UserId = (await _userManager.FindByNameAsync("Malenia")).Id, ProductId = 2 });
            _context.Reviews.Add(new Review { Score = 3, Message = "Decent product, average performance", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Moderated, UserId = (await _userManager.FindByNameAsync("Andrew")).Id, ProductId = 3 });
            _context.Reviews.Add(new Review { Score = 5, Message = "Excellent product, worth every penny", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.NeedReview, UserId = (await _userManager.FindByNameAsync("George")).Id, ProductId = 5 });
            _context.Reviews.Add(new Review { Score = 2, Message = "Not what I expected, disappointed", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Moderated, UserId = (await _userManager.FindByNameAsync("Alice")).Id, ProductId = 6 });
            _context.Reviews.Add(new Review { Score = 4, Message = "Great product, fast delivery", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Hidden, UserId = (await _userManager.FindByNameAsync("Bob")).Id, ProductId = 7 });
            _context.Reviews.Add(new Review { Score = 5, Message = "Outstanding quality, highly recommended", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.NeedReview, UserId = (await _userManager.FindByNameAsync("Emily")).Id, ProductId = 8 });
            _context.Reviews.Add(new Review { Score = 1, Message = "Worst purchase ever, never buying again", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Hidden, UserId = (await _userManager.FindByNameAsync("David")).Id, ProductId = 9 });
            _context.Reviews.Add(new Review { Score = 4, Message = "Satisfied with the product, good value", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Moderated, UserId = (await _userManager.FindByNameAsync("Sophia")).Id, ProductId = 10 });
            _context.Reviews.Add(new Review { Score = 5, Message = "Impressive product, exceeded my expectations", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Hidden, UserId = (await _userManager.FindByNameAsync("Liam")).Id, ProductId = 11 });
            _context.Reviews.Add(new Review { Score = 3, Message = "Average product, nothing special", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.NeedReview, UserId = (await _userManager.FindByNameAsync("Olivia")).Id, ProductId = 12 });
            _context.Reviews.Add(new Review { Score = 4, Message = "Reliable product, happy with the purchase", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Hidden, UserId = (await _userManager.FindByNameAsync("Noah")).Id, ProductId = 1 });
            _context.Reviews.Add(new Review { Score = 5, Message = "Top-notch quality, would buy again", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Moderated, UserId = (await _userManager.FindByNameAsync("Jean-Michel")).Id, ProductId = 2 });
            _context.Reviews.Add(new Review { Score = 2, Message = "Disappointing product, not worth the price", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.NeedReview, UserId = (await _userManager.FindByNameAsync("Jean-Michel")).Id, ProductId = 3 });
            _context.Reviews.Add(new Review { Score = 4, Message = "Good product, could be better", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Hidden, UserId = (await _userManager.FindByNameAsync("Jean-Michel")).Id, ProductId = 4 });
            _context.Reviews.Add(new Review { Score = 3, Message = "Average performance, nothing special", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Moderated, UserId = (await _userManager.FindByNameAsync("Azerty")).Id, ProductId = 5 });
            _context.Reviews.Add(new Review { Score = 4, Message = "Satisfied with the purchase, decent quality", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.Moderated, UserId = (await _userManager.FindByNameAsync("Azerty")).Id, ProductId = 6 });
            _context.Reviews.Add(new Review { Score = 5, Message = "Excellent value for money, highly recommended", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.NeedReview, UserId = (await _userManager.FindByNameAsync("Malenia")).Id, ProductId = 7 });
            _context.Reviews.Add(new Review { Score = 1, Message = "Avoid this product at all costs, terrible", CreatedAt = DateTime.Now, ReviewStatus = EnumReviewStatus.NeedReview, UserId = (await _userManager.FindByNameAsync("Jean-Michel")).Id, ProductId = 8 });
            _context.SaveChanges();
        }

    }
}
