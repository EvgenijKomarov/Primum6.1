using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace PrimumCore.Models;

public partial class AdminProfile
{
    public int AdminId { get; set; }

    public string? Status { get; set; }

    public byte[] Permissions { get; set; } = null!;

    public User User { get; set; } = null!;
}
