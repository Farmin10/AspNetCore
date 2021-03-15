using AspNetCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewComponents
{
    public class CategoryListViewComponent:ViewComponent
    {
        private ICategoryRepository _categoryRepository;

        public CategoryListViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        

        public IViewComponentResult Invoke()
        {
            return View(_categoryRepository.GetAll());
        }
    }
}
