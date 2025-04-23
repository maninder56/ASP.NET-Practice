using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseContext;

public partial class OnsiteCourse
{
    [Required]
    public int CourseId { get; set; }

    [Required]
    [StringLength(50)]
    public string Location { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Days { get; set; } = null!;

    [Required]
    public DateTime Time { get; set; }

    public virtual Course Course { get; set; } = null!;
}
