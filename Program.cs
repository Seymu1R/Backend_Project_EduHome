using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EduHomeProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                builder => {
                    builder.MigrationsAssembly(nameof(EduHomeProject));
                }));
            builder.Services.AddIdentity<User, IdentityRole>(options => 
            {
                options.Lockout.MaxFailedAccessAttempts= 5;
                options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromSeconds(5);
                options.Password.RequireLowercase= false;
                options.Password.RequireUppercase= false;
                options.Password.RequireNonAlphanumeric= false;
                options.User.RequireUniqueEmail= true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.Configure<AdminUser>(builder.Configuration.GetSection("AdminUser"));
            Constants.RootPath = builder.Environment.WebRootPath;
            Constants.SliderImagePath = Path.Combine(Constants.RootPath, "admin", "assets", "img", "slider");
            Constants.TeacherImagePath = Path.Combine(Constants.RootPath, "admin", "assets", "img", "teacher");
            Constants.TeacherSosialLogoPath = Path.Combine(Constants.RootPath, "admin", "assets", "img", "sosiallogo");
            Constants.CourseImagePath= Path.Combine(Constants.RootPath, "admin", "assets", "img", "course");
            Constants.EventImagePath = Path.Combine(Constants.RootPath, "admin", "assets", "img", "event");
            Constants.BlogImagePath = Path.Combine(Constants.RootPath, "admin", "assets", "img", "blog");
            Constants.SpeakerImagePath = Path.Combine(Constants.RootPath, "admin", "assets", "img", "speaker");
            Constants.FooterLeftSideImagePath = Path.Combine(Constants.RootPath, "admin", "assets", "img", "footer");
            Constants.HeaderLogoImagePath = Path.Combine(Constants.RootPath, "admin", "assets", "img", "header");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            using (var scope = app.Services.CreateScope()) {
                var serviceProvider = scope.ServiceProvider; ;
                var dataInthilaizer = new DataInitializer(serviceProvider);
                await dataInthilaizer.SeedData();
            }
                app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });



            await app.RunAsync();
        }
    }
}