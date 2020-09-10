using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeekMenu.Client.Data;
using WeekMenu.Client.Models;

namespace WeekMenu.Client.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ModelsDbContext _context;

        public RecipeService(ModelsDbContext context)
        {
            _context = context;
        }

        public async Task<RecipeModel> AddRecipe(RecipeModel recipe)
        {
            _context.RecipesDBSet.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<List<RecipeModel>> GetRecipes()
        { 
            return await _context.RecipesDBSet.ToListAsync(); 
        }


        public async Task<RecipeModel> GetRecipeById(int id)
        {
            var recipe = await _context.RecipesDBSet.FindAsync(id);
            if (recipe==null)
            {
                recipe = new RecipeModel();
            }
            return recipe;
        }

        public async Task<RecipeModel> UpdateRecipe(RecipeModel recipe)
        {
            _context.Entry(recipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return recipe;
        }
    }
}
