using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Server.Data;
using LMS.Shared.Models;

namespace LMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictSummaryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DistrictSummaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DistrictSummary
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictSummary>>> GetDistrictSummary()
        {
            return await _context.DistrictSummary.ToListAsync();
        }

        // GET: api/DistrictSummary/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictSummary>> GetDistrictSummary(int id)
        {
            var districtSummary = await _context.DistrictSummary.FindAsync(id);

            if (districtSummary == null)
            {
                return NotFound();
            }

            return districtSummary;
        }

        // PUT: api/DistrictSummary/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistrictSummary(int id, DistrictSummary districtSummary)
        {
            if (id != districtSummary.DistrictId)
            {
                return BadRequest();
            }

            _context.Entry(districtSummary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictSummaryExists(id))
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

        // POST: api/DistrictSummary
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DistrictSummary>> PostDistrictSummary(DistrictSummary districtSummary)
        {
            _context.DistrictSummary.Add(districtSummary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistrictSummary", new { id = districtSummary.DistrictId }, districtSummary);
        }

        // DELETE: api/DistrictSummary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrictSummary(int id)
        {
            var districtSummary = await _context.DistrictSummary.FindAsync(id);
            if (districtSummary == null)
            {
                return NotFound();
            }

            _context.DistrictSummary.Remove(districtSummary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DistrictSummaryExists(int id)
        {
            return _context.DistrictSummary.Any(e => e.DistrictId == id);
        }
    }
}
