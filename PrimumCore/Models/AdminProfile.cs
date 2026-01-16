using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace PrimumCore.Models;

public partial class AdminProfile
{
    public int AdminId { get; set; }

    public int UserId { get; set; }

    public string? Status { get; set; }

    public int Permissions { get; set; }

    public User User { get; set; } = null!;
}
