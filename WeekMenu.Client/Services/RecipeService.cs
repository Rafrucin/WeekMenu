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

        public async Task<RecipeModel> BreakfastSetterAsync()
        {
            int total = await _context.RecipesDBSet.Where(x => x.IsBreakfast == true).CountAsync();
            Random r = new Random();
            int offset = r.Next(0, total);
            var result = await _context.RecipesDBSet.Skip(offset).FirstOrDefaultAsync();

            //if (UniqueChecker(result))
            //{
            //    return result;
            //}
            //else
            //{
            //    BreakfastSetter();
            //}
            return result;

        }

        public async Task<List<RecipeModel>> Get5Random()
        {
            int total = await _context.RecipesDBSet.CountAsync();

            List<RecipeModel> output = new List<RecipeModel>();

            for (int i = 0; i < 5; i++)
            {                
                Random r = new Random();
                int offset = r.Next(0, total);            
                var result = _context.RecipesDBSet.Skip(offset).FirstOrDefault();
                if (output.Contains(result))
                {
                    i--;
                }
                else
                {
                    output.Add(result);
                }
                
            }
            return output;
        }
        

        public async Task<List<RecipeModel>> RecipeFinderAsync(RecipeModel model)
        {
            var recipes = _context.RecipesDBSet.AsQueryable();
            if (model.IsVegan == true)
            {
                recipes = recipes.Where(x => x.IsVegan == model.IsVegan);
            }

            if (string.IsNullOrWhiteSpace(model.RecipeName)==false)
            {
                recipes = recipes.Where(x => x.RecipeName.Contains(model.RecipeName));
            }
            if (model.IsBreakfast==true)
            {
                recipes = recipes.Where(x => x.IsBreakfast == model.IsBreakfast);
            }
            if (model.IsSecondBreakfast==true)
            {
                recipes = recipes.Where(x => x.IsSecondBreakfast == model.IsSecondBreakfast);
            }
            if (model.IsLunch==true)
            {
                recipes = recipes.Where(x => x.IsLunch == model.IsLunch);
            }
            if (model.IsDinner==true)
            {
                recipes = recipes.Where(x => x.IsDinner == model.IsDinner);
            }
            if (model.IsAfternoonTea==true)
            {
                recipes = recipes.Where(x => x.IsAfternoonTea == model.IsAfternoonTea);
            }

            return await recipes.OrderBy(x=>x.RecipeName.ToLower()).ToListAsync();                                       
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
