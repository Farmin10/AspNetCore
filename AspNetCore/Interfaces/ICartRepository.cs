using AspNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Interfaces
{
    public interface ICartRepository
    {
        void AddToCart(Product product);
        void RemoveFromCart(Product product);
        List<Product> GetAllCartProducts();
        void CleanCart();
    }
}
