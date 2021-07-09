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
    public class GandersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GandersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ganders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gander>>> GetGanders()
        {
            return await _context.Ganders.ToListAsync();
        }
      
    }
}
