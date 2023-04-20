using DiplomaAPI.Data;
using DiplomaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovedController : ControllerBase
    {
        private readonly UserDbContext _context;

        public ApprovedController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/Approved
        // Returns list of all approved details for files
        [HttpGet]
        public async Task<IEnumerable<Approved>> Get()
        {
            return await _context.Approved.ToListAsync();
        }

        // GET: api/Approved/1
        // Returns specific approved detail for file with given Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Approved), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Approved), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var approvedInfo = await _context.Approved.FindAsync(id);
            return approvedInfo == null ? NotFound() : Ok(approvedInfo);
        }

        // POST: api/Approved
        // Creates a record of approved detail for file in the database
        [HttpPost]
        [ProducesResponseType(typeof(Approved), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Approved), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Approved approvedInfo)
        {
            await _context.Approved.AddAsync(approvedInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = approvedInfo.Id }, approvedInfo);
        }

        // PUT: /api/Approved
        // Updates the record of approved detail for file with the given Id in the database
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Approved approvedInfo)
        {
            if (id != approvedInfo.Id)
                return BadRequest();

            _context.Entry(approvedInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /api/Approved/1
        // Deletes the record of approved detail with given Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var approvedInfoToDelete = await _context.Approved.FindAsync(id);
            if (approvedInfoToDelete == null)
                return BadRequest();

            _context.Approved.Remove(approvedInfoToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
