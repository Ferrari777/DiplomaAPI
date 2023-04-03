using DiplomaAPI.Data;
using DiplomaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocFileController : ControllerBase
    {
        private readonly UserDbContext _context;

        public DocFileController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/DocFile
        // Returns list of all documentary files
        [HttpGet]
        public async Task<IEnumerable<DocFile>> Get()
        {
            return await _context.DocFiles.ToListAsync();
        }

        // GET: api/DocFile/1
        // Returns specific documentary file with given Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocFile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DocFile), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var docFile = await _context.DocFiles.FindAsync(id);
            return docFile == null ? NotFound() : Ok(docFile);
        }

        // POST: api/DocFile
        // Creates a record of documentary file in the database
        [HttpPost]
        [ProducesResponseType(typeof(DocFile), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(DocFile), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(DocFile docFile)
        {
            await _context.DocFiles.AddAsync(docFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = docFile.Id}, docFile);
        }

        // PUT: /api/DocFile
        // Updates the record of documentary file with the given Id in the database
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, DocFile docFile)
        {
            if (id != docFile.Id)
                return BadRequest();

            _context.Entry(docFile).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /api/DocFile/1
        // Deletes the record of specific documentary file with given Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fileDocToDelete = await _context.DocFiles.FindAsync(id);
            if (fileDocToDelete == null)
                return BadRequest();

            _context.DocFiles.Remove(fileDocToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
