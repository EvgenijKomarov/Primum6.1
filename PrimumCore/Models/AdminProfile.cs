using PrimumCore.Models.Enums;
using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Security;

namespace PrimumCore.Models;

public partial class AdminProfile
{
    public int AdminId { get; set; }

    public int UserId { get; set; }

    public string? Status { get; set; }

    public User User { get; set; } = null!;

    public virtual ICollection<AdminPermission> Permissions {  get; set; } = new List<AdminPermission>();

    public virtual ICollection<AdminPermission> GivenPermissions { get; set; } = new List<AdminPermission>();

    public virtual ICollection<IncidentLog> IncidentLogs { get; set; } = new List<IncidentLog>();

    public bool CheckPermissions(Permission permission) => Permissions.Any(x => x.Permission == permission);
}
