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
    [ApiController]
    public class UnionCouncilsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnionCouncilsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UnionCouncils
        [HttpGet]
        [Route("api/UnionCouncils")]
        public async Task<ActionResult<IEnumerable<UnionCouncil>>> GetUnionCouncils()
        {
            return await _context.UnionCouncils.Include(a=>a.Tehsil.District).ToListAsync();
        }
        [HttpGet]
        [Route("api/UnioncouncilsOfTehsil/{id}")]
        public async Task<ActionResult<IEnumerable<UnionCouncil>>> GetUnionCouncils(int id)
        {
            return await _context.UnionCouncils.Where(a=>a.TehsilId == id).ToListAsync();
        }
        // GET: api/UnionCouncils/5
        [HttpGet]
        [Route("api/UnionCouncilGet/{id}")]
        public async Task<ActionResult<UnionCouncil>> GetUnionCouncilGet(int id)
        {
            var unionCouncil = await _context.UnionCouncils.Include(a=>a.Tehsil.District).Where(a=>a.UnionCouncilId == id).FirstOrDefaultAsync();

            if (unionCouncil == null)
            {
                return NotFound();
            }

            return unionCouncil;
        }

        // PUT: api/UnionCouncils/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("api/UnionCouncilUpdate/{id}")]
        public async Task<IActionResult> PutUnionCouncilUpdate(int id, UnionCouncil unionCouncil)
        {
            if (id != unionCouncil.UnionCouncilId)
            {
                return BadRequest();
            }

            _context.Entry(unionCouncil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnionCouncilExists(id))
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

        // POST: api/UnionCouncils
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api/UnionCouncilCreate")]
        public async Task<ActionResult<UnionCouncil>> PostUnionCouncilCreate(UnionCouncil unionCouncil)
        {
            _context.UnionCouncils.Add(unionCouncil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnionCouncil", new { id = unionCouncil.UnionCouncilId }, unionCouncil);
        }

        // DELETE: api/UnionCouncils/5
        [HttpDelete]
        [Route("api/TehsilRemove/{id}")]
        public async Task<IActionResult> DeleteUnionCouncilRemove(int id)
        {
            var unionCouncil = await _context.UnionCouncils.FindAsync(id);
            if (unionCouncil == null)
            {
                return NotFound();
            }

            _context.UnionCouncils.Remove(unionCouncil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnionCouncilExists(int id)
        {
            return _context.UnionCouncils.Any(e => e.UnionCouncilId == id);
        }
    }
}
