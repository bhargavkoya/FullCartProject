using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce.Context;
using e_commerce.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace e_commerce.Controllers
{
    public class ElectronicDevicesController : Controller
    {
        private readonly ShoppingDbContext _context;
        HttpClient client = new HttpClient();
        string url = "https://localhost:44366/api/ElectronicDevices/";

        public ElectronicDevicesController(ShoppingDbContext context)
        {
            _context = context;
           
        }

        // GET: ElectronicDevices
        public async Task<IActionResult> Index()
        {

            // return View(await _context.ElectronicDevice.ToListAsync());
            return View(JsonConvert.DeserializeObject<List<ElectronicDevice>>(await client.GetStringAsync(url)).ToList());
        }
        public async Task<IActionResult> ElectronicPartial()
        {
           // var electronicList = await _context.ElectronicDevice.ToListAsync();
            return View(await _context.ElectronicDevice.ToListAsync());
        }
        // GET: ElectronicDevices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var electronicDevice = await _context.ElectronicDevice
            //   .FirstOrDefaultAsync(m => m.EId == id);
            var electronicDevice = JsonConvert.DeserializeObject<ElectronicDevice>(await client.GetStringAsync(url + id));
            if (electronicDevice == null)
            {
                return NotFound();
            }

            return View(electronicDevice);
        }

        // GET: ElectronicDevices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElectronicDevices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EId,EName,Category,Price,Quantity,Rating,LaunchDate,ImageFile, EBrand,Description,FreeDelivery,Active")] ElectronicDevice electronicDevice)
        {

            if (ModelState.IsValid)
            {
                TempData["ECreate"] = "Successfully Created";
                // _context.Add(electronicDevice);
                // await _context.SaveChangesAsync();
                await client.PostAsJsonAsync<ElectronicDevice>(url, electronicDevice);
                return RedirectToAction("AdminView", "Login");
            }
            else
            {
                ViewBag.ECreate = "Create Failed.";
                return View(electronicDevice);
            }

           
        }

        // GET: ElectronicDevices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var electronicDevice = await _context.ElectronicDevice.FindAsync(id);
            var electronicDevice = JsonConvert.DeserializeObject<ElectronicDevice>(await client.GetStringAsync(url + id));

            if (electronicDevice == null)
            {
                return NotFound();
            }
            return View(electronicDevice);
        }

        // POST: ElectronicDevices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EId,EName,Category,Price,Quantity,Rating,LaunchDate,ImageFile,EBrand,Description,FreeDelivery,Active")] ElectronicDevice electronicDevice)
        {
            if (id != electronicDevice.EId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(electronicDevice);
                    await client.PutAsJsonAsync<ElectronicDevice>(url + id, electronicDevice);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectronicDeviceExists(electronicDevice.EId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["EEdit"] = "Edited Successfully.";
                return RedirectToAction("AdminView","Login");
            }
            ViewBag.EEdit = "Edit Failed.";
            return View(electronicDevice);
        }

        // GET: ElectronicDevices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var electronicDevice = await _context.ElectronicDevice
            //.FirstOrDefaultAsync(m => m.EId == id);
            var electronicDevice = JsonConvert.DeserializeObject<ElectronicDevice>(await client.GetStringAsync(url + id));
            if (electronicDevice == null)
            {
                ViewBag.EDelete = "Product Not Found";
                return NotFound();
            }

            return View(electronicDevice);
        }

        // POST: ElectronicDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electronicDevice = await _context.ElectronicDevice.FindAsync(id);
            // _context.ElectronicDevice.Remove(electronicDevice);
            // await _context.SaveChangesAsync();
            await client.DeleteAsync(url + id);
            TempData["EDelete"] = "Deleted Successfully";
            return RedirectToAction("AdminView", "Login");
        }

        private bool ElectronicDeviceExists(int id)
        {
            return _context.ElectronicDevice.Any(e => e.EId == id);
        }
    }
}
