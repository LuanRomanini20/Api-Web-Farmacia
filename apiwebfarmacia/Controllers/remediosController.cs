using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiwebfarmacia.Data;
using apiwebfarmacia.Models;

namespace apiwebfarmacia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class remediosController : ControllerBase
    {
        private readonly apiwebfarmaciaContext _context;

        public remediosController(apiwebfarmaciaContext context)
        {
            _context = context;
        }

        // GET: api/remedios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<remedio>>> Getremedio()
        {
          if (_context.remedio == null)
          {
              return NotFound();
          }
            return await _context.remedio.ToListAsync();
        }

        // GET: api/remedios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<remedio>> Getremedio(int id)
        {
          if (_context.remedio == null)
          {
              return NotFound();
          }
            var remedio = await _context.remedio.FindAsync(id);

            if (remedio == null)
            {
                return NotFound();
            }

            return remedio;
        }

        // PUT: api/remedios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putremedio(int id, remedio remedio)
        {
            if (id != remedio.id)
            {
                return BadRequest();
            }

            _context.Entry(remedio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!remedioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/remedios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<remedio>> Postremedio(remedio remedio)
        {
          if (_context.remedio == null)
          {
              return Problem("Entity set 'apiwebfarmaciaContext.remedio'  is null.");
          }
            _context.remedio.Add(remedio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getremedio", new { id = remedio.id }, remedio);
        }
        // DELETE: api/remedios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<remedio>> Deleteremedio(int id)
        {
            if (_context.remedio == null)
            {
                return NotFound();
            }
            var remedio = await _context.remedio.FindAsync(id);
            if (remedio == null)
            {
                return NotFound();
            }

            _context.remedio.Remove(remedio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool remedioExists(int id)
        {
            return (_context.remedio?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
