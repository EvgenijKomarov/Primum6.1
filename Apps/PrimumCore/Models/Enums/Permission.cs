using CoreConnection.Enums;
using PrimumCore.Models.Attributes;

namespace PrimumCore.Models.Enums
{
    public enum Permission
    {
        EditPermissions = 0,
        AddCash = 1,
        DeleteLessons = 2,
        [AvailableIncident(IncidentMeaningDto.Student, IncidentDecisionDto.Approve)]
        [AvailableIncident(IncidentMeaningDto.Student, IncidentDecisionDto.Delete)]
        [AvailableIncident(IncidentMeaningDto.Student, IncidentDecisionDto.SendToAdministrator)]
        ModerateStudents = 3,
        [AvailableIncident(IncidentMeaningDto.Teacher, IncidentDecisionDto.SendToManager)]
        [AvailableIncident(IncidentMeaningDto.Teacher, IncidentDecisionDto.Delete)]
        [AvailableIncident(IncidentMeaningDto.Teacher, IncidentDecisionDto.SendToAdministrator)]
        ModerateTeachers = 4,
        [AvailableIncident(IncidentMeaningDto.Course, IncidentDecisionDto.SendToManager)]
        [AvailableIncident(IncidentMeaningDto.Course, IncidentDecisionDto.Delete)]
        [AvailableIncident(IncidentMeaningDto.Course, IncidentDecisionDto.SendToAdministrator)]
        ModerateCourses = 5,
        [AvailableIncident(IncidentMeaningDto.Student, IncidentDecisionDto.Approve)]
        AdministrateStudents = 6,
        [AvailableIncident(IncidentMeaningDto.Course, IncidentDecisionDto.SendToManager)] 
        AdministrateCourses = 7,
        [AvailableIncident(IncidentMeaningDto.Teacher, IncidentDecisionDto.SendToManager)]
        AdministrateTeachers = 8,
        [AvailableIncident(IncidentMeaningDto.Course, IncidentDecisionDto.Approve)]
        [AvailableIncident(IncidentMeaningDto.Course, IncidentDecisionDto.Delete)]
        ApproveCourses = 10,
        [AvailableIncident(IncidentMeaningDto.Teacher, IncidentDecisionDto.Approve)]
        [AvailableIncident(IncidentMeaningDto.Teacher, IncidentDecisionDto.Delete)]
        ApproveTeachers = 11,
        [AvailableIncident(IncidentMeaningDto.Lesson, IncidentDecisionDto.Delete)]
        [AvailableIncident(IncidentMeaningDto.Lesson, IncidentDecisionDto.Revisioned)]
        InspectMissedLessons = 12,
        CreateAdminProfiles = 13,
        InspectIncidentLogs = 14,
        AddPromocodes = 15,
        DeletePromocodes = 16,
        ChangeBanStatus = 17,
        EditCourseThemes = 18
    }
}
