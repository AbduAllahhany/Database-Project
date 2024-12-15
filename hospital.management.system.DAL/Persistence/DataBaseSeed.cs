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
      
        if (!context.Rooms.Any())
        {
            var roomTypes = new[] { "Single", "Double", "VIP", "Suite" }; // Room types
            var random = new Random();
            for (int i = 1; i <= 100; i++) // Create 50 rooms
            {
                var costPerDay =
                    (decimal)(random.Next(100, 500) + random.NextDouble()); // Random cost between 100 and 500
                var roomType = roomTypes[random.Next(roomTypes.Length)]; // Random room type
                var roomNumber = i; // Sequential room number (1 to 50)

                // Create a new room with default status as true (available)
                context.Rooms.Add(new Room
                {
                    RoomNumber = roomNumber,
                    CostPerDay = costPerDay,
                    Type = roomType,
                    Status = true // All rooms are available by default
                });
            }

            await context.SaveChangesAsync();
        }

        if (!context.Departments.Any())
        {
            var departments = new List<Department>
            {
                new Department
                {
                    Name = "Emergency",
                    Description =
                        "The Emergency Department provides immediate treatment for acute illnesses or injuries."
                },
                new Department
                {
                    Name = "Cardiology",
                    Description =
                        "The Cardiology Department specializes in the diagnosis and treatment of heart diseases."
                },
                new Department
                {
                    Name = "Neurology",
                    Description =
                        "The Neurology Department focuses on the diagnosis and treatment of brain and nervous system disorders."
                },
                new Department
                {
                    Name = "Orthopedics",
                    Description =
                        "The Orthopedics Department deals with conditions involving the musculoskeletal system."
                },
                new Department
                {
                    Name = "Pediatrics",
                    Description =
                        "The Pediatrics Department provides medical care for children from birth to adolescence."
                },
                new Department
                {
                    Name = "Dermatology",
                    Description =
                        "The Dermatology Department specializes in the diagnosis and treatment of skin conditions."
                },
                new Department
                {
                    Name = "Psychiatry",
                    Description = "The Psychiatry Department focuses on the treatment of mental health disorders."
                },
                new Department
                {
                    Name = "General Surgery",
                    Description =
                        "The General Surgery Department provides surgical care for a wide range of conditions."
                },
                new Department
                {
                    Name = "Radiology",
                    Description =
                        "The Radiology Department uses imaging technologies to diagnose and treat medical conditions."
                },
                new Department
                {
                    Name = "Anesthesiology",
                    Description =
                        "The Anesthesiology Department focuses on providing anesthesia and pain management during surgeries."
                },
                new Department
                {
                    Name = "Oncology",
                    Description = "The Oncology Department specializes in the diagnosis and treatment of cancer."
                },
                new Department
                {
                    Name = "Gynecology",
                    Description =
                        "The Gynecology Department focuses on the health of the female reproductive system."
                },
                new Department
                {
                    Name = "Urology",
                    Description =
                        "The Urology Department deals with conditions related to the urinary tract and male reproductive system."
                },
                new Department
                {
                    Name = "Nephrology",
                    Description =
                        "The Nephrology Department focuses on the diagnosis and treatment of kidney-related diseases."
                },
                new Department
                {
                    Name = "Pathology",
                    Description =
                        "The Pathology Department provides diagnostic services based on laboratory analysis of tissues and fluids."
                }
            };
            context.Departments.AddRange(departments);
            await context.SaveChangesAsync();
        }
        
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
                Name = SD.Intern,
                NormalizedName = SD.Intern.ToUpper(),
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
                LastName = "Doctor",
                WorkingHours = 4,
                Salary = default,
                Specialization = "test",
                DepartmentId = SD.Departments.FirstOrDefault().Value,
                UserId = doctor.Id,
                EndSchedule = default,
                StartSchedule = default,
            });
            await context.SaveChangesAsync();
        }
        var keyValues = context.Departments.ToDictionary(d => d.Name, d => d.Id);
        SD.Departments = keyValues;
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