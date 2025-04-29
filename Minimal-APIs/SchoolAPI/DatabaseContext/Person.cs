using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseContext;

public partial class Person
{
    [Required]
    public int PersonId { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    public DateTime? HireDate { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    [Required]
    [StringLength(50)]
    public string Discriminator { get; set; } = null!;

    public virtual OfficeAssignment? OfficeAssignment { get; set; }

    public virtual ICollection<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
