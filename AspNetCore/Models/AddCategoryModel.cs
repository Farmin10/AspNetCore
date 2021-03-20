using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class AddCategoryModel
    {
        public int CategoryId { get; set; }
        public string  CategoryName { get; set; }
        public bool IsAllow { get; set; }
    }
}
