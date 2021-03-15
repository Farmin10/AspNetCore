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
