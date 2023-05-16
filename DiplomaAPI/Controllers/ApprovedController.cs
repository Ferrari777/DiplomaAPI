using AutoMapper;
using DiplomaAPI.Data;
using DiplomaAPI.DTOs;
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
        private readonly IMapper _mapper;

        public ApprovedController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Approved
        // Returns list of all approved details for files
        [HttpGet]
        public async Task<IEnumerable<Approved>> Get()
        {
            var approvedList = await _context.Approved
                .Include(p => p.DocFile)
                .ToListAsync();
            return approvedList;
        }

        // GET: api/Approved/1
        // Returns specific approved detail for file with given Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Approved), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var approvedInfo = await _context.Approved
                .Include(p => p.DocFile)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            
            return approvedInfo == null ? NotFound() : Ok(approvedInfo);
        }

        // POST: api/Approved
        // Creates a record of approved detail for file in the database
        [HttpPost]
        [ProducesResponseType(typeof(Approved), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ApprovedDto approvedDtoPayload)
        {
            var newApprovedInfo = _mapper.Map<Approved>(approvedDtoPayload);
            
            await _context.Approved.AddAsync(newApprovedInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newApprovedInfo.Id }, newApprovedInfo);
        }

        // PUT: /api/Approved
        // Updates the record of approved detail for file with the given Id in the database
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, ApprovedDto approvedDtoPayload)
        {
            var updatedApprovedInfo = _mapper.Map<Approved>(approvedDtoPayload);
            if (id != updatedApprovedInfo.Id)
                return BadRequest();

            _context.Entry(updatedApprovedInfo).State = EntityState.Modified;
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
