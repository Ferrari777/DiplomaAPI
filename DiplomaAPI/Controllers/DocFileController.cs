using AutoMapper;
using DiplomaAPI.Data;
using DiplomaAPI.DTOs;
using DiplomaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocFileController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webEnvironment;

        public DocFileController(UserDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _webEnvironment = environment;
        }

        // GET: api/DocFile
        // Returns list of all documentary files
        [HttpGet]
        public async Task<IEnumerable<DocFileDto>> Get()
        {
            var docFileList = await _context.DocFiles
                .Include(p => p.Agreed)
                .Include(p => p.Approved)
                .ToListAsync();
            var docFileListDto = _mapper.Map<IEnumerable<DocFileDto>>(docFileList);

            return docFileListDto;
        }

        // GET: api/DocFile/1
        // Returns specific documentary file with given Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocFile), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var docFile = await _context.DocFiles
                          .Include(p => p.Agreed)
                          .Include(p => p.Approved)
                          .Where(p => p.Id == id)
                          .FirstOrDefaultAsync();
            if (docFile == null)
            {
                NotFound();
            }

            var docFileDto = _mapper.Map<DocFileDto>(docFile);
           
            return Ok(docFileDto);
        }

        // POST: api/DocFile
        // Creates a record of documentary file in the database
        [HttpPost]
        [ProducesResponseType(typeof(DocFile), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(DocFileDto docFileDtoPayload)
        {
            var newDocFile = _mapper.Map<DocFile>(docFileDtoPayload);
            
            await _context.DocFiles.AddAsync(newDocFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newDocFile.Id}, newDocFile);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([Required] int userId, [Required] IFormFile file)
        {
            if (file == null)
                return BadRequest("No file is uploaded.");

            long size = file.Length;
            //int fileID;
            var rootPath = Path.Combine(_webEnvironment.ContentRootPath, "Resources", "Documents");

            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            var filePath = Path.Combine(rootPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var docFile = new DocFile
                {
                    Name = file.FileName,
                    ContentType = file.ContentType,
                    FileSize = file.Length,
                    UserId = userId
                };

                await file.CopyToAsync(stream);

                //fileID = docFile.Id;
                await _context.DocFiles.AddAsync(docFile);
                await _context.SaveChangesAsync();
            }

            return Ok(new { file.FileName, size });
        }

        // PUT: /api/DocFile
        // Updates the record of documentary file with the given Id in the database
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, DocFileDto docFileDtoPayload)
        {
            var updatedDocFile = _mapper.Map<DocFile>(docFileDtoPayload);
            
            if (id != updatedDocFile.Id)
                return BadRequest();

            _context.Entry(updatedDocFile).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /api/DocFile/1
        // Deletes the record of specific documentary file with given Id
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var fileDocToDelete = await _context.DocFiles
                                  .Where(p => p.Id == id)
                                  .FirstOrDefaultAsync();
            if (fileDocToDelete == null)
                return NotFound();

            _context.DocFiles.Remove(fileDocToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
