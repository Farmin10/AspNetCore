using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Interfaces;
using AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _productRepository;
        
        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
            return View(new UserLoginModel());
        }
    }
}
