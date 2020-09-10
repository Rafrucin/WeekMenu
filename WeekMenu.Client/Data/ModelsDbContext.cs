using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeekMenu.Client.Models;

namespace WeekMenu.Client.Data
{
    public class ModelsDbContext : DbContext
    {
        public ModelsDbContext(DbContextOptions options) : base(options) { }
        public DbSet<DayMenuModel> DaysDBSet { get; set; }
        public DbSet<RecipeModel> RecipesDBSet { get; set; } 

    }
}
