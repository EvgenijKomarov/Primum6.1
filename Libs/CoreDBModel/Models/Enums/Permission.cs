using CoreDBModel.Models.Attributes;

namespace CoreDBModel.Models.Enums
{
    public enum Permission
    {
        EditPermissions = 0,
        AddCash = 1,
        DeleteLessons = 2,
        [AvailableIncident(IncidentMeaning.Student, IncidentDecision.Approve)]
        [AvailableIncident(IncidentMeaning.Student, IncidentDecision.Delete)]
        [AvailableIncident(IncidentMeaning.Student, IncidentDecision.SendToAdministrator)]
        ModerateStudents = 3,
        [AvailableIncident(IncidentMeaning.Teacher, IncidentDecision.SendToManager)]
        [AvailableIncident(IncidentMeaning.Teacher, IncidentDecision.Delete)]
        [AvailableIncident(IncidentMeaning.Teacher, IncidentDecision.SendToAdministrator)]
        ModerateTeachers = 4,
        [AvailableIncident(IncidentMeaning.Course, IncidentDecision.SendToManager)]
        [AvailableIncident(IncidentMeaning.Course, IncidentDecision.Delete)]
        [AvailableIncident(IncidentMeaning.Course, IncidentDecision.SendToAdministrator)]
        ModerateCourses = 5,
        [AvailableIncident(IncidentMeaning.Student, IncidentDecision.Approve)]
        AdministrateStudents = 6,
        [AvailableIncident(IncidentMeaning.Course, IncidentDecision.SendToManager)] 
        AdministrateCourses = 7,
        [AvailableIncident(IncidentMeaning.Teacher, IncidentDecision.SendToManager)]
        AdministrateTeachers = 8,
        [AvailableIncident(IncidentMeaning.Course, IncidentDecision.Approve)]
        [AvailableIncident(IncidentMeaning.Course, IncidentDecision.Delete)]
        ApproveCourses = 10,
        [AvailableIncident(IncidentMeaning.Teacher, IncidentDecision.Approve)]
        [AvailableIncident(IncidentMeaning.Teacher, IncidentDecision.Delete)]
        ApproveTeachers = 11,
        [AvailableIncident(IncidentMeaning.Lesson, IncidentDecision.Delete)]
        [AvailableIncident(IncidentMeaning.Lesson, IncidentDecision.Revisioned)]
        InspectMissedLessons = 12,
        CreateAdminProfiles = 13,
        InspectIncidentLogs = 14,
        AddPromocodes = 15,
        DeletePromocodes = 16,
        ChangeBanStatus = 17,
        EditCourseThemes = 18
    }
}
