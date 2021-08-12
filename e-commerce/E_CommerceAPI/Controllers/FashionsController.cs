using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_CommerceAPI.Context;
using E_CommerceAPI.Models;

namespace E_CommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FashionsController : ControllerBase
    {
        private readonly ShoppingDbContext _context;

        public FashionsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: api/Fashions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fashion>>> GetFashion()
        {
            return await _context.Fashion.ToListAsync();
        }

        // GET: api/Fashions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fashion>> GetFashion(int id)
        {
            var fashion = await _context.Fashion.FindAsync(id);

            if (fashion == null)
            {
                return NotFound();
            }

            return fashion;
        }

        // PUT: api/Fashions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFashion(int id, Fashion fashion)
        {
            if (id != fashion.FId)
            {
                return BadRequest();
            }

            _context.Entry(fashion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FashionExists(id))
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

        // POST: api/Fashions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fashion>> PostFashion(Fashion fashion)
        {
            _context.Fashion.Add(fashion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFashion", new { id = fashion.FId }, fashion);
        }

        // DELETE: api/Fashions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFashion(int id)
        {
            var fashion = await _context.Fashion.FindAsync(id);
            if (fashion == null)
            {
                return NotFound();
            }

            _context.Fashion.Remove(fashion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FashionExists(int id)
        {
            return _context.Fashion.Any(e => e.FId == id);
        }
    }
}
