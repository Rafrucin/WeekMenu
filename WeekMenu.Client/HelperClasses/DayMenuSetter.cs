using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WeekMenu.Client.Data;
using WeekMenu.Client.Models;
using WeekMenu.Client.Services;

namespace WeekMenu.Client.HelperClasses
{
    public class DayMenuSetter
    {
        private readonly ModelsDbContext _context;
        private List<RecipeModel> recipes = new List<RecipeModel>();

        public DayMenuSetter(ModelsDbContext context)
        {
            _context = context;
        }

        public async Task<DayMenuModel> GetDayMenuAsync(DateTime date)
        {
            recipes = new List<RecipeModel>();
            await BreakfastSetterAsync();
            await SecondBreakfastSetterAsync();
            await LunchSetterAsync();
            await AfternoonTeaSetterAsync();
            await DinnerSetterAsync();

            DayMenuModel output = new DayMenuModel
            {
                DayMenuDate = date,
                Breakfast = recipes[0],
                SecondBreakfast = recipes[1],
                Lunch = recipes[2],
                AfternoonTea = recipes[3],
                Dinner = recipes[4]
            };
            return output;
        }

        bool UniqueChecker(RecipeModel model)
        {                      
            if (recipes.Contains(model))
            {
                return false;
            }
            recipes.Add(model);
            return true;
        }

        public async Task<RecipeModel> BreakfastSetterAsync()
        {
            int total = await _context.RecipesDBSet.Where(x => x.IsBreakfast == true).CountAsync();
            Random r = new Random();
            int offset = r.Next(0, total);
            Console.WriteLine(r);
            var result =  await _context.RecipesDBSet.Where(x=>x.IsBreakfast==true).Skip(offset).FirstAsync();

            recipes.Add(result);

            return result;

        }

        public async Task<RecipeModel> SecondBreakfastSetterAsync()
        {
            int total = await _context.RecipesDBSet.Where(x => x.IsSecondBreakfast == true).CountAsync();
            Random r = new Random();
            int offset = r.Next(0, total);
            var result = await _context.RecipesDBSet.Where(x => x.IsSecondBreakfast == true).Skip(offset).FirstOrDefaultAsync();

            if (UniqueChecker(result))
            {
                return result;
            }
            else
            {
                await SecondBreakfastSetterAsync();
            }

            return result;
        }

        private async Task<RecipeModel> LunchSetterAsync()
        {
            int total = await _context.RecipesDBSet.Where(x => x.IsLunch == true).CountAsync();
            Random r = new Random();
            int offset = r.Next(0, total);
            var result =await _context.RecipesDBSet.Where(x => x.IsLunch == true).Skip(offset).FirstOrDefaultAsync();

            if (UniqueChecker(result))
            {
                return result;
            }
            await LunchSetterAsync();
 
            return result;
        }

        private async Task<RecipeModel> AfternoonTeaSetterAsync()
        {
            int total = await _context.RecipesDBSet.Where(x => x.IsAfternoonTea == true).CountAsync();
            Random r = new Random();
            int offset = r.Next(0, total);
            var result = await _context.RecipesDBSet.Where(x => x.IsAfternoonTea == true).Skip(offset).FirstOrDefaultAsync();

            if (UniqueChecker(result))
            {
                return result;
            }
            await AfternoonTeaSetterAsync();

            return result;
        }
        private async Task<RecipeModel> DinnerSetterAsync()
        {
            int total = await _context.RecipesDBSet.Where(x => x.IsDinner == true).CountAsync();
            Random r = new Random();
            int offset = r.Next(0, total);
            var result =await _context.RecipesDBSet.Where(x => x.IsDinner == true).Skip(offset).FirstOrDefaultAsync();

            if (UniqueChecker(result))
            {
                return result;
            }
            await DinnerSetterAsync();

            return result;
        }
    }
}
