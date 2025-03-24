using Microsoft.EntityFrameworkCore;
using SampleStoreCodingTest.Data;
using SampleStoreCodingTest.Models.Dtos;
using SampleStoreCodingTest.Services.Interfaces;

namespace SampleStoreCodingTest.Services.Implementations
{
    public class ShipperService : IShipperService
    {
        private readonly StoreSampleDbContext _context;

        public ShipperService(StoreSampleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShipperDto>> GetAllAsync()
        {
            return await _context.Shippers
                .Select(s => new ShipperDto
                {
                    ShipperId = s.ShipperId,
                    CompanyName = s.CompanyName
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
