using hospital.management.system.BLL.Services;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Web;

public class Program
{
    public static async Task  Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("hospital.management.system.DAL")
            ));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddTransient<IAdminService, AdminService>();
        builder.Services.AddTransient<IDoctorService, DoctorService>();
        builder.Services.AddTransient<IPatientService, PatientService>();
        builder.Services.AddTransient<IStaffService, StaffService>();
        builder.Services.AddHttpContextAccessor();

        builder.Services.Configure<IdentityOptions>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            //opt.Password.RequireDigit = false;
            //opt.Password.RequireLowercase = false;
            //opt.Password.RequireNonAlphanumeric = false;
            //opt.Lockout.MaxFailedAccessAttempts = 5;
            //opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            //opt.SignIn.RequireConfirmedEmail = false;
            opt.User.RequireUniqueEmail = true;
       
        });
        builder.Services.AddControllersWithViews();

        var app = builder.Build();
        using var scope = app.Services.CreateScope();
        await DataBaseSeed.SeedDatabaseAsync(scope.ServiceProvider);
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

       await app.RunAsync();
    }
}