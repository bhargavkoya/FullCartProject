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
    public class ElectronicDevicesController : ControllerBase
    {
        private readonly ShoppingDbContext _context;

        public ElectronicDevicesController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: api/ElectronicDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectronicDevice>>> GetElectronicDevice()
        {
            return await _context.ElectronicDevice.ToListAsync();
        }

        // GET: api/ElectronicDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectronicDevice>> GetElectronicDevice(int id)
        {
            var electronicDevice = await _context.ElectronicDevice.FindAsync(id);

            if (electronicDevice == null)
            {
                return NotFound();
            }

            return electronicDevice;
        }

        // PUT: api/ElectronicDevices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectronicDevice(int id, ElectronicDevice electronicDevice)
        {
            if (id != electronicDevice.EId)
            {
                return BadRequest();
            }

            _context.Entry(electronicDevice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectronicDeviceExists(id))
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

        // POST: api/ElectronicDevices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ElectronicDevice>> PostElectronicDevice(ElectronicDevice electronicDevice)
        {
            _context.ElectronicDevice.Add(electronicDevice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElectronicDevice", new { id = electronicDevice.EId }, electronicDevice);
        }

        // DELETE: api/ElectronicDevices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectronicDevice(int id)
        {
            var electronicDevice = await _context.ElectronicDevice.FindAsync(id);
            if (electronicDevice == null)
            {
                return NotFound();
            }

            _context.ElectronicDevice.Remove(electronicDevice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElectronicDeviceExists(int id)
        {
            return _context.ElectronicDevice.Any(e => e.EId == id);
        }
    }
}
