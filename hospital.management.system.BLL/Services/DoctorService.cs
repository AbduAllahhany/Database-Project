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

    public IEnumerable<Doctor> getPendingAppointments(Guid loggedDoctorId)
    {
        //var sql = $@"";
        IQueryable<Doctor> query = _context.Doctors.FromSqlInterpolated($@"
            SELECT P.firstName + ' ' + P.lastName as 'Full Name', P.dateOfBirth, A.reason, A.date
            FROM Patient P 
            INNER JOIN Appointment A
            ON A.patientId = P.Id AND A.doctorId = {loggedDoctorId}
            WHERE A.status = 'Pending'
            ORDER BY A.date");
        return query.ToList();
    }

    public IEnumerable<Doctor> getDailyAppointments(Guid loggedDoctorId)
    {
        //var sql = ;
        IQueryable<Doctor> query = _context.Doctors.FromSqlInterpolated($@"
            SELECT P.firstName + ' ' + P.lastName as 'Full Name', P.dateOfBirth, A.reason, A.date
            FROM Patient P 
            INNER JOIN Appointment A
            ON A.patientId = P.Id AND A.doctorId = {0}
            WHERE A.status = 'Pending' AND A.date = CONVERT (date, GETDATE())
            ORDER BY A.date");
        return query.ToList();
    }
    
    public IEnumerable<Doctor> getUpcomingAppointments(Guid loggedDoctorId)
    {
        //var sql = ;
        IQueryable<Doctor> query = _context.Doctors.FromSqlInterpolated($@"
            SELECT P.firstName + ' ' + P.lastName as 'Full Name', P.dateOfBirth, A.reason, A.date
            FROM Patient P 
            INNER JOIN Appointment A
            ON A.patientId = P.Id AND A.doctorId = {0}
            WHERE A.status = 'Pending' AND A.date > CONVERT (date, GETDATE())
            ORDER BY A.date");
        return query.ToList();
    }
    
    public IEnumerable<Doctor> getMonthlyAppointmentSummary(Guid loggedDoctorId)
    {
        //var sql = ;
        IQueryable<Doctor> query = _context.Doctors.FromSqlInterpolated($@"
            SELECT COUNT(*) AS TotalAppointments,
                SUM(CASE WHEN status = 'Approved' THEN 1 ELSE 0 END) AS ApprovedAppointments,
                SUM(CASE WHEN status = 'Pending' THEN 1 ELSE 0 END) AS PendingAppointments,
                SUM(CASE WHEN status = 'Reject' THEN 1 ELSE 0 END) AS RejectedAppointments
            FROM Appointment A
            WHERE A.doctorId = {0} AND MONTH(A.date) = MONTH(getDate()) AND YEAR(date) = YEAR(getDate())");
        return query.ToList();
    }
    
    public int approveNextAppointment(Guid loggedDoctorId) 
    {
        //var sql = ;
        var res = _context.Database.ExecuteSqlRaw($@"
            UPDATE Appointment
            SET status = 'Approved'
            WHERE Id IN (
	            SELECT TOP(1) A.Id
	            FROM Patient P, Appointment A
	            WHERE A.patientId = P.Id AND A.doctorId = {0} AND A.status = 'Pending' AND A.date = CONVERT (date, GETDATE())
	            ORDER BY A.date
            )");
        return res;
    }

    public int postponingAppointment(Guid loggedDoctorId)
    {
        //var sql = ;
        var res = _context.Database.ExecuteSqlRaw($@"
            UPDATE Appointment
            SET date = DATEADD(DAY, 1,date)
            WHERE Id IN (
	            SELECT A.Id
	            FROM Patient P, Appointment A
	            WHERE A.patientId = P.Id AND A.doctorId = {0} AND A.status = 'Pending' AND A.date = CONVERT (date, GETDATE())
            )");
        return res;
    }

    public int cancelingAppointment(DoctorCancelingAppointmentModel model)
    {
        var sql = $@"
            UPDATE Appointment
            SET status = 'Rejected'
            WHERE Id IN (
	            SELECT A.Id
	            FROM Patient P, Appointment A
	            WHERE A.patientId = {0} AND A.doctorId = {1} AND A.status = 'Pending'
            )";
        var res = _context.Database.ExecuteSqlRaw(sql, model.SelectedPatientId, model.LoggedDoctorId);
        return res;
    }

    public int followUpAppointment(FollowUpAppointmentModel model)
    {
        var sql = $@"
            INSERT INTO Appointment(patientId, doctorId, reason, date) 
            VALUES (@p1, @p2, @p3, @p4))";
        var res = _context.Database.ExecuteSqlRaw(sql, model.PatientId, model.DoctorId, model.Reason, model.AppointmentDate);
        return res;
    }
    
    // int !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public int getNextAppointmentInfo(Guid loggedDoctorId)
    {
        var sql = $@"            
	        SELECT TOP(1) P.firstName + ' ' + P.lastName as 'Full Name', P.dateOfBirth, A.reason
	        FROM Patient P, Appointment A
	        WHERE A.patientId = P.Id AND A.doctorId = {0} AND A.status = 'Pending' AND A.date = CONVERT (date, GETDATE())
	        ORDER BY A.date";
        var res = _context.Database.ExecuteSqlRaw(sql, loggedDoctorId);
        return res;
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
