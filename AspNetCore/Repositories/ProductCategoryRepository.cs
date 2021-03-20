using AspNetCore.Contexts;
using AspNetCore.Entities;
using AspNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCore.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategory GetByFilter(Expression<Func<ProductCategory, bool>> filter)
        {
            using (var context=new AspContext())
            {
                return context.ProductCategories.FirstOrDefault(filter);
            }
        }
    }
}
