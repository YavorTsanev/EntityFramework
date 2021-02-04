using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipesApp.Models.EntityConfigurations
{
    public class RecipeConfigurations : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(x => new { x.Id, x.Name });

            builder.Property(x => x.Name).HasColumnName("Title").IsUnicode();

            builder.Property<int>("EGN").IsRequired();

            

        }
    }
}
