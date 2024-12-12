
using hospital.management.system.Models.Entities;
using hospital.management.system.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace hospital.management.system.DAL.Persistence;

public static class DataBaseSeed
{
    private static async Task SeedDatabaseAsync(ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        if (!userManager.Users.Any())
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = SD.Admin,
                NormalizedName = SD.Admin.ToUpper(),
            });
            await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = SD.Nurse,
                NormalizedName = SD.Nurse.ToUpper(),
            });
            await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = SD.SuperAdmin,
                NormalizedName = SD.SuperAdmin.ToUpper(),
            });
            await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = SD.Doctor,
                NormalizedName = SD.Patient.ToUpper(),
            });
            await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = SD.Patient,
                NormalizedName = SD.Patient.ToUpper(),
            });

            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@admin.com",
                SSN = "123456",
                EmailConfirmed = true,
                Gender = Gender.Male,
            };
            await userManager.CreateAsync(admin, "Admin123.?");

            var doctor = new ApplicationUser
            {
                UserName = "doctor",
                SSN = "123456",
                Email = "Doctor@admin.com",
                EmailConfirmed = true,
                Gender = Gender.Male,
            };
            await userManager.CreateAsync(doctor, "Admin123.?");

            await userManager.AddToRoleAsync(admin, SD.Admin);
            await userManager.AddToRoleAsync(doctor, SD.Doctor);

            context.Doctors.Add(new Doctor()
            {
                User = doctor,
                FirstName = "Doctor",
                UserId = doctor.Id,
            });
        }


        await context.SaveChangesAsync();
    }

public static async Task SeedDatabaseAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        if (context.Database.IsSqlServer()) await context.Database.MigrateAsync();

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        await SeedDatabaseAsync(context, userManager, roleManager);
    }
}



