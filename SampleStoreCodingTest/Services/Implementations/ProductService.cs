using Microsoft.EntityFrameworkCore;
using SampleStoreCodingTest.Data;
using SampleStoreCodingTest.Models.Dtos;
using SampleStoreCodingTest.Services.Interfaces;

namespace SampleStoreCodingTest.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly StoreSampleDbContext _context;

        public ProductService(StoreSampleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _context.Products
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
