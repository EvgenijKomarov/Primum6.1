using CoreConnection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs.Inputs
{
    public class GradingInputDto
    {
        public Grade HomeworkGrade { get; set; } = Grade.None;

        public Grade LessonActivityGrade { get; set; } = Grade.None;

        public Grade RepetitionOfMaterialGrade { get; set; } = Grade.None;

        public Grade StudyInitiativeGrade { get; set; } = Grade.None;
    }
}
