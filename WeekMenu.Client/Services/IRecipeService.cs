using System.Collections.Generic;
using System.Threading.Tasks;
using WeekMenu.Client.Models;

namespace WeekMenu.Client.Services
{
    public interface IRecipeService
    {
        Task<RecipeModel> AddRecipe(RecipeModel recipe);
        Task<List<RecipeModel>> GetRecipes();
        Task<RecipeModel> GetRecipeById(int id);
        Task<RecipeModel> UpdateRecipe(RecipeModel recipe);
    }
}