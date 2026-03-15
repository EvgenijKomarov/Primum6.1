using CoreConnection.DTOs;
using CoreConnection.Entities;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PublishServiceConnection;
using PublishServiceConnection.Events;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class StudentSheduleIterator(DatabaseIterator dbIterator, PublisherService publisher)
    {
        public async Task<PageResult<AbonementSheduleDto>> GetAbonementShedules(int abonementId, int _page, int _pageSize)
        {
            return await dbIterator.AbonementShedules()
                .Where(x => x.Abonement.Id == abonementId)
                .ToDto()
                .ToPageResult(_page, _pageSize);
        }

        public async Task<PageResult<AbonementSheduleDto>> GetStudentShedules(int studentId, int _page, int _pageSize)
        {
            return await dbIterator.AbonementShedules()
                .Where(x => x.Abonement.Student.User.Id == studentId)
                .ToDto()
                .ToPageResult(_page, _pageSize);
        }

        public async Task<AbonementSheduleDto> GetStudentShedule(int studentId, int sheduleId)
        {
            return await dbIterator.AbonementShedules()
                .Where(x => x.Abonement.Student.User.Id == studentId)
                .ToDto()
                .One(x => x.Id == sheduleId);
        }

        public async Task<int> DeleteStudentShedule(int studentId, int abonementSheduleId)
        {
            var abonementShedule = await dbIterator.AbonementShedules()
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .One(x => x.Id == abonementSheduleId);
            if (abonementShedule.Abonement.Student.User.Id != studentId) { throw new BusinessLogicException("Only owner can delete shedule"); }

            var notification = new DeleteAbonementSheduleEvent
            {
                StudentName = abonementShedule.Abonement.Student.User.DisplayName,
                StudentUserId = abonementShedule.Abonement.Student.User.Id,
                TeacherName = abonementShedule.TeacherShedule.Teacher.User.DisplayName,
                TeacherUserId = abonementShedule.TeacherShedule.Teacher.User.Id,
                CourseName = abonementShedule.Abonement.Course.Name,
                AbonementId = abonementShedule.Abonement.Id,
                AbonementSheduleId = abonementShedule.Id,
                DayOfWeek = abonementShedule.TeacherShedule.DayOfWeek.ToString(),
                Time = abonementShedule.TeacherShedule.Time
            };
            abonementShedule.Abonement.AbonementShedules.Remove(abonementShedule);
            await dbIterator.SaveChangesAsync();

            await publisher.Push(notification);
            return abonementShedule.Id;
        }
    }
}
