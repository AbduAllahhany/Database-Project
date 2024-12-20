﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hospital.management.system.BLL.Models.Patient;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.BLL.Services.IServices;

public interface IPatientService
{
    public List<PatientAppointment> GetPatientAppointments(Guid patientId);
    public List<PatientBill> GetPatientBills(Guid patientId);
    public List<PatientMedicalRecord> GetPatientMedicalRecord(Guid patientId);
    public List<PatientVisit> GetPatientVisits(Guid patientId);
    public PatientRoom GetRoomStatus(Guid patientId);
    public ViewInsurance GetViewInsurance(Guid patientId);
    public List<PatientEmergancyContact> GetPatientEmergancyContacts(Guid patientId);
    public int DeletePatient(Guid patientId);
    public List<Patient> GetAllPetient();
    public Task<GetPatientProfileModel> PatientProfileDataByIdAsync(Guid? Id);
    public Task<int> GetPatientsCountAsync();
    public Task<int> AddPatientPhoneNumberAsync(Guid? patientId, string phoneNumber);
    public Task<int> EditPatientAsync(PatientEditModel? model);
    public Task<ApplicationUser> GetUserByIdAsync(Guid userId);
    //public void MarkPatientAppoinment(Appointment appoinment);
}