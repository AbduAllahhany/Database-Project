﻿

using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models;

public class ProfileModel
{
    public string UserName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string? NationalIdOrPassport { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public bool IsEmailConfirmed { get; set; } = false;
    public bool IsTwoFactorEnabled { get; set; } = false;
    //public string? Address { get; set; } = string.Empty;
    //public DateTime? DateOfBirth { get; set; }


}

