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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using e_commerce.ViewModel;

namespace e_commerce.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShoppingDbContext _context;

        public OrdersController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username1") != null)
            {

                var addressobj = _context.Address.FirstOrDefault(a => a.CustId.Equals(Convert.ToInt32(HttpContext.Session.GetString("custId"))));
                var shoppingDbContext = _context.Order.Include(o => o.Address).Where(a => a.AddrId.Equals(addressobj.AddressId));
                var count = shoppingDbContext.ToList().Count();
                if (shoppingDbContext != null && count > 0)
                {
                    return View(await shoppingDbContext.ToListAsync());
                }
                else
                {
                    return RedirectToAction("OrderEmpty");
                }

            }
            else
            {
                return RedirectToAction("Customer", "Login");
            }
           
           
        }
        [HttpPost]
        public ActionResult GetDate(DateTime SD, DateTime ED)
        {
            var addressobj = _context.Address.FirstOrDefault(a => a.CustId.Equals(Convert.ToInt32(HttpContext.Session.GetString("custId"))));
            var shoppingDbContext = _context.Order.Include(o => o.Address).Where(a => a.AddrId.Equals(addressobj.AddressId)).Where(a=>a.DateOfOrder >= SD && a.DateOfOrder <= ED);
            return View("Index", shoppingDbContext.ToList());
        }

       /* [HttpPost] 
        public ActionResult GetDate(DateTime date1, DateTime date2)
        {
           // HttpContext.Session.SetString("fromDate", date1.ToString());
           // HttpContext.Session.SetString("endDate", date2.ToString());
            return RedirectToAction("Index");
        }*/

        
        public ActionResult OrderEmpty()
        {
            return View();

        }
        public ActionResult PaymentProcess()
        {
            var cartobj = _context.Cart.Include(c => c.Customer).Where(a => a.Userid.Equals(Convert.ToInt32(HttpContext.Session.GetString("custId"))));
            ViewBag.Amount = cartobj.ToList().Sum(a => a.Price * a.Quantity);
            ViewBag.CartList = cartobj;
            return View();
        }
        [HttpPost]
       
        public ActionResult PaymentProcess([Bind("CardNumber,Password")] Payment payment)
        {
            var paymentobj = _context.Payment.FirstOrDefault(a => a.CardHolderName.ToLower().Equals(HttpContext.Session.GetString("username1")));
            
            var cartobj = _context.Cart.Include(c => c.Customer).Where(a => a.Userid.Equals(Convert.ToInt32(HttpContext.Session.GetString("custId"))));
            ViewBag.CartList = cartobj;
            ViewBag.Amount1 = cartobj.ToList().Sum(a => a.Price * a.Quantity);
            ViewBag.Amount2 = cartobj.ToList().Sum(a => (Convert.ToInt32(a.Price * a.Quantity)-(Convert.ToInt32(a.Price * a.Quantity*0.02))));

           
                if (paymentobj.CardNumber == payment.CardNumber && paymentobj.Password == payment.Password)
                {
                    if (paymentobj.Balance >= ViewBag.Amount1)
                    {

                        foreach (var item in cartobj)
                        {
                            if (item.Category == "Laptop" || item.Category == "Mobile" || item.Category == "EarPhone" || item.Category == "Camera" || item.Category == "Television" || item.Category == "Printers")
                            {
                                var productobj = _context.ElectronicDevice.FirstOrDefault(a => a.EName.Equals(item.productName));
                                productobj.Quantity = productobj.Quantity - item.Quantity;
                                _context.ElectronicDevice.Update(productobj);

                            }
                            else if (item.Category == "Watch" || item.Category == "Wallet" || item.Category == "Sunglasses")
                            {
                                var productobj1 = _context.Fashion.FirstOrDefault(a => a.FName.Equals(item.productName));
                                productobj1.Quantity = productobj1.Quantity - item.Quantity;
                                _context.Fashion.Update(productobj1);

                            }
                            else if (item.Category == "Furniture" || item.Category == "SecurityCameras" || item.Category == "SmartHomelightening" || item.Category == "Clocks" || item.Category == "Mirrors" || item.Category == "Wallpapers" || item.Category == "DreamCatcher")
                            {
                                var productobj2 = _context.HomeDecor.FirstOrDefault(a => a.HName.Equals(item.productName));
                                productobj2.Quantity = productobj2.Quantity - item.Quantity;
                                _context.HomeDecor.Update(productobj2);

                            }

                        }
                        paymentobj.Balance = paymentobj.Balance - ViewBag.Amount2;
                        _context.Payment.Update(paymentobj);

                        var orderobj = new Order();
                        orderobj.Price = ViewBag.Amount1;
                        orderobj.AddrId = _context.Address.Find(Convert.ToInt32(HttpContext.Session.GetString("custId"))).AddressId;
                        orderobj.DateOfOrder = DateTime.Now;
                        orderobj.PaymentMode = paymentmode.creditCard;
                        orderobj.OrderStatus = OrderStatus.Progress;
                        orderobj.DateOfDelivery = DateTime.Now.AddDays(1);
                        _context.Order.Add(orderobj);
                        _context.SaveChanges();
                        TempData["PSucess"] = "Payment Was Successfull and Order is Placed";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.InsufficientAmount = "Insufficient amount";
                        ModelState.AddModelError("", "Insufficient amount");
                    }


                }
                else
                {
                    ViewBag.InvalidCredentials = "Invalid Credentials";
                    ModelState.AddModelError("", "Invalid Credentials");

                }
           
           
            return View(payment);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Address)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["AddrId"] = new SelectList(_context.Address, "AddressId", "AddressLineone");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Price,AddrId,DateOfOrder,PaymentMode,OrderStatus,DateOfDelivery")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddrId"] = new SelectList(_context.Address, "AddressId", "AddressLineone", order.AddrId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["AddrId"] = new SelectList(_context.Address, "AddressId", "AddressLineone", order.AddrId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Price,AddrId,DateOfOrder,PaymentMode,OrderStatus,DateOfDelivery")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["AddrId"] = new SelectList(_context.Address, "AddressId", "AddressLineone", order.AddrId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Address)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
