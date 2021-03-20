using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class CategoryAddModel
    {
        [Required(ErrorMessage ="Enter Category Name")]
        public string  Name { get; set; }
    }
}
