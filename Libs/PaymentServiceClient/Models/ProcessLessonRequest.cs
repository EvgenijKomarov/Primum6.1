using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PaymentServiceConnection.Models
{
    public record LessonPaymentRequest(
        [property: JsonPropertyName("studentUserId")] int StudentUserId,
        [property: JsonPropertyName("teacherUserId")] int TeacherUserId,
        [property: JsonPropertyName("teacherCash")] decimal TeacherCash,
        [property: JsonPropertyName("platformCash")] decimal PlatformCash
    );
}
