using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Entities;
using AspNetCore.Interfaces;
using AspNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {

        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View(new ProductAddModel());
        }

        [HttpPost]
        public IActionResult Create(ProductAddModel productAddModel)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                if (productAddModel.Picture != null)
                {
                    var path = Path.GetExtension(productAddModel.Picture.FileName);
                    var newPictureName = Guid.NewGuid() + path;
                    var uploadSpace = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + newPictureName);
                    var stream = new FileStream(uploadSpace, FileMode.Create);
                    productAddModel.Picture.CopyTo(stream);
                    product.Picture = newPictureName;
                }
                product.Name = productAddModel.Name;
                product.Price = productAddModel.Price;
                _productRepository.Add(product);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }


        public IActionResult Update(int id)
        {
            var getProduct = _productRepository.GetById(id);
            ProductUpdateModel productUpdateModel = new ProductUpdateModel
            {
                Name = getProduct.Name,
                Price = getProduct.Price,
                Id = getProduct.Id
            };
            return View(productUpdateModel);
        }


        [HttpPost]
        public IActionResult Update(ProductUpdateModel productUpdateModel)
        {
            if (ModelState.IsValid)
            {
                var updatedProduct = _productRepository.GetById(productUpdateModel.Id);
                if (productUpdateModel.Picture != null)
                {
                    var path = Path.GetExtension(productUpdateModel.Picture.FileName);
                    var newPictureName = Guid.NewGuid() + path;
                    var uploadSpace = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + newPictureName);
                    var stream = new FileStream(uploadSpace, FileMode.Create);
                    productUpdateModel.Picture.CopyTo(stream);
                    updatedProduct.Picture = newPictureName;
                }

                updatedProduct.Name = productUpdateModel.Name;
                updatedProduct.Price = productUpdateModel.Price;

                _productRepository.Update(updatedProduct);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View(productUpdateModel);
        }



        public IActionResult Delete(int id)
        {
            _productRepository.Delete(new Product { Id = id });
            return RedirectToAction("Index");
        }


        public IActionResult AddCategory(int id)
        {
            var productCategory = _productRepository.GetCategories(id).Select(i=>i.Name);
            var categories = _categoryRepository.GetAll();

            TempData["ProductId"] = id;

            List<AddCategoryModel> list = new List<AddCategoryModel>();
            foreach (var item in categories)
            {
                AddCategoryModel addCategoryModel = new AddCategoryModel();
                addCategoryModel.CategoryId = item.Id;
                addCategoryModel.CategoryName = item.Name;
                addCategoryModel.IsAllow = productCategory.Contains(item.Name);
                list.Add(addCategoryModel);
            }
            return View(list);
        }



        [HttpPost]
        public IActionResult AddCategory(List<AddCategoryModel> addCategoryModel)
        {
            int productId = (int)TempData["ProductId"];
            foreach (var item in addCategoryModel)
            {
                if (item.IsAllow)
                {
                    _productRepository.AddCategory(new ProductCategory
                    {
                        CategoryId=item.CategoryId,
                        ProductId=productId
                    });
                }
                else
                {
                    _productRepository.DeleteCategory(new ProductCategory
                    {
                        CategoryId = item.CategoryId,
                        ProductId = productId
                    });
                }
            }
            return RedirectToAction("Index");
        }
    }
}
