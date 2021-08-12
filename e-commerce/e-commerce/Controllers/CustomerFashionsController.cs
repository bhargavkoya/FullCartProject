using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce.Context;
using e_commerce.Models;

namespace e_commerce.Controllers
{
    public class CustomerFashionsController : Controller
    {
        private readonly ShoppingDbContext _context;

        public CustomerFashionsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: CustomerFashions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fashion.ToListAsync());
        }

        public IActionResult FList()
        {
            return View(_context.Fashion.ToList());
        }

        public IActionResult Watch()
        {
            return View(_context.Fashion.ToList().Where(a => a.SubCategory.Equals(Scategory.Watch)));
        }

        // GET: CustomerFashions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fashion = await _context.Fashion
                .FirstOrDefaultAsync(m => m.FId == id);
            if (fashion == null)
            {
                return NotFound();
            }

            return View(fashion);
        }

        // GET: CustomerFashions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerFashions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FId,FName,FType,SubCategory,Price,Quantity,LaunchDate,FreeDelivery,Rating,ImageFile,Active,FBrand,Description")] Fashion fashion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fashion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fashion);
        }

        // GET: CustomerFashions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fashion = await _context.Fashion.FindAsync(id);
            if (fashion == null)
            {
                return NotFound();
            }
            return View(fashion);
        }

        // POST: CustomerFashions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FId,FName,FType,SubCategory,Price,Quantity,LaunchDate,FreeDelivery,Rating,ImageFile,Active,FBrand,Description")] Fashion fashion)
        {
            if (id != fashion.FId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fashion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FashionExists(fashion.FId))
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
            return View(fashion);
        }

        // GET: CustomerFashions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fashion = await _context.Fashion
                .FirstOrDefaultAsync(m => m.FId == id);
            if (fashion == null)
            {
                return NotFound();
            }

            return View(fashion);
        }

        // POST: CustomerFashions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fashion = await _context.Fashion.FindAsync(id);
            _context.Fashion.Remove(fashion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FashionExists(int id)
        {
            return _context.Fashion.Any(e => e.FId == id);
        }
    }
}
