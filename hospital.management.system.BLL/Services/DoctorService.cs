﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hospital.management.system.BLL.Models.Admin;
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Services;

[Authorize(Roles = SD.Doctor)]
public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DoctorService(IUnitOfWork _unitOfWork, ApplicationDbContext _context,
        UserManager<ApplicationUser> userManager)
    {
        unitOfWork = _unitOfWork;
        this._context = _context;
        _userManager = userManager;
    }

    public IEnumerable<DoctorAppoinment> GetPendingAppointments(Guid loggedDoctorId)
    {
        //var sql = $@"";
        //var sql = $@"";
        IEnumerable<DoctorAppoinment> query = _context.Database.SqlQuery<DoctorAppoinment>($@"
            SELECT A.Id as AppId, p.Id as PatientId ,concat(P.firstName , ' ' , P.lastName) as FullName, DATEDIFF(YEAR, p.dateOfBirth, GETDATE()) as dateOfBirth, A.reason, A.date , A.status
            FROM Patient P 
            INNER JOIN Patient_Doctor_Appointment A
            ON A.patientId = P.Id AND A.doctorId = {loggedDoctorId}
            WHERE LOWER(A.status) = 'pending' and A.date >= CONVERT (date, GETDATE())
            ORDER BY A.date").ToList();

        return query;
    }

    public IEnumerable<DoctorAppoinment> GetDailyAppointments(Guid loggedDoctorId)
    {
        IEnumerable<DoctorAppoinment> query = _context.Database.SqlQuery<DoctorAppoinment>($@"
            SELECT A.Id as AppId,p.Id as PatientId ,concat(P.firstName , ' ' , P.lastName) as FullName,  DATEDIFF(YEAR, p.dateOfBirth, GETDATE()) as dateOfBirth, A.reason, A.date , A.status
            FROM Patient P 
            INNER JOIN Patient_Doctor_Appointment A
            ON A.patientId = P.Id AND A.doctorId = {loggedDoctorId}
            WHERE  A.date = CONVERT (date, GETDATE())
            ORDER BY A.date").ToList();
        return query;
    }

    public IEnumerable<DoctorAppoinment> GetUpcomingAppointments(Guid loggedDoctorId)
    {
        //var sql = ;
        IEnumerable<DoctorAppoinment> query = _context.Database.SqlQuery<DoctorAppoinment>($@"
            SELECT A.Id as AppId,p.Id as PatientId ,concat(P.firstName , ' ' , P.lastName) as FullName,  DATEDIFF(YEAR, p.dateOfBirth, GETDATE()) as dateOfBirth, A.reason, A.date, A.status
            FROM Patient P 
            INNER JOIN Patient_Doctor_Appointment A
            ON A.patientId = P.Id AND A.doctorId = {loggedDoctorId}
            WHERE LOWER(A.status) = 'pending' AND A.date > CONVERT (date, GETDATE())
            ORDER BY A.date").ToList();
        return query;
    }

    public IEnumerable<DoctorMonthlyAppointmentSummary> GetMonthlyAppointmentSummary(Guid loggedDoctorId)
    {
        //var sql = ;
        var query = _context.Database
            .SqlQuery<DoctorMonthlyAppointmentSummary>($@"
            SELECT COUNT(*) AS TotalAppointments,
                SUM(CASE WHEN LOWER(status) = 'approved' THEN 1 ELSE 0 END) AS ApprovedAppointments,
                SUM(CASE WHEN LOWER(status) = 'pending' THEN 1 ELSE 0 END) AS PendingAppointments,
                SUM(CASE WHEN LOWER(status) = 'rejected' THEN 1 ELSE 0 END) AS RejectedAppointments,
                SUM(CASE WHEN LOWER(status) = 'completed' THEN 1 ELSE 0 END) AS CompletedAppointments
            FROM Patient_Doctor_Appointment A
            WHERE A.doctorId = {loggedDoctorId} AND MONTH(A.date) = MONTH(getDate()) AND YEAR(date) = YEAR(getDate())");
        return query.ToList();
    }

    public int ApproveNextAppointment(Guid appointmentId)
    {
        var sql = $"""
                               UPDATE Patient_Doctor_Appointment
                               SET status = 'Approved'
                               WHERE Id = @p0
                   """;
        var res = _context.Database.ExecuteSqlRaw(sql, appointmentId);
        return res;
    }

    public int PostponingAppointment(Guid loggedDoctorId)
    {
        var sql = $"""
                           UPDATE Patient_Doctor_Appointment
                           SET date = DATEADD(DAY, 1, date)
                           WHERE Id IN (
                               SELECT A.Id
                               FROM Patient P, Patient_Doctor_Appointment A
                               WHERE A.patientId = P.Id 
                                 AND A.doctorId = @p0
                                 AND LOWER(A.status) = 'pending' 
                                 AND A.date = CONVERT(date, GETDATE()) )
                   """;
        var res = _context.Database.ExecuteSqlRaw(sql, loggedDoctorId);
        return res;
    }

    public int CancelingAppointment(Guid Id)
    {
        var sql = $"""
                               UPDATE Patient_Doctor_Appointment
                               SET status = 'Rejected'
                               WHERE Id=@p0
                   """;
        var res = _context.Database.ExecuteSqlRaw(sql,Id);
        return res;
    }

    public int FollowUpAppointment(FollowUpAppointmentModel model)
    {
        if (!_context.Patients.Any(p => p.Id == model.PatientId))
        {
            throw new Exception("The specified patient does not exist.");
        }

        var sql = $"""
                   
                           INSERT INTO Patient_Doctor_Appointment (patientId, doctorId, reason, date, time) 
                           VALUES (@p0, @p1, @p2, @p3, @p4)
                   """;

        var res = _context.Database.ExecuteSqlRaw(sql, model.PatientId, model.DoctorId, model.Reason,
            model.AppointmentDate, model.AppointmentTime);
        return res;
    }

    public IEnumerable<DoctorAppoinment> GetNextAppointmentInfo(Guid loggedDoctorId)
    {
        IEnumerable<DoctorAppoinment> res = _context.Database.SqlQuery<DoctorAppoinment>($@"            
	        SELECT TOP(1) A.Id as AppId,p.Id as PatientId ,concat(P.firstName , ' ' , P.lastName) as FullName,  DATEDIFF(YEAR, p.dateOfBirth, GETDATE()) as dateOfBirth, A.reason, A.date , A.status
	        FROM Patient P, Patient_Doctor_Appointment A
	        WHERE A.patientId = P.Id AND A.doctorId = {loggedDoctorId} AND LOWER(A.status) = 'pending' AND A.date = CONVERT (date, GETDATE())
	        ORDER BY A.date").ToList();
        return res;
    }

    public int CreateMedicalRecord(MedicalRecordModel model)
    {
        var sql = $"""
                       INSERT INTO medical_record(dateOfRecording, diagnostic, prescription, doctorId, patientId)
                       VALUES (CONVERT (date, GETDATE()),@p0, @p1, @p2, @p3)
                   """;
        var res = _context.Database.ExecuteSqlRaw(sql, model.Diagnostic, model.Prescription, model.LoggedDoctorId,
        model.SelectedPatientId);
        return res;
    }

    public int DeleteDoctor(Guid doctorId)
    {
        var sql = $"""DELETE FROM Doctor WHERE Id = @p0""";
        var res = _context.Database.ExecuteSqlRaw(sql, doctorId);
        return res;
    }

    public async Task<int> GetDoctorsCountAsync()
    {

        var res = _context.Database.SqlQuery<int>($@"select count(*) from Doctor");
        var count = await res.ToListAsync();
        return count.FirstOrDefault();
    }

    public async Task<GetDoctorProfileModel> DocotorProfileDataByIdAsync(Guid doctorId)
    {
        var res = await _context.Database.SqlQuery<GetDoctorProfileModel>($@"
            SELECT TOP(1) firstName AS FirstName, lastName AS LastName ,
            salary ,workingHours, startSchedule, endSchedule,specialization,dept.name as [DepartmentName]
            FROM Doctor do ,Department dept
            WHERE do.Id = {doctorId} AND departmentId=dept.Id
            ").ToListAsync();
        return res.FirstOrDefault() ?? null;
    }

    public async Task<ApplicationUser> GetUserByIdAsync(Guid docotorId)
    {
        var userId = await _context.Database.SqlQuery<Guid>($"""
                                                             select UserId from dbo.Doctor
                                                             where Id = {docotorId} 
                                                             """).ToListAsync();
        if (userId == null) return null;
        var user = await _userManager.FindByIdAsync(userId.FirstOrDefault().ToString());
        return user;
    }

    public List<Doctor> GetAllDoctors()
    {
        List<Doctor> doctors = _context.Doctors.FromSql($@"select * from dbo.doctor").ToList();
        return doctors;
    }

    public List<DoctorAppoinment> GetDoctorAppointments(Guid? doctorId)
    {
        if (doctorId == null || doctorId == Guid.Empty) return null;

        List<DoctorAppoinment> result = _context.Database.SqlQuery<DoctorAppoinment>(
            $@"select A.Id as AppId,p.Id as PatientId ,concat(P.firstName , ' ' , P.lastName) as FullName,  DATEDIFF(YEAR, p.dateOfBirth, GETDATE()) as dateOfBirth, A.reason, A.date, A.status
                  from Patient_Doctor_Appointment a ,  Patient p , dbo.Doctor d
                  where a.patientId = p.Id and d.Id = a.doctorId
                  and  a.doctorId = {doctorId}").ToList();

        if (result.Count() == 0) return null;
        return result;
    }

    public async Task<int> EditDoctorAsync(DoctorEditModel? model)
    {
        if (model == null) return 0;

        if (model.UserId == null) return 0;
        var user = await _userManager.FindByIdAsync(model.UserId.ToString());
        //update 
        user.UserName = model.UserName;
        user.PhoneNumber = model.PhoneNumber;
       await _context.SaveChangesAsync();
        string sqlcommand1 =
            $"""
             Update Doctor 
             SET firstName =@p0,lastName=@p1
             where UserId=@p2
             """;
        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, model.FirstName, model.LastName, model.UserId);
        return res;
    }

    public async Task<IEnumerable<UsernameIdModel>> GetApprovedPatientsAsync(Guid? loggedDoctorId)
    {
        var res = _context.Database.SqlQuery<UsernameIdModel>(
        $@"                                                                
            select T.pId as Id, A.UserName as [Username]
            FROM (
                 select a.Id as Id, a.date as [date] , a.patientId as pId, a.reason as reason,a.status as [status],a.time as [time],U.UserName as [username]
                 from Patient_Doctor_Appointment as A, AspNetUsers as U, Doctor as D
                 where D.UserId = U.Id AND A.doctorId = D.Id AND D.Id = {loggedDoctorId} AND LOWER(A.Status) = 'approved'
            ) as T
            inner join Patient as p
            on p.Id = T.pId
            inner join AspNetUsers A
            ON p.UserId = A.Id
           ");
        return await res.ToListAsync();
    }
}

