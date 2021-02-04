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


            builder.Property<int>("EGN").IsRequired();

            

        }
    }
}
