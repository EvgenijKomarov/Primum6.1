using CoreConnection.Enums;
using PrimumCore.Models.Attributes;

namespace PrimumCore.Models.Enums
{
    public enum Permission
    {
        GivePermissions = 0,
        AddCash = 1,
        DeleteLessons = 2,
        [AvailableIncendent(IncendentMeaningDto.Student, IncendentDecisionDto.SendToManager)]
        [AvailableIncendent(IncendentMeaningDto.Student, IncendentDecisionDto.Delete)]
        [AvailableIncendent(IncendentMeaningDto.Student, IncendentDecisionDto.SendToAdministrator)]
        ModerateStudents = 3,
        [AvailableIncendent(IncendentMeaningDto.Teacher, IncendentDecisionDto.SendToManager)]
        [AvailableIncendent(IncendentMeaningDto.Teacher, IncendentDecisionDto.Delete)]
        [AvailableIncendent(IncendentMeaningDto.Teacher, IncendentDecisionDto.SendToAdministrator)]
        ModerateTeachers = 4,
        [AvailableIncendent(IncendentMeaningDto.Course, IncendentDecisionDto.SendToManager)]
        [AvailableIncendent(IncendentMeaningDto.Course, IncendentDecisionDto.Delete)]
        [AvailableIncendent(IncendentMeaningDto.Course, IncendentDecisionDto.SendToAdministrator)]
        ModerateCourses = 5,
        [AvailableIncendent(IncendentMeaningDto.Student, IncendentDecisionDto.SendToManager)]
        AdministrateStudents = 6,
        [AvailableIncendent(IncendentMeaningDto.Course, IncendentDecisionDto.SendToManager)] 
        AdministrateCourses = 7,
        [AvailableIncendent(IncendentMeaningDto.Teacher, IncendentDecisionDto.SendToManager)]
        AdministrateTeachers = 8,
        [AvailableIncendent(IncendentMeaningDto.Course, IncendentDecisionDto.Approve)]
        [AvailableIncendent(IncendentMeaningDto.Course, IncendentDecisionDto.Delete)]
        ApproveCourses = 10,
        [AvailableIncendent(IncendentMeaningDto.Teacher, IncendentDecisionDto.Approve)]
        [AvailableIncendent(IncendentMeaningDto.Teacher, IncendentDecisionDto.Delete)]
        ApproveTeachers = 11,
        [AvailableIncendent(IncendentMeaningDto.Lesson, IncendentDecisionDto.Delete)]
        InspectMissedLessons = 12,
        CreateAdminProfiles = 13,
    }
}
