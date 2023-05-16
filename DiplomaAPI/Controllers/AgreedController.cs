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
    public class AgreedController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public AgreedController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Agreed
        // Returns list of all agreed details for files
        [HttpGet]
        public async Task<IEnumerable<Agreed>> Get()
        {
            var agreedList = await _context.Agreed
                .Include(p => p.DocFile)
                .ToListAsync();
            return agreedList;
        }

        // GET: api/Agreed/1
        // Returns specific agreed detail for file with given Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Agreed), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var agreedInfo = await _context.Agreed
                .Include(p => p.DocFile)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            return agreedInfo == null ? NotFound() : Ok(agreedInfo);
        }

        // POST: api/Agreed
        // Creates a record of agreed detail for file in the database
        [HttpPost]
        [ProducesResponseType(typeof(Agreed), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(AgreedDto agreedDtoPayload)
        {
            var newAgreedInfo = _mapper.Map<Agreed>(agreedDtoPayload);
            
            await _context.Agreed.AddAsync(newAgreedInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newAgreedInfo.Id }, newAgreedInfo);
        }

        // PUT: /api/Agreed
        // Updates the record of agreed detail for file with the given Id in the database
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, AgreedDto agreedDtoPayload)
        {
            var updatedAgreedInfo = _mapper.Map<Agreed>(agreedDtoPayload);
            if (id != updatedAgreedInfo.Id)
                return BadRequest();

            _context.Entry(updatedAgreedInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /api/Agreed/1
        // Deletes the record of agreed detail with given Id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
