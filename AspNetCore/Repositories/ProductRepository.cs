using AspNetCore.Contexts;
using AspNetCore.Entities;
using AspNetCore.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        private IProductCategoryRepository _productCategoryRepository;

        public ProductRepository(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public void AddCategory(ProductCategory productCategory)
        {
            var control = _productCategoryRepository.GetByFilter(i => i.CategoryId == productCategory.CategoryId && i.ProductId == productCategory.ProductId);
            if (control == null)
            {
                _productCategoryRepository.Add(productCategory);
            }
        }

        public void DeleteCategory(ProductCategory productCategory)
        {
            var control= _productCategoryRepository.GetByFilter(i => i.CategoryId == productCategory.CategoryId && i.ProductId == productCategory.ProductId);
            if (control!=null)
            {
                _productCategoryRepository.Delete(control);
            }
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            using (var context=new AspContext())
            {
                return context.Products.Join(context.ProductCategories, p => p.Id, pc => pc.ProductId, (product, productCategory) => new { 
                Product = product,
                ProductCategory = productCategory
                }).Where(i=>i.ProductCategory.CategoryId==categoryId).Select(i=>new Product { 
                Id=i.Product.Id,
                Name=i.Product.Name,
                Price=i.Product.Price,
                Picture=i.Product.Picture
                }).ToList();
            }
        }

        public List<Category> GetCategories(int productId)
        {
            using (var context = new AspContext())
            {
                return context.Products.Join(context.ProductCategories, product => product.Id, productCategory => productCategory.ProductId,
                (p, pc) => new
                {
                    product = p,
                    productCategory = pc
                }).Join(context.Categories, two => two.productCategory.CategoryId, category => category.Id, (pc, c) => new
                {
                    product = pc.product,
                    category = c,
                    productCategory = pc.productCategory
                }).Where(i => i.product.Id == productId).Select(i => new Category
                {
                    Name = i.category.Name,
                    Id = i.category.Id,
                }).ToList();
            }
        }
    }
}
