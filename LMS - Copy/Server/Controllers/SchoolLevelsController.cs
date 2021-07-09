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
    public class SchoolLevelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SchoolLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SchoolLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolLevel>>> GetSchoolLevels()
        {
            return await _context.SchoolLevels.ToListAsync();
        }      
    }
}
