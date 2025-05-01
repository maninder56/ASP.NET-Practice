using DatabaseContext;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SchoolAPI.Data.SchoolRecords; 

public static class StudentGradeRecords
{
    public record StudentGradeRecord
    {
        public int EnrollmentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int StudentId { get; set; }

        public decimal? Grade { get; set; }

        public StudentGradeRecord() { }

        public StudentGradeRecord(StudentGrade studentGrade)
        {
            EnrollmentId = studentGrade.EnrollmentId;
            CourseId = studentGrade.CourseId;
            StudentId = studentGrade.StudentId;
            Grade = studentGrade.Grade;
        }
    }
}
