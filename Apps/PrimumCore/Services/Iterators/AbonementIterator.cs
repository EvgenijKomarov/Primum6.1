using CoreConnection.DTOs;
using PrimumCore.Entities;
using CoreDBModel.Models.Enums;
using PrimumCore.Extentions;
using PublishServiceConnection;
using PublishServiceConnection.Events;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using CoreDBModel.Models;

namespace PrimumCore.Services.Iterators
{
    public class AbonementIterator(DatabaseIterator dbIterator, PublisherService publisher)
    {
        public async Task<PageResult<AbonementDto>> GetTeacherAbonements(int teacherId, int _page, int _pageSize)
        {
            return await dbIterator.Abonements(true)
                .Where(x => x.Course.Teacher.User.Id == teacherId)
                .ToDto()
                .ToPageResult(_page, _pageSize);
        }

        public async Task<AbonementDto> GetTeacherAbonement(int teacherId, int abonementId)
        {
            return await dbIterator.Abonements(true)
                .Where(x => x.Course.Teacher.User.Id == teacherId)
                .ToDto()
                .One(x => x.Id == abonementId);
        }

        public async Task<PageResult<AbonementDto>> GetStudentAbonements(int studentId, int _page, int _pageSize)
        {
            return await dbIterator.Abonements(false)
                .Where(x => x.Student.User.Id == studentId)
                .ToDto()
                .ToPageResult(_page, _pageSize);
        }

        public async Task<AbonementDto> GetStudentAbonement(int studentId, int abonementId)
        {
            return await dbIterator.Abonements(false)
                .Where(x => x.Student.User.Id == studentId)
                .ToDto()
                .One(x => x.Id == abonementId);
        }

        public async Task<int> AbonementChangeStatus(int studentId, int abonementId, AbonementStatus status)
        {
            var abonement = await dbIterator.Abonements(false)
                .Include(x => x.AbonementShedules)
                .Where(x => x.Student.User.Id == studentId)
                .One(x => x.Id == abonementId);

            abonement.AbonementStatus = status;
            if (status == AbonementStatus.Deleted) { abonement.AbonementShedules.Clear(); }
            await dbIterator.SaveChangesAsync();
            await publisher.Push(new AbonementChangeStatusEvent
            {
                StudentName = abonement.Student.User.DisplayName,
                StudentUserId = abonement.Student.User.Id,
                TeacherName = abonement.Course.Teacher.User.DisplayName,
                TeacherUserId = abonement.Course.Teacher.User.Id,
                CourseName = abonement.Course.Name,
                AbonementId = abonement.Id,
                AbonementStatus = abonement.AbonementStatus.ToString()
            });
            return abonement.Id;
        }

        public async Task<int> CreateReferalAbonement(int studentId, string token)
        {
            var course = await dbIterator.Courses(false)
                .One(x => x.ReferalToken == token);
            var student = await dbIterator.Students()
                .Include(x => x.Abonements)
                .One(x => x.User.Id == studentId);

            if (student.Abonements.Any(x => x.CourseId == course.Id))
            {
                throw new BusinessLogicException("Abonement already created");
            }

            var abonement = new Abonement
            {
                Course = course,
                PricePerLesson = course.Price,
                FreeLessons = course.FreeLessons,
                IsReferal = true
            };
            student.Abonements.Add(abonement);
            
            await dbIterator.SaveChangesAsync();
            return abonement.Id;
        }
    }
}
