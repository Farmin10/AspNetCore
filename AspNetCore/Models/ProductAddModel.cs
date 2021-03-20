using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class ProductAddModel
    {
        [Required(ErrorMessage ="Enter Name")]
        public string  Name { get; set; }
        [Range(0,double.MaxValue,ErrorMessage ="Price must bigger than 0")]
        public decimal Price { get; set; }

        public IFormFile Picture { get; set; }
    }
}
