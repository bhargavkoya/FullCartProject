using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce.Context;
using e_commerce.Models;
using Microsoft.AspNetCore.Http;
using e_commerce.ViewModel;

namespace e_commerce.Controllers
{
    public class OrderReviewsController : Controller
    {
        private readonly ShoppingDbContext _context;

        public OrderReviewsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: OrderReviews
        public async Task<IActionResult> Index()
        {
            var shoppingDbContext = _context.OrderReview.Include(o => o.Customer);
            return View(await shoppingDbContext.ToListAsync());
        }

        public ActionResult GetOrderId(int id)
        {
            HttpContext.Session.SetString("orderid", id.ToString());
            return RedirectToAction("Review");
        }

        public ActionResult Review()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Review(OrderReviewViewModel orderReviewViewModel)
        {
            var orderReview = new OrderReview();
            orderReview.UserID = Convert.ToInt32(HttpContext.Session.GetString("custId"));
            orderReview.Date = DateTime.Now;
            orderReview.O_Rating = orderReviewViewModel.O_Rating;
            orderReview.Status = orderReviewViewModel.Status;
            orderReview.Description = orderReviewViewModel.Description;
            orderReview.OId = Convert.ToInt32(HttpContext.Session.GetString("orderid"));
            _context.OrderReview.Add(orderReview);
            var orderobj = _context.Order.FirstOrDefault(a=>a.OrderId.Equals(orderReview.OId));
            orderobj.OrderStatus = OrderStatus.Finished;
            _context.Order.Update(orderobj);
            _context.SaveChanges();

            return RedirectToAction("CustomerView", "Login");
        }
        // GET: OrderReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderReview = await _context.OrderReview
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderReview == null)
            {
                return NotFound();
            }

            return View(orderReview);
        }

        // GET: OrderReviews/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Customer, "UserId", "Email");
            return View();
        }

        // POST: OrderReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Date,O_Rating,Status,Description,OId")] OrderReview orderReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Customer, "UserId", "Email", orderReview.UserID);
            return View(orderReview);
        }

        // GET: OrderReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderReview = await _context.OrderReview.FindAsync(id);
            if (orderReview == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Customer, "UserId", "Email", orderReview.UserID);
            return View(orderReview);
        }

        // POST: OrderReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Date,O_Rating,Status,Description,OId")] OrderReview orderReview)
        {
            if (id != orderReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderReviewExists(orderReview.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Customer, "UserId", "Email", orderReview.UserID);
            return View(orderReview);
        }

        // GET: OrderReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderReview = await _context.OrderReview
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderReview == null)
            {
                return NotFound();
            }

            return View(orderReview);
        }

        // POST: OrderReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderReview = await _context.OrderReview.FindAsync(id);
            _context.OrderReview.Remove(orderReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderReviewExists(int id)
        {
            return _context.OrderReview.Any(e => e.Id == id);
        }
    }
}
