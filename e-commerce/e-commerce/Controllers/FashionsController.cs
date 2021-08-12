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
    public class FashionsController : Controller
    {
        private readonly ShoppingDbContext _context;
        HttpClient client = new HttpClient();
        string url = "https://localhost:44366/api/Fashions/";

        public FashionsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: Fashions
        public async Task<IActionResult> Index()
        {
            // return View(await _context.Fashion.ToListAsync());
            return View(JsonConvert.DeserializeObject<List<Fashion>>(await client.GetStringAsync(url)).ToList());
        }

        // GET: Fashions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var fashion = await _context.Fashion
            // .FirstOrDefaultAsync(m => m.FId == id);
            var fashion = JsonConvert.DeserializeObject<Fashion>(await client.GetStringAsync(url + id));
            if (fashion == null)
            {
                return NotFound();
            }

            return View(fashion);
        }

        // GET: Fashions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fashions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FId,FName,FType,SubCategory,Price,Quantity,LaunchDate,FreeDelivery,Rating,ImageFile,Active,FBrand,Description")] Fashion fashion)
        {
            if (ModelState.IsValid)
            {
                TempData["FCreate"] = "Successfully Created";
                // _context.Add(fashion);
                // await _context.SaveChangesAsync();
                await client.PostAsJsonAsync<Fashion>(url, fashion);
                return RedirectToAction("AdminView", "Login");
            }
            else
            {
                ViewBag.FCreate = "Create Failed.";
                return View(fashion);
            }
        }

            // GET: Fashions/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var fashion = await _context.Fashion.FindAsync(id);
            var fashion = JsonConvert.DeserializeObject<Fashion>(await client.GetStringAsync(url + id));

            if (fashion == null)
            {
                return NotFound();
            }
            return View(fashion);
        }

        // POST: Fashions/Edit/5
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
                    //_context.Update(fashion);
                    //await _context.SaveChangesAsync();
                    await client.PutAsJsonAsync<Fashion>(url + id, fashion);
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
                TempData["FEdit"] = "Edited Successfully.";
                return RedirectToAction("AdminView","Login");
            }
            ViewBag.FEdit = "Edit Failed.";
            return View(fashion);
        }

        // GET: Fashions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var fashion = await _context.Fashion
            // .FirstOrDefaultAsync(m => m.FId == id);
            var fashion = JsonConvert.DeserializeObject<Fashion>(await client.GetStringAsync(url + id));
            if (fashion == null)
            {
                ViewBag.FDelete = "Product Not Found";
                return NotFound();
            }

            return View(fashion);
        }

        // POST: Fashions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fashion = await _context.Fashion.FindAsync(id);
            //_context.Fashion.Remove(fashion);
            // await _context.SaveChangesAsync();
            await client.DeleteAsync(url + id);
            TempData["FDelete"] = "Deleted Successfully";
            return RedirectToAction("AdminView", "Login");
        }

        private bool FashionExists(int id)
        {
            return _context.Fashion.Any(e => e.FId == id);
        }
    }
}
