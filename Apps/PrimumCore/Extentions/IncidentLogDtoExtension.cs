using CoreConnection.DTOs;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace PrimumCore.Extentions
{
    public static class IncidentLogDtoExtension
    {
        public static IQueryable<IncidentLogDto> LoadIncidentLogs(
        this IQueryable<IncidentLog> logs,
        int objectId,
        IncidentMeaning meaning)
        {
            return logs
                .Include(log => log.AdminProfile)
                .ThenInclude(profile => profile.User)
                .Where(log => log.ObjectId == objectId && log.Meaning == meaning)
                .Select(log => new IncidentLogDto
                {
                    AdminUserId = log.AdminProfile.User.Id,
                    AdminDisplayName = log.AdminProfile.User.DisplayName,
                    LogId = log.Id,
                    Description = log.Description,
                    DateTime = log.DecisionDate
                });
        }
    }
}
