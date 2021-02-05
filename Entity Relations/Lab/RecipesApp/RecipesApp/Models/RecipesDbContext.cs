using Microsoft.EntityFrameworkCore;
using RecipesApp.EntityConfigurations;
using RecipesApp.Models.EntityConfigurations;

namespace RecipesApp.Models
{
    public class RecipesDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients  { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public RecipesDbContext()
        {
        }

        public RecipesDbContext( DbContextOptions<RecipesDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Recipes;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RecipeConfigurations());
            modelBuilder.ApplyConfiguration(new IngredientConfigurations());

            modelBuilder.Entity<RecipeIngredient>().HasKey(x => new { x.RecipeId, x.IngredientId});
            
        }

    }
}
