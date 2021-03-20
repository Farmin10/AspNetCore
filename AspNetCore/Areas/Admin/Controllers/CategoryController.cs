using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Entities;
using AspNetCore.Interfaces;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View(_categoryRepository.GetAll());
        }
        public IActionResult Create()
        {

            return View(new CategoryAddModel());
        }


        [HttpPost]
        public IActionResult Create(CategoryAddModel categoryAddModel )
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(new Category { 
                Name=categoryAddModel.Name
                });
                return RedirectToAction("Index");
            }
            return View(categoryAddModel);
        }


        public IActionResult Update(int id)
        {
            var updatedCategory = _categoryRepository.GetById(id);
            CategoryUpdateModel categoryUpdateModel = new CategoryUpdateModel
            {
                Id=updatedCategory.Id,
                Name=updatedCategory.Name
            };
            return View(categoryUpdateModel);
        }



        [HttpPost]
        public IActionResult Update(CategoryUpdateModel categoryUpdateModel)
        {
            if (ModelState.IsValid)
            {
                var updatedCategory = _categoryRepository.GetById(categoryUpdateModel.Id);
                updatedCategory.Name = categoryUpdateModel.Name;
                _categoryRepository.Update(updatedCategory);
                return RedirectToAction("Index");
            }
            
            return View(categoryUpdateModel);
        }


        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(new Category { Id = id });
            return RedirectToAction("Index");
        }
    }
}
