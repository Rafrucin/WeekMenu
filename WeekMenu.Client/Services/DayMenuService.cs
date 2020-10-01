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

        public async Task<DayMenuModel> GetDayMenuByDate(DateTime date)
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

        public async Task<DayMenuModel> SaveDayMenuModel(DayMenuModel dayMenuModel)
        {
            var result = _context.DaysDBSet.Add(dayMenuModel);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<DayMenuModel> GetDayMenuModel(DateTime date)
        {
            var dayMenuModel = _context.DaysDBSet.AsQueryable();
            var result = await dayMenuModel.Where(x => x.DayMenuDate == date).FirstOrDefaultAsync();

            if (dayMenuModel == null)
            {
                return new DayMenuModel();
            }

            return result;
        }

        public async Task UpdateDayMenuModel(DayMenuModel dayMenuModel)
        {
            _context.Entry(dayMenuModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayMenuModelExists(dayMenuModel.DayMenuModelId))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool DayMenuModelExists(int id)
        {
            return _context.DaysDBSet.Any(e => e.DayMenuModelId == id);
        }

    }
}
