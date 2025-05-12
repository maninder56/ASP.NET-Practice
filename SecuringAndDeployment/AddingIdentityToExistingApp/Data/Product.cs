using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AddingIdentityToExistingApp.Data;

public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    public int? Quantity { get; set; }
}
