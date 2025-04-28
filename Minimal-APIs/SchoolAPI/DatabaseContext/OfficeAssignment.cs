using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseContext;

public partial class OfficeAssignment
{
    [Required]
    public int InstructorId { get; set; }

    [Required]
    [StringLength(50)]
    public string Location { get; set; } = null!;

    public virtual Person Instructor { get; set; } = null!;
}
