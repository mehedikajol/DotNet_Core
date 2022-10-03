using ECom.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.GetProducts
{
    public class GetProducts
    {
        private readonly AppDbContext _context;
        public GetProducts(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductViewModel>> Do()
        {
            return await _context.Products.Select(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Price = $"Tk {x.Price.ToString("N2")}"
            }).ToListAsync();
        }
    }

    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}
