using AspNetCore.Contexts;
using AspNetCore.Entities;
using AspNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Repositories
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
       
    }
}
