using DiplomaAPI.Data;
using DiplomaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DiplomaAPI.DTOs;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public UserController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/User
        // Returns list of all users
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var userList = await _context.Users
                .Include(p => p.UserRole).ToListAsync();

            return userList;
        }

        // GET: api/User/1
        // Returns specific user with given Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users
                       .Include(p => p.UserRole).Where(p => p.Id == id)
                       .FirstOrDefaultAsync();

            return user == null ? NotFound() : Ok(user);
        }

        // POST: api/User
        // Creates a user record in the database
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(UserDto userDtoPayload)
        {
            var newUser = _mapper.Map<User>(userDtoPayload);

            //newUser.CreatedDatetime = DateTime.Now; 
            //newUser.UpdatedDatetime = DateTime.Now;

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        // PUT: /api/User
        // Updates the user record with the given Id in the database
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, UserDto userDtoPayload)
        {
            var updatedUser = _mapper.Map<User>(userDtoPayload);

            //if (updatedUser.CreatedDatetime != GetById(updatedUser.Id).)
            //updatedUser.UpdatedDatetime = DateTime.Now;

            if (id != updatedUser.Id)
                return BadRequest();

            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /api/User/1
        // Deletes the record of specific user with given Id
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = await _context.Users
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            if (userToDelete == null)
                return NotFound();

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
