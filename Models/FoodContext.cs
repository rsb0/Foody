using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Foody.Models
{
    public class FoodContext : DbContext
    {
        public FoodContext(DbContextOptions<FoodContext> options) : base(options)
        {

        }
        public DbSet<FoodItem> FoodItems { get; set; } = null;
    }
}