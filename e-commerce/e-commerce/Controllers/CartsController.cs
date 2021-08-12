using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce.Context;
using e_commerce.Models;
using e_commerce.ViewModel;
using Microsoft.AspNetCore.Http;

namespace e_commerce.Controllers
{
    public class CartsController : Controller
    {
        private readonly ShoppingDbContext _context;

        public CartsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
           if(HttpContext.Session.GetString("username1")!=null)
            {

                var shoppingDbContext = _context.Cart.Include(c => c.Customer).Where(a=>a.Userid.Equals(Convert.ToInt32(HttpContext.Session.GetString("custId"))));
                var count = shoppingDbContext.ToList().Count();
                HttpContext.Session.SetString("CartCount", count.ToString());
                if (shoppingDbContext!= null && count>0)
                {
                    return View(await shoppingDbContext.ToListAsync());
                }
                else
                {
                    return RedirectToAction("CartEmpty");
                }
               
            }
            else
            {
                return RedirectToAction("Customer", "Login");
            }

        }

        public ActionResult CartEmpty()
        {
            return View();
        }
        public ActionResult GetElectronic(int id)
        {
            var productobj = _context.ElectronicDevice.FirstOrDefault(a => a.EId.Equals(id));
            HttpContext.Session.SetString("EId", productobj.EId.ToString());
            HttpContext.Session.SetString("Ename", productobj.EName.ToString());
            HttpContext.Session.SetString("ECategory", productobj.Category.ToString());
            HttpContext.Session.SetString("EPrice", productobj.Price.ToString());
            HttpContext.Session.SetString("EQuantity", productobj.Quantity.ToString());
            HttpContext.Session.SetString("ERating", productobj.Rating.ToString());
            HttpContext.Session.SetString("ELaunchDate", productobj.LaunchDate.ToString());
            HttpContext.Session.SetString("EImageFile", productobj.ImageFile.ToString());
            HttpContext.Session.SetString("EBrand", productobj.EBrand.ToString());
            HttpContext.Session.SetString("EDescription", productobj.Description.ToString());
            HttpContext.Session.SetString("EFreeDelivery", productobj.FreeDelivery.ToString());
            HttpContext.Session.SetString("EActive", productobj.Active.ToString());
            
            return RedirectToAction("EGetQuantity");

        }

        public ActionResult EGetQuantity()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EGetQuantity(ElectronicViewModel electronicViewModel)
        {
           
            Cart c = new Cart();
            c.Userid = Convert.ToInt32(HttpContext.Session.GetString("custId"));
            c.productName = HttpContext.Session.GetString("Ename");
            c.Price = Convert.ToInt32(HttpContext.Session.GetString("EPrice"));
            c.Quantity = electronicViewModel.Quantity;
            c.productImage = HttpContext.Session.GetString("EImageFile");
            c.Description = HttpContext.Session.GetString("EDescription");
            c.FreeDelivery = Convert.ToBoolean(HttpContext.Session.GetString("EFreeDelivery"));
            c.Category = HttpContext.Session.GetString("ECategory");
            c.Brand = HttpContext.Session.GetString("EBrand");
           
            _context.Cart.Add(c);
            _context.SaveChanges();
            TempData["ECart"] = "Item Added To Cart Successfully";
            return RedirectToAction("Index");
        }

        //Get Fashion Product
        public ActionResult GetFashion(int id)
        {
            var productobj1 = _context.Fashion.FirstOrDefault(a => a.FId.Equals(id));
            HttpContext.Session.SetString("FId", productobj1.FId.ToString());
            HttpContext.Session.SetString("Fname", productobj1.FName.ToString());
            HttpContext.Session.SetString("Ftype", productobj1.FType.ToString());
            HttpContext.Session.SetString("FSubCategory", productobj1.SubCategory.ToString());
            HttpContext.Session.SetString("FPrice", productobj1.Price.ToString());
            HttpContext.Session.SetString("FQuantity", productobj1.Quantity.ToString());
            HttpContext.Session.SetString("FLaunchDate", productobj1.LaunchDate.ToString());
            HttpContext.Session.SetString("FFreeDelivery", productobj1.FreeDelivery.ToString());
            HttpContext.Session.SetString("FRating", productobj1.Rating.ToString());
            HttpContext.Session.SetString("FImageFile", productobj1.ImageFile.ToString());
            HttpContext.Session.SetString("FActive", productobj1.Active.ToString());
            HttpContext.Session.SetString("FBrand", productobj1.FBrand.ToString());
            HttpContext.Session.SetString("FDescription", productobj1.Description.ToString());
            
           

            return RedirectToAction("FGetQuantity");

        }

        public ActionResult FGetQuantity()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FGetQuantity(FashionViewModel fashionViewModel)
        {

            Cart c = new Cart();
            c.Userid = Convert.ToInt32(HttpContext.Session.GetString("custId"));
            c.productName = HttpContext.Session.GetString("Fname");
            c.Price = Convert.ToInt32(HttpContext.Session.GetString("FPrice"));
            c.Quantity = fashionViewModel.Quantity;
            c.productImage = HttpContext.Session.GetString("FImageFile");
            c.Description = HttpContext.Session.GetString("FDescription");
            c.FreeDelivery = Convert.ToBoolean(HttpContext.Session.GetString("FFreeDelivery"));
            c.Category = HttpContext.Session.GetString("FSubCategory");
            c.Brand = HttpContext.Session.GetString("FBrand");
           
            _context.Cart.Add(c);
            _context.SaveChanges();
            TempData["FCart"] = "Item Added To Cart Successfully";
            return RedirectToAction("Index");
        }



        //Get Home Decor Product
        public ActionResult GetHomeDecor(int id)
        {
            var productobj = _context.HomeDecor.FirstOrDefault(a => a.HId.Equals(id));
            HttpContext.Session.SetString("HId", productobj.HId.ToString());
            HttpContext.Session.SetString("Hname", productobj.HName.ToString());
            HttpContext.Session.SetString("Htype", productobj.HType.ToString());          
            HttpContext.Session.SetString("HPrice", productobj.Price.ToString());
            HttpContext.Session.SetString("HQuantity", productobj.Quantity.ToString());
            HttpContext.Session.SetString("HRating", productobj.Rating.ToString());
            HttpContext.Session.SetString("HLaunchDate", productobj.LaunchDate.ToString());
            HttpContext.Session.SetString("HImageFile", productobj.ImageFile.ToString());
            HttpContext.Session.SetString("HBrand", productobj.HBrand.ToString());
            HttpContext.Session.SetString("HDescription", productobj.Description.ToString());
            HttpContext.Session.SetString("HFreeDelivery", productobj.FreeDelivery.ToString());
            HttpContext.Session.SetString("HActive", productobj.Active.ToString());

            return RedirectToAction("HGetQuantity");

        }

        public ActionResult HGetQuantity()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HGetQuantity(HomeDecorViewModel homeDecorViewModel)
        {

            Cart c = new Cart();
            c.Userid = Convert.ToInt32(HttpContext.Session.GetString("custId"));
            c.productName = HttpContext.Session.GetString("Hname");
            c.Price = Convert.ToInt32(HttpContext.Session.GetString("HPrice"));
            c.Quantity = homeDecorViewModel.Quantity;
            c.productImage = HttpContext.Session.GetString("HImageFile");
            c.Description = HttpContext.Session.GetString("HDescription");
            c.FreeDelivery = Convert.ToBoolean(HttpContext.Session.GetString("HFreeDelivery"));
            c.Category = HttpContext.Session.GetString("Htype");
            c.Brand = HttpContext.Session.GetString("HBrand");
           
            _context.Cart.Add(c);
            _context.SaveChanges();
            TempData["HCart"] = "Item Added To Cart Successfully";
            return RedirectToAction("Index");
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["Userid"] = new SelectList(_context.Customer, "UserId", "Email");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,Userid,productName,Price,Quantity,productImage,Description,FreeDelivery,Category,Brand")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.Customer, "UserId", "Email", cart.Userid);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.Customer, "UserId", "Email", cart.Userid);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,Userid,productName,Price,Quantity,productImage,Description,FreeDelivery,Category,Brand")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
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
            ViewData["Userid"] = new SelectList(_context.Customer, "UserId", "Email", cart.Userid);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            TempData["CDelete"] = "Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.CartId == id);
        }
    }
}
