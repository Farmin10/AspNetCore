using AspNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCore.Interfaces
{
    public interface IProductCategoryRepository:IGenericRepository<ProductCategory>
    {
        ProductCategory GetByFilter(Expression<Func<ProductCategory, bool>> filter);
    }
}
