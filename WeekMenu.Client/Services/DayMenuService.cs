using Blazored.LocalStorage;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
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
        private readonly ILocalStorageService _localStorage;
        string menuOwner = string.Empty;

        public DayMenuService(ModelsDbContext context, ILocalStorageService localStorage)
        {
            _context = context;
            _localStorage = localStorage;           
        }

        public async Task<DayMenuModel> GetDayMenuByDate(DateTime date)
        {
            if (string.IsNullOrEmpty(menuOwner))
            {
                menuOwner = await _localStorage.GetItemAsync<string>("MenuOwner");
                if (string.IsNullOrEmpty(menuOwner))
                {
                    return new DayMenuModel();
                }
            }
            
            var dayMenu = await _context.DaysDBSet.
                Where(x => x.DayMenuDate.Date == date.Date && x.DayMenuOwner==menuOwner).
                Include(x => x.Breakfast).
                Include(x => x.SecondBreakfast).
                Include(x => x.Lunch).
                Include(x => x.AfternoonTea).
                Include(x => x.Dinner).FirstOrDefaultAsync();

            if (dayMenu == null)
            {
                return new DayMenuModel();
            } 
            return dayMenu;
        }

        public async Task<DayMenuModel> SaveDayMenuModel(DayMenuModel dayMenuModel)
        {
            if (string.IsNullOrEmpty(menuOwner))
            {
                menuOwner = await _localStorage.GetItemAsync<string>("MenuOwner");
                if (string.IsNullOrEmpty(menuOwner))
                {
                    menuOwner = Guid.NewGuid().ToString();
                    await _localStorage.SetItemAsync<string>("MenuOwner", menuOwner);
                }
            }

            dayMenuModel.DayMenuOwner = menuOwner;
            dayMenuModel.DayMenuDate = dayMenuModel.DayMenuDate.Date;

            var result = _context.DaysDBSet.Add(dayMenuModel);
            await _context.SaveChangesAsync();

            return result.Entity;
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
