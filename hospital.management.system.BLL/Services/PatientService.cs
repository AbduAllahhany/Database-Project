

using hospital.management.system.BLL.Models.Patient;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Services;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ApplicationDbContext _context;

    public PatientService(IUnitOfWork _unitOfWork, ApplicationDbContext _context)
    {
        unitOfWork = _unitOfWork;
        this._context = _context;
    }


    public List<PatientAppointment> GetPatientAppointments(Guid patientId)
    {

        if (patientId == null || patientId == Guid.Empty) return null;

        List<PatientAppointment> result = _context.Database.SqlQuery<PatientAppointment>(
            $@"select a.Id,a.status,reason   ,concat(p.firstName,' ',p.lastName) as PatientName , concat(d.firstName,' ',d.lastName) as DoctorName, a.date 
                  from Patient_Doctor_Appointment a ,  Patient p , dbo.Doctor d
                  where a.patientId=p.Id and d.Id = a.doctorId
                  and  a.patientId={patientId}").ToList();

        if (result.Count() == 0) return null;
        return result;

    }

    public List<PatientBill> GetPatientBills(Guid patientId)
    {

        if (patientId == null || patientId == Guid.Empty) return null;

        List<PatientBill> result = _context.Database.SqlQuery<PatientBill>(
            $@"select  b.Id ,concat(p.firstName,' ',p.lastName) as PatientName , b.totalAmount , b.paidAmount , b.remaining  
                from dbo.Bill b inner join dbo.Patient p 
                on b.patientId=p.Id
                where b.patientId={patientId}").ToList();

        if (result.Count() == 0) return null;
        return result;

    }

    public List<PatientMedicalRecord> GetPatientMedicalRecord(Guid patientId)
    {
        if (patientId == null || patientId == Guid.Empty) return null;

        List<PatientMedicalRecord> result = _context.Database.SqlQuery<PatientMedicalRecord>(
            $@"select concat(p.firstName,' ',p.lastName) as PatientName ,concat(d.firstName,' ',d.lastName) as DoctorName , m.prescription , m.diagnostic , m.dateOfRecording 
                from dbo.Medical_Record m , dbo.Patient p , dbo.Doctor d
                where m.patientId=p.Id and m.doctorId = d.Id
                and m.patientId={patientId}").ToList();

        if (result.Count() == 0) return null;
        return result;
    }

    public List<PatientVisit> GetPatientVisits(Guid patientId)
    {
        if (patientId == null || patientId == Guid.Empty) return null;

        List<PatientVisit> result = _context.Database.SqlQuery<PatientVisit>(
            $@"select concat(p.firstName,' ',p.lastName) as PatientName ,v.date , v.notes , v.reason 
                from dbo.Visit v inner join dbo.Patient p 
                 on v.patientId=p.Id 
                 where v.patientId={patientId}").ToList();

        if (result.Count() == 0) return null;
        return result;
    }

    public PatientRoom GetRoomStatus(Guid patientId)
    {
        if (patientId == null || patientId == Guid.Empty) return null;

        PatientRoom? result = _context.Database.SqlQuery<PatientRoom>(
            $@"select concat(p.firstName,' ',p.lastName) as PatientName ,a.startDate , a.endDate ,r.costPerDay  , r.roomNumber , r.type , r.status
                from dbo.Room r , dbo.Admission a , dbo.Patient p
                where r.Id =a.roomId and p.Id = a.patientId 
                and p.Id={patientId}").FirstOrDefault();

        if (result == null) return null;
        return result;
    }

    public ViewInsurance GetViewInsurance(Guid patientId)
    {
        if (patientId == null || patientId == Guid.Empty) return null;

        ViewInsurance? result = _context.Database.SqlQuery<ViewInsurance>(
            $@"select  i.Id , concat(p.firstName,' ',p.lastName) as PatientName , i.expiryDate , i.coverageMoney , i.policyNumber , i.providerName 
                from dbo.Insurance i inner join dbo.Patient p 
                on i.patientId=p.Id 
                where i.patientId={patientId}").FirstOrDefault();

        if (result == null) return null;
        return result;
    }

    public List<PatientEmergancyContact> GetPatientEmergancyContacts(Guid patientId)
    {
        if (patientId == null || patientId == Guid.Empty) return null;

        List<PatientEmergancyContact> result = _context.Database.SqlQuery<PatientEmergancyContact>(
            $@" select e.Id ,  concat(p.firstName,' ',p.lastName) as PatientName , e.name , e.phone , e.relationship 
                from dbo.Emergency_Contact e inner join dbo.Patient p 
                on e.patientId=p.Id
                where e.patientId={patientId}").ToList();

        if (result.Count() == 0) return null;
        return result;
    }

    public async Task<GetPatientProfileModel> GetPatientByUserId(Guid? Id)
    {
        var res =  _context.Database.SqlQuery<GetPatientProfileModel>($"""
                                                                            SELECT Top(1) firstName as FirstName, lastName as LastName, dateOfBirth as Birthdate, 
                                                                            bloodGroup as BloodGroup, allergies as Allergies,chronicDiseases as ChronicDiseases,
                                                                            address as Address 
                                                                            from Patient
                                                                            where Id = {Id}
                                                                            """);
        var temp = await res.ToListAsync();
        return res.FirstOrDefault() ?? null;
    }



    public List<Patient> GetAllPetient()
        { 
            List<Patient> patients = _context.Patients.FromSql($@"select * from dbo.Patient").ToList();
            
           // if(patients.Count==0) return null;
            return patients;
        }
        
        // public void MarkPatientAppoinment(Appointment appoinment)
        // {
        //     string query = $@"INSERT INTO Patient_Doctor_Appointment (patientId, doctorId, status, reason, date) VALUES
        //                         (@patientId, @doctorId, @status, @reason, @date)"; 
        //     _context.Database.ExecuteSqlRaw(query, appoinment.PatientId, appoinment.DoctorId, appoinment.Status, appoinment.Reason , appoinment.Date);
        //     _context.SaveChanges();
        // }
    }
 
 

 
 
 
 
 
 