using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Entities;
using AspNetCore.Interfaces;
using AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<AppUser> _signInManager;

        private IProductRepository _productRepository;
        
        public HomeController(IProductRepository productRepository, SignInManager<AppUser> signInManager)
        {
            _productRepository = productRepository;
            _signInManager = signInManager;
        }




        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
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
    }
}
