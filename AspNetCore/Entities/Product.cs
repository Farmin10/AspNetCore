using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Entities
{
    public class Product:IEquatable<Product>
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

        public bool Equals([AllowNull] Product other)
        {
            return Id == other.Id && Name == other.Name && Picture == other.Picture && Price == other.Price;
        }
    }
}
