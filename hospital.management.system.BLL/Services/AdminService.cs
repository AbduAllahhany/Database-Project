using hospital.management.system.BLL.Models.Admin;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminService(ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<int> CreatePatientAsync(PatientCreateModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            SSN = model.SSN,
            Gender = model.Gender,
        };

        var res1 = await _userManager.CreateAsync(user, "Patient_" + model.SSN);
        var res2 = await _userManager.AddToRoleAsync(user, SD.Patient);
        if (!res1.Succeeded || !res2.Succeeded) return 0;

        string sqlcommand1 = $"INSERT INTO Patient (firstName,lastName,dateOfBirth,address,UserId)" +
                             $@" VALUES (@p0, @p1, @p2,@p3,@p4)";

        int res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1,
            model.FirstName,
            model.LastName,
            model.DateOfBirth,
            model.Address,
            user.Id);

        var query = _context.Patients.FromSql($@"select top(1) * from patient where UserId={user.Id}");
        var patient = query.ToList().FirstOrDefault();
        if (patient == null)
            return 0;

        string sqlcommand2 =
            $"INSERT INTO Patient_Phone (patientId,number)VALUES (@p0, @p1)";
        await _context.Database.ExecuteSqlRawAsync(sqlcommand2, patient.Id, model.PhoneNumber);
        return res;
    }

    public async Task<int> CreateDoctorAsync(DoctorCreateModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            SSN = model.SSN,
            PhoneNumber = model.PhoneNumber,
            Gender = model.Gender,
        };

        var res1 = await _userManager.CreateAsync(user, "Doctor_" + model.SSN);
        var res2 = await _userManager.AddToRoleAsync(user, SD.Doctor);
        if (!res1.Succeeded || !res2.Succeeded) return 0;
        int res = 0;
        try
        {
            string sqlcommand =
                $@"INSERT INTO Doctor (firstName,lastName,specialization,departmentId,workingHours,UserId)" +
                $@"VALUES (@p0, @p1, @p2,@p3,@p4,@p5)";

            res = await _context.Database.ExecuteSqlRawAsync(sqlcommand,
                model.FirstName,
                model.LastName,
                model.Specialization,
                model.DepartmentId,
                model.WorkingHours,
                user.Id);
        }
        catch
        {
            await _userManager.DeleteAsync(user);
            return 0;
        }

        return res;
    }

    public async Task<int> CreateStaffAsync(StaffCreateModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            SSN = model.SSN,
            PhoneNumber = model.PhoneNumber,
            Gender = model.Gender,
        };

        var res1 = await _userManager.CreateAsync(user, "Staff_" + model.SSN);

        var res2 = await _userManager.AddToRoleAsync(user, model.Role);
        if (!res1.Succeeded || !res2.Succeeded) return 0;

        string sqlcommand =
            $@"INSERT INTO Staff (firstName,lastName,role,depId,startSchedule,endSchedule,dayOfWork,UserId)" +
            $@" VALUES (@p0, @p1, @p2,@p3,@p4,@p5,@p6,@p7)";

        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand,
            model.FirstName,
            model.LastName,
            model.Role,
            model.DeptId,
            model.StartSchedule,
            model.EndSchedule,
            model.DayOfWork,
            user.Id);

        return res;
    }

    public async Task<int> CreateVisitAsync(VisitCreateModel model)
    {
        string sqlcommand1 = $@"INSERT INTO  Visit(notes,patientId,reason,date)" +
                             $@" values (@p0, @p1, @p2, @p3)";
        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, model.Notes, model.PatientId, model.Reason,
            model.Date);
        return res;
    }

    public async Task<int> CreateBillAsync(BillCreateModel model)
    {
        string sqlcommand1 = $@"INSERT INTO  Bill(date,totalAmount,paidAmount,patientId)" +
                             $@" values (@p0, @p1, @p2, @p3)";
        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, model.TotalAmount,
            model.PaidAmount,
            model.PatientId);
        return res;
    }

    public async Task<int> CreateAdmissionAsync(AdmissionCreateModel model)
    {
        string sqlcommand1 = $@"INSERT INTO  Addmission(startDate,startDate,roomId,patientId)"
                             + $@"values (@p0, @p1, @p2, @p3)";
        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, model.StartDate,
            model.EndDate,
            model.RoomId,
            model.PatientId);
        return res;
    }

    public async Task<int> CreateInsuranceAsync(InsuranceCreateModel model)
    {
        string sqlcommand1 = $@"INSERT INTO  Insurance(providerName,policyNumber,coverageMoney,expiryDate,patientId)" +
                             $@" values (@p0, @p1, @p2, @p3)";
        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, model.ProviderName,
            model.PolicyNumber,
            model.CoverageMoney,
            model.ExpiryDate,
            model.PatientId);
        return res;
    }

    public async Task<int> CreateEmergencyContactAsync(EmergencyContactCreateModel model)
    {
        string sqlcommand1 = $@"INSERT INTO  Emergency_Contact(name,phone,relationship,patientId)" +
                             $@" values (@p0, @p1, @p2, @p3)";
        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, model.Name,
            model.Phone,
            model.RelationShip,
            model.PatientId);
        return res;
    }

    public async Task<IEnumerable<GetUpcomingAppointmentResponseModel>> GetUpcomingAppointmentAsync()
    {
        var res = _context.Database.SqlQuery<GetUpcomingAppointmentResponseModel>(
            $"""
             SELECT a.Id,d.firstName as DoctorFirstName, d.lastName as DoctorLastName,p.firstName as PatientFirstName , p.lastName as PatientLastName , a.status,a.date
             FROM Patient_Doctor_Appointment a, Doctor d, Patient p
             WHERE a.patientId = p.Id AND a.DoctorId = d.Id AND a.Status = {SD.Confirmed}
             ORDER BY a.date ASC 
             """);
        return await res.ToListAsync();
    }

    public async Task<IEnumerable<GetAllAppointmentsResponseModel>> GetAllAppointmentsAsync()
    {
        var res = _context.Database.SqlQuery<GetAllAppointmentsResponseModel>(
            $"""
             SELECT a.Id,d.firstName as DoctorFirstName, d.lastName as DoctorLastName,p.firstName as PatientFirstName , p.lastName as PatientLastName , a.status,a.date
             FROM Patient_Doctor_Appointment a, Doctor d, Patient p
             WHERE a.patientId = p.Id AND a.DoctorId = d.Id 
             ORDER BY a.date ASC 
             """);
        return await res.ToListAsync();
    }

    public async Task<IEnumerable<GetAllAdminsResponseModel>> GetAllAdminsAsync()
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(SD.Admin);
        IEnumerable<GetAllAdminsResponseModel> admins = usersInRole.Select(u => new GetAllAdminsResponseModel
        {
            UserName = u.UserName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
        });
        return admins;
    }

    // new feature => available time for doctor
    public async Task<int> CreateAppointmentAsync(AppointmentAddModel model)
    {
        string sqlcommand1 =
            $@"INSERT INTO  Patient_Doctor_Appointment (patientId,doctorId,status,reason,date)" +
            $@" values (@p0, @p1, @p2, @p3,@p4)";
        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, model.PatientId,
            model.DoctorId,
            model.Status,
            model.Date,
            model.PatientId);
        return res;
    }

    //to be added to patient
    public async Task<int> AddPatientPhoneNumberAsync(Guid? patientId, string phoneNumber)
    {
        if (patientId == null) return 0;
        var temp = await _context.PatientPhones
            .FromSql($@"select * from PatientPhone where PatientId = {patientId}")
            .ToListAsync();
        var patientPhones = temp.Select(p => p.Number).ToList();

        if (patientPhones.Contains(phoneNumber) || patientPhones.Count < 3)
            return 0;

        string sqlcommand1 =
            $@"INSERT INTO Patient_Phone VALUES (@p0 ,@p1)";

        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, patientId, phoneNumber);
        return res;
    }

    public async Task<int> EditPatientAsync(AdminEditPatientModel? model)
    {
        if (model == null) return 0;

        Guid? patientId = model.PatientId;
        if (patientId == null) return 0;

        var user = await _userManager.FindByEmailAsync(model.Email);

        //update 
        user.UserName = model.UserName;
        user.Email = model.Email;
        user.SSN = model.SSN;
        string sqlcommand1 =
            $@"Update Patient 
               SET firstName =@p0,lastName=@p1,dateOfBirth=@p2,address = @p3 
               where Id=@p4";

        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, model.FirstName, model.LastName,
            model.DateOfBirth, model.Address, patientId);
        return 1;
    }

    public async Task<int> EditDoctorAsync(AdminDoctorEditModel? model)
    {
        if (model == null) return 0;
        Guid? doctorId = model.DoctorId;
        if (doctorId == null) return 0;
        var user = await _userManager.FindByEmailAsync(model.Email);
        //update 
        user.UserName = model.UserName;
        user.Email = model.Email;
        user.SSN = model.SSN;
        string sqlcommand1 =
            $@"Update Dotor 
             SET firstName =@p0,lastName=@p1,specialization=@p2,departmentId = @p3 workingHours=@p4
             where Id=@p5";

        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, model.FirstName, model.LastName,
            model.DateOfBirth, model.Address, doctorId);
        return 1;
    }

    public async Task<int> EditStaffAsync(AdminEditStaffModel model)
    {
        if (model == null) return 0;
        Guid? staffId = model.StaffId;
        if (staffId == null) return 0;
        var user = await _userManager.FindByEmailAsync(model.Email);
        //update 
        user.UserName = model.UserName;
        user.Email = model.Email;
        user.SSN = model.SSN;

        string sqlcommand1 =
            $@"Update Staff SET firstName =@p0,lastName=@p1,role=@p2,depttId = @p3,
                                startSchedule=@p4, endSchedule=@p5,dayOfWork=@p6
                                where Id=@p7";

        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1,
            model.FirstName,
            model.LastName,
            model.DateOfBirth,
            model.Address,
            staffId);
        return 1;
    }

    //to be added in other service
    public async Task<int> GetDoctorsCountAsync()
    {
        var res = _context.Database.SqlQuery<int>($@"select count(*) from Doctor");
        var count = await res.ToListAsync();
        return count.FirstOrDefault();
    }

    public async Task<int> GetPateintCountAsync()
    {
        var res = _context.Database.SqlQuery<int>($@"select count(*) from Patient");
        var count = await res.ToListAsync();
        return count.FirstOrDefault();
    }

    public async Task<int> GetStaffCountAsync()
    {
        var res = _context.Database.SqlQuery<int>($@"select count(*) from Staff");
        var count = await res.ToListAsync();
        return count.FirstOrDefault();
    }

    public async Task<int> GetAppointmentCountAsync()
    {
        var res = _context.Database.SqlQuery<int>($@"select count(*) from Patient_Doctor_Appointment");
        var count = await res.ToListAsync();
        return count.FirstOrDefault();
    }


    public async Task<IEnumerable<Patient>> Test()
    {
        //not sending query to database => IQueryable
        //IQueryable<Patient> query = _context.Patients.FromSql($"SELECT * FROM Patient");
        // return IQueryable of the specific DTO
        //var res1 = _context.Database.SqlQuery<string>($"SELECT firstName FROM Patient").ToList();
        //var res2 = _context.Database.SqlQuery<modeltest>(
        //  $@"SELECT Id,firstName , lastName FROM Patient").ToList();
        //string sql1 = "INSERT INTO Patient (firstName,lastName,gender,dateOfBirth,address)"
        //           + $"VALUES (@p0, @p1, @p2,@p3,@p4)";
        //for delete or insert or update => return number of row affected
        //var res3 = _context.Database.ExecuteSqlRaw(sql1, "Khattab", "khattab", true, DateTime.Now, "ASaffas");
        //sending query
        //var test = _context.Patients.ToList();
        //string sql2 = "Delete FROM Patient where Id= (@p0)";
        //var res4 = _context.Database.ExecuteSqlRaw(sql2, 6);
        //var email = "admin@admin.com";
        //var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
        //var user1   = await _userManager.FindByIdAsync(email);
        //var  patient1 =  _context.Patients.FromSql($@"select *  from patient where UserId={user.Id}");


        return null;
    }
}