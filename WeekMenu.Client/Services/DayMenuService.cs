using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WeekMenu.Client.Data;
using WeekMenu.Client.Models;

namespace WeekMenu.Client.Services
{
    public class DayMenuService : IDayMenuService
    {
        private readonly ModelsDbContext _context;

        public DayMenuService(ModelsDbContext context)
        {
            _context = context;
        }

        public async Task<DayMenuModel> GetRecipeByDate(DateTime date)
        {
            var dayMenu = await _context.DaysDBSet.Where(x => x.DayMenuDate.Date == date.Date).
                Include(x => x.Breakfast).
                Include(x => x.SecondBreakfast).
                Include(x => x.Lunch).
                Include(x => x.AfternoonTea).
                Include(x => x.Dinner).FirstOrDefaultAsync();

            if (dayMenu == null)
            {
                dayMenu = new DayMenuModel();
            }
            return dayMenu;
        }

    }
}
