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
            BreakfastSetter();
            SecondBreakfastSetter();
            LunchSetter();
            AfternoonTeaSetter();
            DinnerSetter();

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

        public RecipeModel BreakfastSetter()
        {
            int total = _context.RecipesDBSet.Where(x => x.IsBreakfast == true).Count();
            Random r = new Random();
            int offset = r.Next(0, total);
            var result = _context.RecipesDBSet.Skip(offset).FirstOrDefault();

            if(UniqueChecker(result))
            {
                return result;
            }
            else
            {            
                BreakfastSetter();
            }
            return result;

        }

        public void SecondBreakfastSetter()
        {
            int total = _context.RecipesDBSet.Where(x => x.IsSecondBreakfast == true).Count();
            Random r = new Random();
            int offset = r.Next(0, total);
            var result = _context.RecipesDBSet.Skip(offset).FirstOrDefault();

            if (UniqueChecker(result))
            {
                return;
            }
            SecondBreakfastSetter();
        }

        private void LunchSetter()
        {
            int total = _context.RecipesDBSet.Where(x => x.IsLunch == true).Count();
            Random r = new Random();
            int offset = r.Next(0, total);
            var result = _context.RecipesDBSet.Skip(offset).FirstOrDefault();

            if (UniqueChecker(result))
            {
                return;
            }
            LunchSetter();
        }

        private void AfternoonTeaSetter()
        {
            int total = _context.RecipesDBSet.Where(x => x.IsAfternoonTea == true).Count();
            Random r = new Random();
            int offset = r.Next(0, total);
            var result = _context.RecipesDBSet.Skip(offset).FirstOrDefault();

            if (UniqueChecker(result))
            {
                return;
            }
            AfternoonTeaSetter();
        }
        private void DinnerSetter()
        {
            int total = _context.RecipesDBSet.Where(x => x.IsDinner == true).Count();
            Random r = new Random();
            int offset = r.Next(0, total);
            var result = _context.RecipesDBSet.Skip(offset).FirstOrDefault();

            if (UniqueChecker(result))
            {
                return;
            }
            DinnerSetter();
        }
    }
}
