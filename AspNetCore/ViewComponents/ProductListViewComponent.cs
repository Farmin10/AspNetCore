using AspNetCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewComponents
{
    public class ProductListViewComponent:ViewComponent
    {
        private IProductRepository _productRepository;

        public ProductListViewComponent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_productRepository.GetAll());
        }
    }
}
