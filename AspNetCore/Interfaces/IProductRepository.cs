using AspNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        List<Category> GetCategories(int productId);
    }
}
