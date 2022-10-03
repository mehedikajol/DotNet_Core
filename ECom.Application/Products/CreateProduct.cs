using ECom.Database;
using ECom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.CreateProducts
{
    public class CreateProduct
    {
        private readonly AppDbContext _context;
        public CreateProduct(AppDbContext context)
        {
            _context = context;
        }

        public async Task DoAsync(ProductViewModel product)
        {
            _context.Products.Add(new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            });

            await _context.SaveChangesAsync();
        }
    }

    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
