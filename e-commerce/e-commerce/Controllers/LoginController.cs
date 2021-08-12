using DNTCaptcha.Core;
using e_commerce.Context;
using e_commerce.Models;
using e_commerce.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class LoginController : Controller
    {
        private readonly ShoppingDbContext _context;
       
        public LoginController(ShoppingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            if (id == 1)
                return RedirectToAction("Admin");
            else
                return RedirectToAction("CustomerRegistration");
        }

        public IActionResult AdminRegistration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminRegistration(Admin obj)
        {
            if (ModelState.IsValid)
            {

                _context.Admin.Add(obj);
                _context.SaveChanges();
                ModelState.Clear();

                ViewBag.Message = obj.FirstName + " " + obj.LastName + "Successfully Registered.";
            }
            return RedirectToAction("Admin");
        }

        public IActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Admin(Admin obj)
        {
             var obj1 = _context.Admin.Where(a => a.UserName.Equals(obj.UserName) && a.Password.Equals(obj.Password)).FirstOrDefault();
          
            //var obj1 = JsonConvert.DeserializeObject<Admin>(await client.GetStringAsync(url + obj.UserId));
            if (obj1 != null)
            {
                HttpContext.Session.SetString("userid", obj1.UserId.ToString());
                HttpContext.Session.SetString("username", obj1.UserName.ToString());
                TempData["AMessage"] = "Successfully Login";
                return this.RedirectToAction("AdminView");

            }
            else
            {
                ViewBag.AMessage = "Login Failed. Wrong Username or Password !!!";
                ModelState.AddModelError("", "UserName or Password is wrong");
            }

            return View();


        }
        public ActionResult AdminLogOff()
        {
            HttpContext.Session.Clear();
            TempData["ALogOff"] = "Log Off done Successfully";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AMyAccount()
        {

            ViewBag.AdminAccountList = _context.Admin.ToList().Where(a => a.UserId.Equals(Convert.ToInt32(HttpContext.Session.GetString("userid"))));
            return View();
        }
        public ActionResult CMyAccount()
        {

            ViewBag.Account1 = _context.Customer.ToList().Where(a => a.UserId.Equals(Convert.ToInt32(HttpContext.Session.GetString("custId"))));
            return View();
        }


        public ActionResult AdminView()
        {
            ViewBag.ReviewList = _context.OrderReview.ToList();
            ViewBag.EleList = _context.ElectronicDevice.ToList();
            ViewBag.HomeList = _context.HomeDecor.ToList();
            ViewBag.FashionList = _context.Fashion.ToList();
            return View();
        }
       
        public ActionResult CustomerRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerRegistration(Customer obj)
        {
            if (ModelState.IsValid)
            {

                _context.Customer.Add(obj);
                _context.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = obj.FirstName + " " + obj.LastName + "Successfully Registered.";
            }
            return RedirectToAction("Customer");
        }

        public ActionResult Customer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Customer(Customer obj)
        {
            var obj1 = _context.Customer.Where(a => a.UserName.Equals(obj.UserName) && a.Password.Equals(obj.Password)).FirstOrDefault();
           
            if (obj1 != null)
            {
               
                HttpContext.Session.SetString("custId", obj1.UserId.ToString());
                HttpContext.Session.SetString("username1", obj1.UserName.ToString());
                TempData["CLogin"] = "Successfully Login";
                return RedirectToAction("CustomerView");
            }
            else
            {
                ViewBag.CLogin = "Login Failed. Wrong Username or Password !!!";
                ModelState.AddModelError("Error", "UserName or Password is wrong");
                
            }

            return View();


        }

        


        public ActionResult CustomerView()
        {
            
            ViewBag.EleList1 = _context.ElectronicDevice.ToList();
            ViewBag.HomeList1 = _context.HomeDecor.ToList();
            ViewBag.FashionList1 = _context.Fashion.ToList();
            return View();
        }
       
    }

}
