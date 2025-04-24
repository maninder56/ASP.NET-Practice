using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseContext;

public partial class OnlineCourse
{
    [Required]
    public int CourseId { get; set; }

    [Required]
    [StringLength(100)]
    public string Url { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
