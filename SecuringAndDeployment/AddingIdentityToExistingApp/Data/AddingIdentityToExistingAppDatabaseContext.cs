using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AddingIdentityToExistingApp.Data;

public partial class AddingIdentityToExistingAppDatabaseContext : IdentityDbContext<ApplicationUser>
{
    public AddingIdentityToExistingAppDatabaseContext()
    {
    }

    public AddingIdentityToExistingAppDatabaseContext(DbContextOptions<AddingIdentityToExistingAppDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED3B4BDDD9");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
