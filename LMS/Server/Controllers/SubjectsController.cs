using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Server.Data;
using LMS.Shared.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public SubjectsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return await _context.Subjects.Include(a=>a.Grade).ToListAsync();
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return BadRequest();
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {            
            var folderName = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Subject");            
            var path = Path.Combine(folderName, subject.File.FileName);
            if (!System.IO.Directory.Exists(folderName))
            {
                System.IO.Directory.CreateDirectory(folderName);
            }
            var fs = System.IO.File.Create(path);
            fs.Write(subject.File.FileContent, 0, subject.File.FileContent.Length);
            fs.Close();
            subject.ImagePath = _environment.WebRootPath +  "/Upload" + "/Subject/" + subject.File.FileName;
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSubject", new { id = subject.SubjectId }, subject);
        }
        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       /* [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject([FromForm] IEnumerable<IFormFile> files, Subject subject, IFormFile Attachment)
        {
            var maxAllowedFiles = 3;
            long maxFileSize = 1024 * 1024 * 15;
            var filesProcessed = 0;
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");            

            foreach (var file in files)
            {                
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;                            

                if (filesProcessed < maxAllowedFiles)
                {
                    if (file.Length == 0)
                    {                                                
                    }
                    else if (file.Length > maxFileSize)
                    {                                                
                    }
                    else
                    {
                        try
                        {
                            trustedFileNameForFileStorage = Path.GetRandomFileName();
                            var path = Path.Combine(_environment.ContentRootPath,
                                _environment.EnvironmentName, "unsafe_uploads",
                                trustedFileNameForFileStorage);

                            await using FileStream fs = new(path, FileMode.Create);
                            await file.CopyToAsync(fs);                            
                        }
                        catch (IOException ex)
                        {                                                       
                        }
                    }

                    filesProcessed++;
                }
                else
                {                                        
                }                
            }
            string fileExtenstion = subject.File.FileType.ToLower().Contains("png") ? "png" : "jpg";
            string fileName = $@"{Guid.NewGuid()}.{fileExtenstion}";
            var path1 = Path.Combine(_environment.ContentRootPath, "Upload", fileName);
            await using FileStream fs1 = new(path1, FileMode.Create);
            await Attachment.OpenReadStream().CopyToAsync(fs1);
            subject.ImagePath = path1;
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new { id = subject.SubjectId }, subject);
        }
*/
        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.SubjectId == id);
        }
    }
}
