using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Services;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ApplicationDbContext _context;

    public DoctorService(IUnitOfWork _unitOfWork,ApplicationDbContext _context)
    {
        unitOfWork = _unitOfWork;
        this._context = _context;
    }

    public IEnumerable<Doctor> GetPendingAppointments(Guid loggedDoctorId)
    {
        IQueryable<Doctor> query = _context.Doctors.FromSqlInterpolated($@"
            SELECT P.firstName + ' ' + P.lastName as 'Full Name', DATEDIFF(year, P.dateOfBirth, GETDATE()) as 'Age', P.bloodGroup, A.reason, A.date, A.time
            FROM Patient P 
            INNER JOIN Patient_Doctor_Appointment A
            ON A.patientId = P.Id AND A.doctorId = {loggedDoctorId}
            WHERE LOWER(A.status) = 'pending'
            ORDER BY A.date");
        return query.ToList();
    }

    public IEnumerable<Doctor> GetDailyAppointments(Guid loggedDoctorId)
    {
        IQueryable<Doctor> query = _context.Doctors.FromSqlInterpolated($@"
            SELECT P.firstName + ' ' + P.lastName as 'Full Name', DATEDIFF(year, P.dateOfBirth, GETDATE()) as 'Age', P.bloodGroup, A.reason, A.date, A.time
            FROM Patient P 
            INNER JOIN Patient_Doctor_Appointment A
            ON A.patientId = P.Id AND A.doctorId = {loggedDoctorId}
            WHERE LOWER(A.status) = 'pending' AND A.date = CONVERT (date, GETDATE())
            ORDER BY A.date");
        return query.ToList();
    }
    
    public IEnumerable<Doctor> GetUpcomingAppointments(Guid loggedDoctorId)
    {
        IQueryable<Doctor> query = _context.Doctors.FromSqlInterpolated($@"
            SELECT P.firstName + ' ' + P.lastName as 'Full Name', DATEDIFF(year, P.dateOfBirth, GETDATE()) as 'Age', P.bloodGroup, A.reason, A.date, A.time
            FROM Patient P 
            INNER JOIN Patient_Doctor_Appointment A
            ON A.patientId = P.Id AND A.doctorId = {loggedDoctorId}
            WHERE LOWER(A.status) = 'pending' AND A.date > CONVERT (date, GETDATE())
            ORDER BY A.date");
        return query.ToList();
    }
    
    public IEnumerable<Doctor> GetMonthlyAppointmentSummary(Guid loggedDoctorId)
    {
        IQueryable<Doctor> query = _context.Doctors.FromSqlInterpolated($@"
            SELECT COUNT(*) AS TotalAppointments,
                SUM(CASE WHEN LOWER(status) = 'approved' THEN 1 ELSE 0 END) AS ApprovedAppointments,
                SUM(CASE WHEN LOWER(status) = 'pending' THEN 1 ELSE 0 END) AS PendingAppointments,
                SUM(CASE WHEN LOWER(status) = 'reject' THEN 1 ELSE 0 END) AS RejectedAppointments
            FROM Appointment A
            WHERE A.doctorId = {loggedDoctorId} AND MONTH(A.date) = MONTH(getDate()) AND YEAR(date) = YEAR(getDate())");
        return query.ToList();
    }
    
    public int ApproveNextAppointment(Guid loggedDoctorId) 
    {
        var sql = $@"
            UPDATE Patient_Doctor_Appointment
            SET status = 'Approved'
            WHERE Id IN (
	            SELECT TOP(1) A.Id
	            FROM Patient P, Patient_Doctor_Appointment A
	            WHERE A.patientId = P.Id AND A.doctorId = {0} AND LOWER(A.status) = 'pending' AND A.date = CONVERT (date, GETDATE())
	            ORDER BY A.date, A.time
            )";
        var res = _context.Database.ExecuteSqlRaw(sql, loggedDoctorId);
        return res;
    }

    public int PostponingAppointment(Guid loggedDoctorId)
    {
        var sql = $@"
            UPDATE Patient_Doctor_Appointment
            SET date = DATEADD(DAY, 1,date)
            WHERE Id IN (
	            SELECT A.Id
	            FROM Patient P, Patient_Doctor_Appointment A
	            WHERE A.patientId = P.Id AND A.doctorId = {0} AND LOWER(A.status) = 'pending' AND A.date = CONVERT (date, GETDATE())
            )";
        var res = _context.Database.ExecuteSqlRaw(sql, loggedDoctorId);
        return res;
    }

    public int CancelingAppointment(DoctorCancelingAppointmentModel model)
    {
        // dhsajhdkasjhdkjsahdkjsads
        var sql = $@"
            UPDATE Patient_Doctor_Appointment
            SET status = 'Rejected'
            WHERE Id IN (
	            SELECT A.Id
	            FROM Patient P, Patient_Doctor_Appointment A
	            WHERE A.patientId = {0} AND A.doctorId = {1} AND LOWER(A.status) = 'pending'
            )";
        var res = _context.Database.ExecuteSqlRaw(sql, model.SelectedPatientId, model.LoggedDoctorId);
        return res;
    }

    public int FollowUpAppointment(FollowUpAppointmentModel model)
    {
        var sql = $@"
            INSERT INTO Patient_Doctor_Appointment(patientId, doctorId, reason, date, time) 
            VALUES (@p1, @p2, @p3, @p4, @p5))";
        var res = _context.Database.ExecuteSqlRaw(sql, model.PatientId, model.DoctorId, model.Reason, model.AppointmentDate, model.AppointmentTime);
        return res;
    }
    
    public IEnumerable<Doctor> GetNextAppointmentInfo(Guid loggedDoctorId)
    {
        IQueryable<Doctor> query = _context.Doctors.FromSqlInterpolated($@"            
	        SELECT TOP(1) P.firstName + ' ' + P.lastName as 'Full Name', DATEDIFF(year, P.dateOfBirth, GETDATE()) as 'Age', A.reason
	        FROM Patient P, Patient_Doctor_Appointment A
	        WHERE A.patientId = P.Id AND A.doctorId = {loggedDoctorId} AND LOWER(A.status) = 'pending' AND A.date = CONVERT (date, GETDATE())
	        ORDER BY A.date");
        return query.ToList();
    }

    public int CreateMedicalRecord(MedicalRecordModel model) 
    {
        var sql = @"            
            INSERT INTO medical_record(dateOfRecording, diagnostic, prescription, doctorId, patientId)
            VALUES (CONVERT (date, GETDATE()), @p1, @p2, @p3, @p4)";
        var res = _context.Database.ExecuteSqlRaw(sql, model.Diagnostic, model.Prescription, model.LoggedDoctorId, model.SelectedPatientId);
        return res;
    }
}

