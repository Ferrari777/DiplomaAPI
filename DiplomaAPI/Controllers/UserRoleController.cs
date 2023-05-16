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
    public class UserRoleController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public UserRoleController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/UserRole
        // Returns list of all user roles
        [HttpGet]
        public async Task<IEnumerable<UserRoleDto>> Get()
        {
            var userRoleList = await _context.UserRoles.ToListAsync();
            var userRoleListDto = _mapper.Map<IEnumerable<UserRoleDto>>(userRoleList);

            return userRoleListDto;
        }

        // GET: api/UserRole/1
        // Returns specific user role with given Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserRole), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null) 
            {
                NotFound();
            }
            var userRoleDto = _mapper.Map<UserRoleDto>(userRole);

            return Ok(userRoleDto);
        }

        // POST: api/UserRole
        // Creates a user role record in the database
        [HttpPost]
        [ProducesResponseType(typeof(UserRole), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(UserRoleDto userRoleDtoPayload)
        {
            var newUserRole = _mapper.Map<UserRole>(userRoleDtoPayload);

            await _context.UserRoles.AddAsync(newUserRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newUserRole.Id }, newUserRole);
        }

        // PUT: /api/UserRole
        // Updates the user role record with the given Id in the database
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, UserRoleDto userRoleDtoPayload)
        {
            var updatedUserRole = _mapper.Map<UserRole>(userRoleDtoPayload);
            if (id != updatedUserRole.Id)
                return BadRequest();

            _context.Entry(updatedUserRole).State = EntityState.Modified;
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
            var userRoleToDelete = await _context.UserRoles
                .FindAsync(id);
            if (userRoleToDelete == null)
                return NotFound();

            _context.UserRoles.Remove(userRoleToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
