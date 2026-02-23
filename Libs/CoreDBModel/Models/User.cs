using System;
using System.Collections.Generic;

namespace CoreDBModel.Models;

public partial class User
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public string MailAdress { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public long Cash { get; set; }

    public bool IsBanned { get; set; } = false;

    public bool IsMailChecked { get; set; } = false;

    public StudentProfile? StudentProfile { get; set; }

    public TeacherProfile? TeacherProfile { get; set; }

    public AdminProfile? AdminProfile { get; set; }

    public virtual ICollection<VerificationToken> VerificationTokens { get; set; } = new List<VerificationToken>();

    public string DisplayName => $"{Surname} {Name} {Patronymic}";
}
