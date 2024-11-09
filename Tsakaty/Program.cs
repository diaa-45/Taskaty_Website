
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tsakaty.Models;
using Tsakaty.Repository;

namespace Tsakaty
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ITaskRepoitory,TaskRepository>();
            builder.Services.AddScoped<IUserRepository,UserRepository>();

            var connectionString =builder.Configuration.GetConnectionString("cs")
                ?? throw new Exception("Connection string 'cs' not found."); ;
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString)
            );
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireLowercase=false;
                option.Password.RequireUppercase=false;
                option.Password.RequireNonAlphanumeric=false;
                option.Password.RequiredLength = 4;
                option.Password.RequireDigit=false;
                option.User.RequireUniqueEmail=true;
            })
                .AddEntityFrameworkStores<AppDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var roleManger= scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManger= scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                if (!await roleManger.RoleExistsAsync("Admin"))
                {
                    await roleManger.CreateAsync(new IdentityRole("Admin"));
                }
                var admin= await userManger.FindByEmailAsync("admin@gmail.com");
                if(admin == null)
                {
                   admin = new ApplicationUser
                   {
                       Email= "admin@gmail.com",
                       Address="Al a`rish",
                       UserName="admin"
                   };
                    IdentityResult result= await userManger.CreateAsync(admin, "user");
                    if (result.Succeeded)
                    {
                        await userManger.AddToRoleAsync(admin, "Admin");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create admin user.");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error: {error.Description}");
                        }
                    }
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }

        private static Exception Exception()
        {
            throw new NotImplementedException();
        }
    }
}
