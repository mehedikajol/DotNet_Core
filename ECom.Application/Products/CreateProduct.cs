using ECom.Database;
using ECom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.Products
{
    public class CreateProduct
    {
        private readonly AppDbContext _context;
        public CreateProduct(AppDbContext context)
        {
            _context = context;
        }

        public void Do(int ProductId, string Name, string Description, decimal Price)
        {
            _context.Products.Add(new Product
            {
                ProductId = ProductId,
                Name = Name,
                Description = Description,
                Price = Price
            });
        }
    }
}
