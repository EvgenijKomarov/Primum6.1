using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class Added_Ranks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -1,
                column: "Rank",
                value: "Экспериментальный");

            migrationBuilder.InsertData(
                table: "CourseRanks",
                columns: new[] { "Id", "CreatedAt", "Level", "Rank", "RequiredExperience" },
                values: new object[,]
                {
                    { -10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Легендарный", 20000 },
                    { -9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Флагманский", 17000 },
                    { -8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Высшей пробы", 14000 },
                    { -7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Экспертный", 12000 },
                    { -6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Мастерский", 10000 },
                    { -5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Продвинутый", 8000 },
                    { -4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Проверенный временем", 6000 },
                    { -3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Базовый", 4000 },
                    { -2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Новаторский", 2000 }
                });

            migrationBuilder.InsertData(
                table: "StudentRanks",
                columns: new[] { "Id", "CoinDiscount", "CreatedAt", "Level", "Rank", "RequiredExperience" },
                values: new object[,]
                {
                    { -10, 0.1f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Легенда школы", 20000 },
                    { -9, 0.085f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Гений алгоритмов", 17000 },
                    { -8, 0.07f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Виртуоз кода", 14000 },
                    { -7, 0.06f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Мастер логики", 12000 },
                    { -6, 0.05f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Опытный программист", 10000 },
                    { -5, 0.04f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Разработчик-практик", 8000 },
                    { -4, 0.03f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Уверенный кодер", 6000 },
                    { -3, 0.02f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Любознательный ученик", 4000 },
                    { -2, 0.01f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Начинающий программист", 2000 }
                });

            migrationBuilder.InsertData(
                table: "TeacherRanks",
                columns: new[] { "Id", "CreatedAt", "EarningMultiplier", "Level", "Rank", "RequiredExperience" },
                values: new object[,]
                {
                    { -10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.2f, 10, "Легенда преподавания", 20000 },
                    { -9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.17f, 9, "Ветеран педагогики", 17000 },
                    { -8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.14f, 8, "Гуру алгоритмов", 14000 },
                    { -7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.12f, 7, "Мастер обучения", 12000 },
                    { -6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.1f, 6, "Учитель-виртуоз", 10000 },
                    { -5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.08f, 5, "Наставник-эксперт", 8000 },
                    { -4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.06f, 4, "Опытный куратор", 6000 },
                    { -3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.04f, 3, "Практикующий тренер", 4000 },
                    { -2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.02f, 2, "Молодой ментор", 2000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "StudentRanks",
                keyColumn: "Id",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "StudentRanks",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "StudentRanks",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "StudentRanks",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "StudentRanks",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "StudentRanks",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "StudentRanks",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "StudentRanks",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "StudentRanks",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.UpdateData(
                table: "CourseRanks",
                keyColumn: "Id",
                keyValue: -1,
                column: "Rank",
                value: "Новый");
        }
    }
}
