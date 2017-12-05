namespace GobelinsWorld.Web.Infrastructure.Extensions
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<GobelinsWorldDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    var administrator = WebConstants.AdministratorRole;

                    var roleExists = await roleManager.RoleExistsAsync(administrator);

                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = administrator
                        });
                    }

                    var adminEmail = "admin@admin.bg";

                    var adminUser = await userManager.FindByEmailAsync(adminEmail);

                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            UserName = adminEmail,
                            Email = adminEmail,
                            FirstName = "Admin",
                            LastName = "Admin",
                            PhoneNumber = "0888888888",
                            Country = Country.Bulgaria,
                            State = "Sofia",
                            City = "Sofia",
                            ZipCode = "1000",
                            StreetAddress = "Vitosha 16",
                            IsPersonalAccount = true
                        };

                        await userManager.CreateAsync(adminUser, "admin123");

                        await userManager.AddToRoleAsync(adminUser, administrator);
                    }
                })
                .Wait();
            }

            return app;
        }
    }
}
