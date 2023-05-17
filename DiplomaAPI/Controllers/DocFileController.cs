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
    public class DocFileController : ControllerBase
    {
        private readonly UserDbContext _context;

        public DocFileController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/DocFile
        /// <summary>
        /// Returns list of all chemical files
        /// </summary>
        /// <returns>A list of chemical files</returns>
        /// <remarks>
        /// The endpoint 'GET api/DocFile' returns a list of all records
        /// of documentation files of chemical substances.  
        /// </remarks>
        [HttpGet]
        public async Task<IEnumerable<DocFile>> Get()
        {
            return await _context.DocFiles.ToListAsync();
        }

        // GET: api/DocFile/1
        /// <summary>
        /// Returns the record of documentation file of chemical substances with given Id.
        /// </summary>
        /// <returns>A record of documentation file of chemical substance with given Id</returns>
        /// <remarks>
        /// The endpoint 'GET api/DocFile/{id}' returns 
        /// a specific documentation file of chemical substance with given id.
        /// If the request was successful, it'll return the 200 status code.
        /// If the documentation file of chemical substance wasn't found, it'll return the 404 status code.
        /// </remarks>
        /// <response code="200">OK - Returns a specific user with given Id</response>
        /// <response code="404">Not Found - If the user wasn't found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocFile), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var docFile = await _context.DocFiles.FindAsync(id);
            return docFile == null ? NotFound() : Ok(docFile);
        }

        // POST: api/DocFile
        /// <summary>
        /// Creates a record of documentation file of chemical substances in the database.
        /// </summary>
        /// <returns>A record of created documentation file of chemical substance</returns>
        /// <remarks>
        /// The endpoint 'POST api/DocFile' creates 
        /// a specific documentation file of chemical substance.
        /// If the request was successful, it'll return the 201 status code.
        /// If the documentation file payload was null, it'll return the 400 status code.
        /// </remarks>
        /// <response code = "201">Created - Returns the newly created record
        /// of documentation file of chemical substance
        /// </response>
        /// <response code = "400">Bad Request - If the documentation file payload was null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(DocFile docFile)
        {
            await _context.DocFiles.AddAsync(docFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = docFile.Id}, docFile);
        }

        // PUT: /api/DocFile/1
        /// <summary>
        /// Updates a record of documentation file of chemical substances in the database.
        /// </summary>
        /// <returns>
        /// An updated record of documentation file of chemical substance in the database
        /// </returns>
        /// <remarks>
        /// The endpoint 'PUT api/DocFile/{id}' updates 
        /// a specific documentation file of chemical substance with given id in the database.
        /// If the request was successful, it'll return the 204 status code.
        /// If the documentation file payload was null or wrong, it'll return the 400 status code.
        /// If the documentation file with given id wasn't found, it'll return the 404 status code.
        /// </remarks>
        /// <response code="204">No Content - If the request was successful</response>
        /// <response code="400">Bad Request - If the documentation file payload was null or wrong</response>
        /// <response code="404">Not Found - If the documentation file payload wasn't found with given id</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, DocFile docFile)
        {
            var fileDocToUpdate = await _context.DocFiles.FindAsync(id);
            if (fileDocToUpdate == null)
                return NotFound();
            else
            if (id != docFile.Id)
                return BadRequest();

            // EF Core is tracking of the duplicate of the entity 'fileDocToUpdate'
            // with the same primary key may cause an error so it must be detached
            // for successful updating of the entered payload
            _context.Entry(fileDocToUpdate).State = EntityState.Detached;
            _context.Entry(docFile).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /api/DocFile/1
        /// <summary>
        /// Deletes the record of documentation file of chemical substances with given Id in the database.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The endpoint 'DELETE api/DocFile/{id}' deletes
        /// a specific documentation file of chemical substance with given Id in the database.
        /// If the request was successful, it'll return the 204 status code.
        /// If the documentation file's Id wasn't found, it'll return the 404 status code.
        /// </remarks>
        /// <response code="204">No Content - If the request was successful
        /// </response>
        /// <response code="404">Not Found - If the documentation file's id wasn't found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var fileDocToDelete = await _context.DocFiles.FindAsync(id);
            if (fileDocToDelete == null)
                return NotFound();

            _context.DocFiles.Remove(fileDocToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}