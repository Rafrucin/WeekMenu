using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeekMenu.Client.Data;
using WeekMenu.Client.Models;

namespace WeekMenu.Client.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayMenuModelsController : ControllerBase
    {
        private readonly ModelsDbContext _context;

        public DayMenuModelsController(ModelsDbContext context)
        {
            _context = context;
        }

        // GET: api/DayMenuModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DayMenuModel>>> GetDaysDBSet()
        {
            return await _context.DaysDBSet.ToListAsync();
        }

        // GET: api/DayMenuModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DayMenuModel>> GetDayMenuModel(int id)
        {
            var dayMenuModel = await _context.DaysDBSet.FindAsync(id);

            if (dayMenuModel == null)
            {
                return NotFound();
            }

            return dayMenuModel;
        }

        // PUT: api/DayMenuModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDayMenuModel(int id, DayMenuModel dayMenuModel)
        {
            if (id != dayMenuModel.DayMenuModelId)
            {
                return BadRequest();
            }

            _context.Entry(dayMenuModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayMenuModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DayMenuModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DayMenuModel>> PostDayMenuModel(DayMenuModel dayMenuModel)
        {
            _context.DaysDBSet.Add(dayMenuModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDayMenuModel", new { id = dayMenuModel.DayMenuModelId }, dayMenuModel);
        }

        // DELETE: api/DayMenuModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DayMenuModel>> DeleteDayMenuModel(int id)
        {
            var dayMenuModel = await _context.DaysDBSet.FindAsync(id);
            if (dayMenuModel == null)
            {
                return NotFound();
            }

            _context.DaysDBSet.Remove(dayMenuModel);
            await _context.SaveChangesAsync();

            return dayMenuModel;
        }

        private bool DayMenuModelExists(int id)
        {
            return _context.DaysDBSet.Any(e => e.DayMenuModelId == id);
        }
    }
}
