using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseContext;

public partial class StudentGrade
{
    [Required]
    public int EnrollmentId { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    public int StudentId { get; set; }

    public decimal? Grade { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Person Student { get; set; } = null!;
}
