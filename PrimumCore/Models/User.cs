using System;
using System.Collections.Generic;

namespace PrimumCore.Models;

public partial class User
{
    public int Id { get; set; }

    public string Password { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public StudentProfile? StudentProfile { get; set; }

    public TeacherProfile? TeacherProfile { get; set; }

    public AdminProfile? AdminProfile { get; set; }

    public string DisplayName => $"{Surname} {Name}";
}
