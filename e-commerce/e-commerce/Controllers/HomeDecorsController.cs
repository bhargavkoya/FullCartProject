using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce.Context;
using e_commerce.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace e_commerce.Controllers
{
    public class HomeDecorsController : Controller
    {
        private readonly ShoppingDbContext _context;
        HttpClient client = new HttpClient();
        string url = "https://localhost:44366/api/HomeDecors/";

        public HomeDecorsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: HomeDecors
        public async Task<IActionResult> Index()
        {
            //return View(await _context.HomeDecor.ToListAsync());
            return View(JsonConvert.DeserializeObject<List<HomeDecor>>(await client.GetStringAsync(url)).ToList());
        }

        // GET: HomeDecors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var homeDecor = await _context.HomeDecor
            // .FirstOrDefaultAsync(m => m.HId == id);
            var homeDecor = JsonConvert.DeserializeObject<HomeDecor>(await client.GetStringAsync(url + id));
            if (homeDecor == null)
            {
                return NotFound();
            }

            return View(homeDecor);
        }

        // GET: HomeDecors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeDecors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HId,HName,Type,Price,Quantity,Active,Description,HBrand,ImageFile,FreeDelivery,LaunchDate,Rating")] HomeDecor homeDecor)
        {
            if (ModelState.IsValid)
            {
                TempData["HCreate"] = "Successfully Created";
                // _context.Add(homeDecor);
                // await _context.SaveChangesAsync();
                await client.PostAsJsonAsync<HomeDecor>(url, homeDecor);
                return RedirectToAction("AdminView", "Login");
            }
            else
            {
                ViewBag.HCreate = "Create Failed.";
                return View(homeDecor);
            }
        }

        // GET: HomeDecors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var homeDecor = await _context.HomeDecor.FindAsync(id);
            var homeDecor = JsonConvert.DeserializeObject<HomeDecor>(await client.GetStringAsync(url + id));

            if (homeDecor == null)
            {
                return NotFound();
            }
            return View(homeDecor);
        }

        // POST: HomeDecors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HId,HName,Type,Price,Quantity,Active,Description,HBrand,ImageFile,FreeDelivery,LaunchDate,Rating")] HomeDecor homeDecor)
        {
            if (id != homeDecor.HId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // _context.Update(homeDecor);
                    // await _context.SaveChangesAsync();
                    await client.PutAsJsonAsync<HomeDecor>(url + id, homeDecor);
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
                TempData["HEdit"] = "Edited Successfully.";
                return RedirectToAction("AdminView", "Login");
            }
            ViewBag.HEdit = "Edit Failed.";
            return View(homeDecor);
        }

        // GET: HomeDecors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var homeDecor = await _context.HomeDecor
            //  .FirstOrDefaultAsync(m => m.HId == id);
            var homeDecor = JsonConvert.DeserializeObject<HomeDecor>(await client.GetStringAsync(url + id));
            if (homeDecor == null)
            {
                ViewBag.HDelete = "Product Not Found";
                return NotFound();
            }

            return View(homeDecor);
        }

        // POST: HomeDecors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeDecor = await _context.HomeDecor.FindAsync(id);
            //  _context.HomeDecor.Remove(homeDecor);
            // await _context.SaveChangesAsync();
            await client.DeleteAsync(url + id);
            TempData["HDelete"] = "Deleted Successfully";
            return RedirectToAction("AdminView", "Login");
        }

        private bool HomeDecorExists(int id)
        {
            return _context.HomeDecor.Any(e => e.HId == id);
        }
    }
}
