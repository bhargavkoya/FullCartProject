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
    public class HomeDecorsController : ControllerBase
    {
        private readonly ShoppingDbContext _context;

        public HomeDecorsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: api/HomeDecors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeDecor>>> GetHomeDecor()
        {
            return await _context.HomeDecor.ToListAsync();
        }

        // GET: api/HomeDecors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeDecor>> GetHomeDecor(int id)
        {
            var homeDecor = await _context.HomeDecor.FindAsync(id);

            if (homeDecor == null)
            {
                return NotFound();
            }

            return homeDecor;
        }

        // PUT: api/HomeDecors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomeDecor(int id, HomeDecor homeDecor)
        {
            if (id != homeDecor.HId)
            {
                return BadRequest();
            }

            _context.Entry(homeDecor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeDecorExists(id))
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

        // POST: api/HomeDecors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HomeDecor>> PostHomeDecor(HomeDecor homeDecor)
        {
            _context.HomeDecor.Add(homeDecor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHomeDecor", new { id = homeDecor.HId }, homeDecor);
        }

        // DELETE: api/HomeDecors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomeDecor(int id)
        {
            var homeDecor = await _context.HomeDecor.FindAsync(id);
            if (homeDecor == null)
            {
                return NotFound();
            }

            _context.HomeDecor.Remove(homeDecor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HomeDecorExists(int id)
        {
            return _context.HomeDecor.Any(e => e.HId == id);
        }
    }
}
