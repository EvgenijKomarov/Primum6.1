using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs.Inputs
{
    public class GradingInputDto
    {
        public Grading HomeworkGrade { get; set; } = Grading.None;

        public Grading LessonActivityGrade { get; set; } = Grading.None;

        public Grading RepetitionOfMaterialGrade { get; set; } = Grading.None;

        public Grading StudyInitiativeGrade { get; set; } = Grading.None;
    }
}
