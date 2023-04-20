using DiplomaAPI.Data;
using DiplomaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgreedController : ControllerBase
    {
        private readonly UserDbContext _context;

        public AgreedController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/Agreed
        // Returns list of all agreed details for files
        [HttpGet]
        public async Task<IEnumerable<Agreed>> Get()
        {
            return await _context.Agreed.ToListAsync();
        }

        // GET: api/Agreed/1
        // Returns specific agreed detail for file with given Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Agreed), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Agreed), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var agreedInfo = await _context.Agreed.FindAsync(id);
            return agreedInfo == null ? NotFound() : Ok(agreedInfo);
        }

        // POST: api/Agreed
        // Creates a record of agreed detail for file in the database
        [HttpPost]
        [ProducesResponseType(typeof(Agreed), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Agreed), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Agreed agreedInfo)
        {
            await _context.Agreed.AddAsync(agreedInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = agreedInfo.Id }, agreedInfo);
        }

        // PUT: /api/Agreed
        // Updates the record of agreed detail for file with the given Id in the database
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Agreed agreedInfo)
        {
            if (id != agreedInfo.Id)
                return BadRequest();

            _context.Entry(agreedInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /api/Agreed/1
        // Deletes the record of agreed detail with given Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var agreedInfoToDelete = await _context.Agreed.FindAsync(id);
            if (agreedInfoToDelete == null)
                return BadRequest();

            _context.Agreed.Remove(agreedInfoToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
