using DiplomaAPI.Data;
using DiplomaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserDbContext _context;
        
        public UserRoleController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/UserRole
        // Returns list of all user roles
        [HttpGet]
        public async Task<IEnumerable<UserRole>> Get()
        {
            return await _context.UserRoles.ToListAsync();
        }

        // GET: api/UserRole/1
        // Returns specific user role with given Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserRole), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserRole), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            return userRole == null ? NotFound() : Ok(userRole);
        }

        // POST: api/UserRole
        // Creates a user role record in the database
        [HttpPost]
        [ProducesResponseType(typeof(UserRole), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(UserRole), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = userRole.Id }, userRole);
        }

        // PUT: /api/UserRole
        // Updates the user role record with the given Id in the database
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, UserRole userRole)
        {
            if (id != userRole.Id)
                return BadRequest();

            _context.Entry(userRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /api/UserRole/1
        // Deletes the record of specific user with given Id
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var userRoleToDelete = await _context.UserRoles.FindAsync(id);
            if (userRoleToDelete == null)
                return NotFound();

            _context.UserRoles.Remove(userRoleToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
