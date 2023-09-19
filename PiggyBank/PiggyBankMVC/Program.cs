using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;

namespace PiggyBankMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add SQL connection
            builder.Services.AddDbContext<PiggyContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PiggyContext")));

            var app = builder.Build();



            // Seed DB
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedGenerator.Initialize(services);
            }



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}