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
    public class CustomerHomeDecorsController : Controller
    {
        private readonly ShoppingDbContext _context;

        public CustomerHomeDecorsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: CustomerHomeDecors
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeDecor.ToListAsync());
        }

        public IActionResult HList()
        {
            return View(_context.HomeDecor.ToList());
        }

        public IActionResult Furniture()
        {
            return View(_context.HomeDecor.ToList().Where(a => a.HType.Equals(HType.Furniture)));
        }

        // GET: CustomerHomeDecors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeDecor = await _context.HomeDecor
                .FirstOrDefaultAsync(m => m.HId == id);
            if (homeDecor == null)
            {
                return NotFound();
            }

            return View(homeDecor);
        }

        // GET: CustomerHomeDecors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerHomeDecors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HId,HName,HType,Price,Quantity,Active,Description,HBrand,ImageFile,FreeDelivery,LaunchDate,Rating")] HomeDecor homeDecor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeDecor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeDecor);
        }

        // GET: CustomerHomeDecors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeDecor = await _context.HomeDecor.FindAsync(id);
            if (homeDecor == null)
            {
                return NotFound();
            }
            return View(homeDecor);
        }

        // POST: CustomerHomeDecors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HId,HName,HType,Price,Quantity,Active,Description,HBrand,ImageFile,FreeDelivery,LaunchDate,Rating")] HomeDecor homeDecor)
        {
            if (id != homeDecor.HId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeDecor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeDecorExists(homeDecor.HId))
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
            return View(homeDecor);
        }

        // GET: CustomerHomeDecors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeDecor = await _context.HomeDecor
                .FirstOrDefaultAsync(m => m.HId == id);
            if (homeDecor == null)
            {
                return NotFound();
            }

            return View(homeDecor);
        }

        // POST: CustomerHomeDecors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeDecor = await _context.HomeDecor.FindAsync(id);
            _context.HomeDecor.Remove(homeDecor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeDecorExists(int id)
        {
            return _context.HomeDecor.Any(e => e.HId == id);
        }
    }
}
