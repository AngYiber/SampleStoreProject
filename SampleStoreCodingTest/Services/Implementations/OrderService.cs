using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SampleStoreCodingTest.Data;
using SampleStoreCodingTest.Models.Dtos;
using SampleStoreCodingTest.Services.Interfaces;

namespace SampleStoreCodingTest.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly StoreSampleDbContext _context;

        public OrderService(StoreSampleDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateOrderAsync(CreateOrderRequest request)
        {
            var outputParam = new SqlParameter
            {
                ParameterName = "@NewOrderId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            var parameters = new[]
            {
                new SqlParameter("@CustId", request.CustId),
                new SqlParameter("@EmpId", request.EmpId),
                new SqlParameter("@ShipperId", request.ShipperId),
                new SqlParameter("@ShipName", request.ShipName),
                new SqlParameter("@ShipAddress", request.ShipAddress),
                new SqlParameter("@ShipCity", request.ShipCity),
                new SqlParameter("@ShipRegion", (object?)request.ShipRegion ?? DBNull.Value),
                new SqlParameter("@ShipPostalCode", (object?)request.ShipPostalCode ?? DBNull.Value),
                new SqlParameter("@ShipCountry", request.ShipCountry),
                new SqlParameter("@OrderDate", request.OrderDate),
                new SqlParameter("@RequiredDate", request.RequiredDate),
                new SqlParameter("@ShippedDate", (object?)request.ShippedDate ?? DBNull.Value),
                new SqlParameter("@Freight", request.Freight),
                new SqlParameter("@ProductId", request.ProductId),
                new SqlParameter("@UnitPrice", request.UnitPrice),
                new SqlParameter("@Qty", request.Qty),
                new SqlParameter("@Discount", request.Discount),
                new SqlParameter
                {
                    ParameterName = "@NewOrderId",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                }
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC Sales.usp_AddNewOrderWithDetails " +
                "@CustId, @EmpId, @ShipperId, @ShipName, @ShipAddress, @ShipCity, " +
                "@ShipRegion, @ShipPostalCode, @ShipCountry, @OrderDate, @RequiredDate, " +
                "@ShippedDate, @Freight, @ProductId, @UnitPrice, @Qty, @Discount, @NewOrderId OUTPUT",
                parameters);

            return (int)parameters.Last().Value!;
        }
    }
}
