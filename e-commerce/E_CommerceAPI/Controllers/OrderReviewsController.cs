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
    public class OrderReviewsController : ControllerBase
    {
        private readonly ShoppingDbContext _context;

        public OrderReviewsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReview>>> GetOrderReview()
        {
            return await _context.OrderReview.ToListAsync();
        }

        // GET: api/OrderReviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReview>> GetOrderReview(int id)
        {
            var orderReview = await _context.OrderReview.FindAsync(id);

            if (orderReview == null)
            {
                return NotFound();
            }

            return orderReview;
        }

        // PUT: api/OrderReviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderReview(int id, OrderReview orderReview)
        {
            if (id != orderReview.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderReviewExists(id))
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

        // POST: api/OrderReviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderReview>> PostOrderReview(OrderReview orderReview)
        {
            _context.OrderReview.Add(orderReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderReview", new { id = orderReview.Id }, orderReview);
        }

        // DELETE: api/OrderReviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderReview(int id)
        {
            var orderReview = await _context.OrderReview.FindAsync(id);
            if (orderReview == null)
            {
                return NotFound();
            }

            _context.OrderReview.Remove(orderReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderReviewExists(int id)
        {
            return _context.OrderReview.Any(e => e.Id == id);
        }
    }
}
