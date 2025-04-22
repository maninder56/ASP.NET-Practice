using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseContext;

public partial class Course
{
    [Required]
    public int CourseId { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    public int Credits { get; set; }

    [Required]
    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual OnlineCourse? OnlineCourse { get; set; }

    public virtual OnsiteCourse? OnsiteCourse { get; set; }

    public virtual ICollection<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
