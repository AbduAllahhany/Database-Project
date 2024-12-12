using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hospital.management.system.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SSN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    description = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__3214EC078DB5010E", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    costPerDay = table.Column<decimal>(type: "money", nullable: false),
                    roomNumber = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "general"),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Room__3214EC07A1950031", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    firstName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    lastName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    bloodGroup = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    allergies = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    chronicDiseases = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patient__3214EC073B6DCB54", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    firstName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    lastName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    specialization = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    departmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    salary = table.Column<decimal>(type: "money", nullable: true, defaultValue: 0m),
                    workingHours = table.Column<int>(type: "int", nullable: false),
                    startSchedule = table.Column<TimeOnly>(type: "time", nullable: false),
                    endSchedule = table.Column<TimeOnly>(type: "time", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctor__3214EC0708B2C4AD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Doctor__departme__534D60F1",
                        column: x => x.departmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    firstName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    lastName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    role = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "intern"),
                    deptId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    startSchedule = table.Column<TimeOnly>(type: "time", nullable: false),
                    endSchedule = table.Column<TimeOnly>(type: "time", nullable: false),
                    dayOfWork = table.Column<byte>(type: "tinyint", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Staff__3214EC0799EFF6CD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Staff__dayOfWork__6C190EBB",
                        column: x => x.deptId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Admission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    roomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Admissio__3214EC077F42FC34", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Admission__patie__70DDC3D8",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Admission__roomI__6FE99F9F",
                        column: x => x.roomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    totalAmount = table.Column<decimal>(type: "money", nullable: false),
                    paidAmount = table.Column<decimal>(type: "money", nullable: false),
                    remaining = table.Column<decimal>(type: "money", nullable: true, computedColumnSql: "([TotalAmount]-[PaidAmount])", stored: false),
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bill__3214EC07FB416F69", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Bill__patientId__74AE54BC",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Emergency_Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    phone = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    relationship = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Emergenc__3214EC073ADB0451", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Emergency__patie__7E37BEF6",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    providerName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    policyNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    coverageMoney = table.Column<decimal>(type: "money", nullable: true, defaultValue: 0m),
                    expiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__3214EC07BC6E5361", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Insurance__patie__797309D9",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient_Phone",
                columns: table => new
                {
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    number = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patient___BEA29408F7AA0AAA", x => new { x.patientId, x.number });
                    table.ForeignKey(
                        name: "FK__Patient_P__patie__571DF1D5",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    notes = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reason = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Visit__3214EC07169C3B2E", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Visit__patientId__619B8048",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medical_Record",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    dateOfRecording = table.Column<DateOnly>(type: "date", nullable: false),
                    diagnostic = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    prescription = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    doctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medical___3214EC07A123C966", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Medical_R__docto__02084FDA",
                        column: x => x.doctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Medical_R__patie__02FC7413",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Patient_Doctor_Appointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    doctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Pending"),
                    reason = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patient___3214EC0784C5A62C", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Patient_D__docto__5BE2A6F2",
                        column: x => x.doctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Patient_D__patie__5AEE82B9",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admission_patientId",
                table: "Admission",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_Admission_roomId",
                table: "Admission",
                column: "roomId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_patientId",
                table: "Bill",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_departmentId",
                table: "Doctor",
                column: "departmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_UserId",
                table: "Doctor",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emergency_Contact_patientId",
                table: "Emergency_Contact",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "UQ__Emergenc__B43B145F0626F054",
                table: "Emergency_Contact",
                column: "phone",
                unique: true,
                filter: "[phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_patientId",
                table: "Insurance",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Record_doctorId",
                table: "Medical_Record",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Record_patientId",
                table: "Medical_Record",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UserId",
                table: "Patient",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Doctor_Appointment_doctorId",
                table: "Patient_Doctor_Appointment",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Doctor_Appointment_patientId",
                table: "Patient_Doctor_Appointment",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "UQ__Patient___FD291E414AD144B4",
                table: "Patient_Phone",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_deptId",
                table: "Staff",
                column: "deptId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UserId",
                table: "Staff",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visit_patientId",
                table: "Visit",
                column: "patientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admission");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Emergency_Contact");

            migrationBuilder.DropTable(
                name: "Insurance");

            migrationBuilder.DropTable(
                name: "Medical_Record");

            migrationBuilder.DropTable(
                name: "Patient_Doctor_Appointment");

            migrationBuilder.DropTable(
                name: "Patient_Phone");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
