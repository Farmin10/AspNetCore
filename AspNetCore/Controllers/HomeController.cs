using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Entities;
using AspNetCore.Interfaces;
using AspNetCore.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<AppUser> _signInManager;

        private IProductRepository _productRepository;

        private ICartRepository _cartRepository;

        public HomeController(IProductRepository productRepository, SignInManager<AppUser> signInManager, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _signInManager = signInManager;
            _cartRepository = cartRepository;
        }




        public IActionResult Index(int? categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }



        public IActionResult Detail(int id)
        {
            return View(_productRepository.GetById(id));
        }



        public IActionResult Login()
        {
            return View(new UserLoginModel());
        }



        [HttpPost]
        public IActionResult Login(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                var signInResult= _signInManager.PasswordSignInAsync(userLoginModel.UserName, userLoginModel.Password, userLoginModel.RememberMe, false).Result;
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                ModelState.AddModelError("", "Username or Password wrong");
            }
            return View(userLoginModel);
        }


        public IActionResult AddToCart(int id)
        {
            var product= _productRepository.GetById(id);
            _cartRepository.AddToCart(product);
            TempData["notification"] = "Product added to cart";
            return RedirectToAction("Index");
        }


        public IActionResult RemoveFromCart(decimal price)
        {
            _cartRepository.CleanCart();
            return RedirectToAction("Thanks",new { price = price });
        }

        public IActionResult Thanks(decimal price)
        {
            ViewBag.Price = price;
            return View();
        }



        public IActionResult NotFound(int code)
        {
            ViewBag.Code = code;
            return View();
        }

        public IActionResult CleanCart(int id)
        {
            var removedProduct = _productRepository.GetById(id);
            _cartRepository.RemoveFromCart(removedProduct);
            return RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            return View(_cartRepository.GetAllCartProducts());
        }


        [Route("/Error")]
        public IActionResult Error()
        {
            var errorInfo= HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var logger= LogManager.GetLogger("FileLogger");
            logger.Log(LogLevel.Error,$"\n Error Location:{errorInfo.Path} \n Error Message:{errorInfo.Error.Message}\n Stack Trace:{errorInfo.Error.StackTrace}");
            return View();
        }
    }
}
