using System;
using System.Collections.Generic;

namespace PrimumCore.Models;

public partial class User
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public long Cash { get; set; }

    public bool IsActive { get; set; }

    public StudentProfile? StudentProfile { get; set; }

    public TeacherProfile? TeacherProfile { get; set; }

    public AdminProfile? AdminProfile { get; set; }

    public string DisplayName => $"{Surname} {Name}";
}
