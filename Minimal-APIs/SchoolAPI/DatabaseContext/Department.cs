using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DatabaseContext;

public partial class Department
{
    [Required]
    public int DepartmentId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    public decimal Budget { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public int? Administrator { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
