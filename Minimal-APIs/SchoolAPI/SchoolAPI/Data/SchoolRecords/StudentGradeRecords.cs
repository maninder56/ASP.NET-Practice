using DatabaseContext;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Data.SchoolRecords; 

public class StudentGradeRecords
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
