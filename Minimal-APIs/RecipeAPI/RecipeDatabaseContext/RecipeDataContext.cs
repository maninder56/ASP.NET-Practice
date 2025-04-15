using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RecipeDatabaseContext;

public partial class RecipeDataContext : DbContext
{
    public RecipeDataContext()
    {
    }

    public RecipeDataContext(DbContextOptions<RecipeDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__BEAEB27AD3BB5EA8");

            entity.ToTable("Ingredient");

            entity.Property(e => e.IngredientId)
                .ValueGeneratedNever()
                .HasColumnName("IngredientID");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Recipe).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Ingredien__Recip__3A81B327");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipies__FDD988D0E4D28C52");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedNever()
                .HasColumnName("RecipeID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
