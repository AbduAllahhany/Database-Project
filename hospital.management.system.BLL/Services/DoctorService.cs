﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public DoctorService(IUnitOfWork _unitOfWork, ApplicationDbContext _context,UserManager<ApplicationUser> userManager)
    {
        unitOfWork = _unitOfWork;
        this._context = _context;
        _userManager = userManager;
    }

    public IEnumerable<DoctorAppoinment> getPendingAppointments(Guid loggedDoctorId)
    {
        IEnumerable<DoctorAppoinment> query = _context.Database.SqlQuery<DoctorAppoinment>($@"
            SELECT p.Id as PatientId ,concat(P.firstName , ' ' , P.lastName) as FullName, DATEDIFF(YEAR, p.dateOfBirth, GETDATE()) as dateOfBirth, A.reason, A.date
            FROM Patient P 
            INNER JOIN Patient_Doctor_Appointment A
            ON A.patientId = P.Id AND A.doctorId = {loggedDoctorId}
            WHERE A.status = 'Pending'
            ORDER BY A.date").ToList();

        return query;
    }
    public async Task<int> GetDoctorsCountAsync()
    {
        var res = _context.Database.SqlQuery<int>($@"select count(*) from Doctor");
        var count = await res.ToListAsync();
        return count.FirstOrDefault();
    }

    // ==> modidfied 
    public IEnumerable<DoctorAppoinment> getDailyAppointments(Guid loggedDoctorId)
    {
        //var sql = ;
        IEnumerable<DoctorAppoinment> query = _context.Database.SqlQuery<DoctorAppoinment>($@"
            SELECT p.Id as PatientId ,concat(P.firstName , ' ' , P.lastName) as FullName,  DATEDIFF(YEAR, p.dateOfBirth, GETDATE()) as dateOfBirth, A.reason, A.date
            FROM Patient P 
            INNER JOIN Patient_Doctor_Appointment A
            ON A.patientId = P.Id AND A.doctorId = {loggedDoctorId}
            WHERE A.status = 'Pending' AND A.date = CONVERT (date, GETDATE())
            ORDER BY A.date").ToList();
        return query;
    }

    // ==> modidfied    
    public IEnumerable<DoctorAppoinment> getUpcomingAppointments(Guid loggedDoctorId)
    {
        //var sql = ;
        IEnumerable<DoctorAppoinment> query = _context.Database.SqlQuery<DoctorAppoinment>($@"
            SELECT p.Id as PatientId ,concat(P.firstName , ' ' , P.lastName) as FullName,  DATEDIFF(YEAR, p.dateOfBirth, GETDATE()) as dateOfBirth, A.reason, A.date
            FROM Patient P 
            INNER JOIN Patient_Doctor_Appointment A
            ON A.patientId = P.Id AND A.doctorId = {loggedDoctorId}
            WHERE A.status = 'Pending' AND A.date > CONVERT (date, GETDATE())
            ORDER BY A.date").ToList();
        return query;
    }

    // ==>  modidfied 
    public IEnumerable<DoctorMonthlyAppointmentSummary> getMonthlyAppointmentSummary(Guid loggedDoctorId)
    {
        //var sql = ;
        IEnumerable<DoctorMonthlyAppointmentSummary> query = _context.Database
            .SqlQuery<DoctorMonthlyAppointmentSummary>($@"
            SELECT COUNT(*) AS TotalAppointments,
                SUM(CASE WHEN status = 'Approved' THEN 1 ELSE 0 END) AS ApprovedAppointments,
                SUM(CASE WHEN status = 'Pending' THEN 1 ELSE 0 END) AS PendingAppointments,
                SUM(CASE WHEN status = 'Reject' THEN 1 ELSE 0 END) AS RejectedAppointments
            FROM Patient_Doctor_Appointment A
            WHERE A.doctorId = {loggedDoctorId} AND MONTH(A.date) = MONTH(getDate()) AND YEAR(date) = YEAR(getDate())")
            .ToList();
        return query;
    }

    public int approveNextAppointment(Guid loggedDoctorId)
    {
        //var sql = ;
        var sql = @"
        UPDATE Patient_Doctor_Appointment
        SET status = 'Approved'
        WHERE Id IN (
            SELECT TOP(1) A.Id
            FROM Patient P, Patient_Doctor_Appointment A
            WHERE A.patientId = P.Id 
                  AND A.doctorId = (@p0) 
                  AND A.status = 'Pending' 
                  AND A.date = CONVERT(date, GETDATE())
        )";

        var res = _context.Database.ExecuteSqlRaw(sql, loggedDoctorId);
        return res;
    }

    public int postponingAppointment(Guid loggedDoctorId)
    {
        //var sql = ;
        var res = _context.Database.ExecuteSqlRaw($@"
            UPDATE Patient_Doctor_Appointment
            SET date = DATEADD(DAY, 1,date)
            WHERE Id IN (
	            SELECT A.Id
	            FROM Patient P, Patient_Doctor_Appointment A
	            WHERE A.patientId = P.Id AND A.doctorId = {loggedDoctorId} AND A.status = 'Pending' AND A.date = CONVERT (date, GETDATE())
            )");
        return res;
    }

    public int cancelingAppointment(DoctorCancelingAppointmentModel model)
    {
        var sql = $@"
            UPDATE Patient_Doctor_Appointment
            SET status = 'Rejected'
            WHERE Id IN (
	            SELECT A.Id
	            FROM Patient P, Patient_Doctor_Appointment A
	            WHERE A.patientId = (@p0) AND A.doctorId = (@p1) AND A.status = 'Pending'
            )";
        var res = _context.Database.ExecuteSqlRaw(sql, model.SelectedPatientId, model.LoggedDoctorId);
        return res;
    }

    public int followUpAppointment(FollowUpAppointmentModel model)
    {
        var sql = $@"
            INSERT INTO Patient_Doctor_Appointment(patientId, doctorId, reason, date) 
            VALUES (@p1, @p2, @p3, @p4))";
        var res = _context.Database.ExecuteSqlRaw(sql, model.PatientId, model.DoctorId, model.Reason,
            model.AppointmentDate);
        return res;
    }

    // int !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // ==>  modified 
    public IEnumerable<DoctorAppoinment> getNextAppointmentInfo(Guid loggedDoctorId)
    {

        IEnumerable<DoctorAppoinment> res = _context.Database.SqlQuery<DoctorAppoinment>($@"            
	        SELECT TOP(1) p.Id as PatientId ,concat(P.firstName , ' ' , P.lastName) as FullName,  DATEDIFF(YEAR, p.dateOfBirth, GETDATE()) as dateOfBirth, A.reason, A.date
	        FROM Patient P, Patient_Doctor_Appointment A
	        WHERE A.patientId = P.Id AND A.doctorId = {loggedDoctorId} AND A.status = 'Pending' AND A.date = CONVERT (date, GETDATE())
	        ORDER BY A.date").ToList();
        return res;
    }

    public int CreateMedicalRecord(MedicalRecordModel model)
    {
        var sql = @"            
            INSERT INTO medical_record(dateOfRecording, diagnostic, prescription, doctorId, patientId)
            VALUES (CONVERT (date, GETDATE()), @p1, @p2, @p3, @p4)";
        var res = _context.Database.ExecuteSqlRaw(sql, model.Diagnostic, model.Prescription, model.LoggedDoctorId,
            model.SelectedPatientId);
        return res;
    }
    public async Task<GetDoctorProfileModel> GetDoctorByIdAsync(Guid doctorId)
    {
        var res = await _context.Database.SqlQuery<GetDoctorProfileModel>($@"
            SELECT TOP(1) firstName AS FirstName, lastName AS LastName 
            FROM Doctor 
            WHERE Id = {doctorId}
            ").ToListAsync();
        return res.FirstOrDefault() ?? null;

    }
    public async Task<int> EditDoctorAsync(DoctorEditModel? model)
    {
        if (model == null) return 0;
        
        if (model.Id == null) return 0;
        var user = await _userManager.FindByIdAsync(model.Id.ToString());
        //update 
        user.UserName = model.UserName;
        string sqlcommand1 =
            $"""
             Update Doctor 
             SET firstName =@p0,lastName=@p1
             where UserId=@p2
             """;
        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1, model.FirstName, model.LastName, model.Id);
        return res;
    }
    
}

