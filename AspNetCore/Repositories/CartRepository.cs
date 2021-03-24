using AspNetCore.CustomExtensions;
using AspNetCore.Entities;
using AspNetCore.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Repositories
{
    public class CartRepository:ICartRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void AddToCart(Product product)
        {
            var getList= _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("cart");
            if (getList==null)
            {
                getList = new List<Product>();
                getList.Add(product);
            }
            else
            {
                getList.Add(product);
            }
            _httpContextAccessor.HttpContext.Session.SetObject("cart", getList);
        }

        public void RemoveFromCart(Product product)
        {
            var getList = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("cart");
            getList.Remove(product);
            
            _httpContextAccessor.HttpContext.Session.SetObject("cart", getList);

        }

        public List<Product> GetAllCartProducts()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("cart");
        }

        public void CleanCart()
        {
            _httpContextAccessor.HttpContext.Session.Remove("cart");
        }
    }
}
