CREATE DATABASE DatabaseProjectTest
GO
USE DatabaseProjectTest
go

DROP TABLE IF EXISTS Visits;
DROP TABLE IF EXISTS EmergencyContacts;
DROP TABLE IF EXISTS Insurance;
DROP TABLE IF EXISTS Admissions;
DROP TABLE IF EXISTS Bills;
DROP TABLE IF EXISTS Rooms;
DROP TABLE IF EXISTS MedicalRecords;
DROP TABLE IF EXISTS Appointments;
DROP TABLE IF EXISTS Doctors;
DROP TABLE IF EXISTS Staff;
DROP TABLE IF EXISTS Patients;
DROP TABLE IF EXISTS Suppliers;
DROP TABLE IF EXISTS Departments;


-- Department Table
CREATE TABLE Department (
                            Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                            description VARCHAR(200) NOT NULL,
                            name VARCHAR(20) NOT NULL
);

-- Patient Table
CREATE TABLE Patient (
                         Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,        
                         firstName VARCHAR(20) NOT NULL,
                         lastName VARCHAR(20) NOT NULL,
                         dateOfBirth DATE NOT NULL,
                         bloodGroup VARCHAR(10) NULL check(bloodGroup in ('A+','A-','B-','B+','AB+','AB-','O+','O-') ),
                         allergies VARCHAR(20) NULL,
                         chronicDiseases VARCHAR(30) NULL,
                         address VARCHAR(50)
);

-- Doctor Table
CREATE TABLE Doctor (
                        Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                        firstName VARCHAR(20) NOT NULL,
                        lastName VARCHAR(20) NOT NULL,                       
                        specialization VARCHAR(50) NOT NULL,
                        departmentId UNIQUEIDENTIFIER NULL,
                        salary money default 0,
                        workingHours int NOT NULL CHECK(workingHours BETWEEN 4 AND 8),
                        startSchedule time NOT NULL,
                        endSchedule time NOT NULL,
                        FOREIGN KEY (DepartmentId) REFERENCES Department(Id)
                        ON DELETE SET NULL ON UPDATE CASCADE
);

-- Patient_Phone Table
CREATE TABLE Patient_Phone (
                               patientId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Patient(Id),
                               number CHAR(11) UNIQUE,
                               PRIMARY KEY(PatientId, Number)
);

-- Patient_Doctor_Appointment Table
CREATE TABLE Patient_Doctor_Appointment (
                                            Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                                            patientId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Patient(Id) ON DELETE CASCADE,
                                            doctorId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Doctor(Id) ON DELETE CASCADE,
                                            status VARCHAR(10) NOT NULL DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Approved', 'Rejected')),
                                            reason VARCHAR(200),
                                            date DATE NOT NULL,
                                            time TIME NOT NULL,
);

-- Visit Table
CREATE TABLE Visit (
                       Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                       notes VARCHAR(200),
                       patientId UNIQUEIDENTIFIER NOT NULL,
                       reason VARCHAR(200) NOT NULL,
                       date DATE NOT NULL,
                       FOREIGN KEY (PatientId) REFERENCES Patient(Id) ON DELETE CASCADE
);

-- Room Table
CREATE TABLE Room (
                      Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                      costPerDay MONEY NOT NULL,
                      roomNumber INT NOT NULL,
                      type VARCHAR(20) DEFAULT 'general',
                      status BIT NOT NULL CHECK (Status IN (0, 1))
);

-- Staff Table
CREATE TABLE Staff (
                       Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                       firstName VARCHAR(20) NOT NULL,
                       lastName VARCHAR(20) NOT NULL,                     
                       role VARCHAR(20) DEFAULT 'intern',
                       deptId UNIQUEIDENTIFIER NULL,
                       startSchedule time NOT NULL,
                       endSchedule time NOT NULL,
                       dayOfWork tinyint NOT NULL CHECK(dayOfWork BETWEEN 1 AND 7)
                       FOREIGN KEY (DeptId) REFERENCES Department(Id) ON DELETE SET NULL
);

-- Admission Table
CREATE TABLE Admission (
                           Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                           startDate DATE NOT NULL,
                           endDate DATE NOT NULL,
                           roomId UNIQUEIDENTIFIER NOT NULL,
                           patientId UNIQUEIDENTIFIER NOT NULL,
                           FOREIGN KEY (RoomId) REFERENCES Room(Id) ON DELETE CASCADE,
                           FOREIGN KEY (PatientId) REFERENCES Patient(Id) ON DELETE CASCADE
);

-- Bill Table
CREATE TABLE Bill (
                      Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                      date DATETIMEOFFSET,
                      totalAmount MONEY NOT NULL,
                      paidAmount MONEY NOT NULL,
                      remaining AS (TotalAmount - PaidAmount),
                      patientId UNIQUEIDENTIFIER NULL,
                      FOREIGN KEY (PatientId) REFERENCES Patient(Id) ON DELETE SET NULL
);

-- Insurance Table
CREATE TABLE Insurance (
                           Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                           providerName VARCHAR(20) NOT NULL,
                           policyNumber VARCHAR(20) NOT NULL,
                           coverageMoney MONEY DEFAULT 0,
                           expiryDate DATE NOT NULL,
                           patientId UNIQUEIDENTIFIER NOT NULL,
                           FOREIGN KEY (patientId) REFERENCES Patient(Id) ON DELETE CASCADE 
);

-- Emergency Contact Table
CREATE TABLE Emergency_Contact (
                                   Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                                   name VARCHAR(20) NOT NULL,
                                   phone CHAR(11) UNIQUE,
                                   relationship VARCHAR(20) NOT NULL,
                                   patientId UNIQUEIDENTIFIER NOT NULL,
                                   FOREIGN KEY (PatientId) REFERENCES Patient(Id) ON DELETE CASCADE
);

-- Medical Record Table
CREATE TABLE Medical_Record (
                                Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
                                dateOfRecording DATE NOT NULL,
                                diagnostic VARCHAR(20),
                                prescription VARCHAR(20),
                                doctorId UNIQUEIDENTIFIER NULL,
                                patientId UNIQUEIDENTIFIER NULL,
                                FOREIGN KEY (DoctorId) REFERENCES Doctor(Id) ON DELETE SET NULL,
                                FOREIGN KEY (PatientId) REFERENCES Patient(Id) ON DELETE SET NULL
);
