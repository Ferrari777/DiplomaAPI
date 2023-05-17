using DiplomaAPI.Data;
using DiplomaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UserController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        /// <summary>
        /// Returns a list of all users
        /// </summary>
        /// <returns>A list of users</returns>
        /// <remarks>
        /// The endpoint 'GET api/User' returns a list of all users.  
        /// </remarks>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/1
        /// <summary>
        /// Returns a specific user with given Id
        /// </summary>
        /// <returns>A user with given Id</returns>
        /// <remarks>
        /// The endpoint 'GET api/User/{id}' returns a specific user with given id.
        /// If the request was successful, it'll return the 200 status code.
        /// If the user wasn't found, it'll return the 404 status code.
        /// </remarks>
        /// <response code="200">OK - Returns a specific user with given Id</response>
        /// <response code="404">Not Found - If the user wasn't found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        // POST: api/User
        // Creates a user record in the database
        /// <summary>
        /// Creates a user record in the database
        /// </summary>
        /// <returns>A newly created user record.</returns>
        /// <remarks>
        /// The endpoint 'POST api/User' creates a new user in database.
        /// If the request was successful, it'll return the 201 status code.
        /// If the user payload was null, it'll return the 400 status code.
        /// </remarks>
        /// <response code="201">Created - Returns the newly created user record</response>
        /// <response code="400">Bad Request - If the user payload was null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // PUT: /api/User/1
        /// <summary>
        /// Updates the user record with the given Id in the database
        /// </summary>
        /// <returns>An updated user record in the database.</returns>
        /// <remarks>
        /// The endpoint 'PUT api/User/{id}' updates an existing user in database with given id.
        /// If the request was successful, it'll return the 204 status code.
        /// If the user payload was null or wrong, it'll return the 400 status code.
        /// If the user with given id wasn't found, it'll return the 404 status code.
        /// </remarks>
        /// <response code="204">No Content - If the request was successful</response>
        /// <response code="400">Bad Request - If the user payload was null or wrong</response>
        /// <response code="404">Not Found - If the user payload wasn't found with given id</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, User user)
        {
            var userToUpdate = await _context.Users.FindAsync(id);
            if (userToUpdate == null)
                return NotFound();
            else
            if (id != user.Id)
                return BadRequest();

            // EF Core is tracking of the duplicate of the entity 'userToUpdate'
            // with the same primary key may cause an error so it must be detached
            // for successful updating of the entered payload
            _context.Entry(userToUpdate).State = EntityState.Detached;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /api/User/1
        /// <summary>
        /// Deletes the record of specific user with given Id
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The endpoint 'DELETE api/User/{id}' deletes an existing user in database with given id.
        /// If the request was successful, it'll return the 204 status code.
        /// If the user's id wasn't found, it'll return the 404 status code.
        /// </remarks>
        /// <response code="204">No Content - If the request was successful</response>
        /// <response code="404">Not Found - If the user's id wasn't found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if (userToDelete == null)
                return NotFound();

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}