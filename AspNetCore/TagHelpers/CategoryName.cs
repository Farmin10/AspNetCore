using AspNetCore.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.TagHelpers
{
    [HtmlTargetElement("getCategoryName")]
    public class CategoryName:TagHelper
    {
        private IProductRepository _productRepository;

        public CategoryName(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public int ProductId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string data = "";
            var categories= _productRepository.GetCategories(ProductId).Select(i => i.Name);
            foreach (var category in categories)
            {
                data += category+" ";
            }
            output.Content.SetContent(data);
        }
    }
}
