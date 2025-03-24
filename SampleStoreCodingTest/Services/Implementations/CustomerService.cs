using Microsoft.EntityFrameworkCore;
using SampleStoreCodingTest.Data;
using SampleStoreCodingTest.Models.Dtos;
using SampleStoreCodingTest.Services.Interfaces;

namespace SampleStoreCodingTest.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly StoreSampleDbContext _context;

        public CustomerService(StoreSampleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerOrderPredictionDto>> GetCustomerPredictedOrdersAsync()
        {
            var sql = @"
WITH CustomerOrders AS (
  SELECT
    O.custid,
    C.companyname AS CustomerName,
    O.orderdate,
    ROW_NUMBER() OVER (PARTITION BY O.custid ORDER BY O.orderdate) AS rn
  FROM Sales.Orders O
  JOIN Sales.Customers C ON O.custid = C.custid
),
OrderDifferences AS (
  SELECT
    curr.custid,
    curr.CustomerName,
    DATEDIFF(DAY, prev.orderdate, curr.orderdate) AS DaysBetween
  FROM CustomerOrders curr
  JOIN CustomerOrders prev
    ON curr.custid = prev.custid
   AND curr.rn = prev.rn + 1
),
Averages AS (
  SELECT
    custid,
    AVG(DaysBetween * 1.0) AS AvgDaysBetween
  FROM OrderDifferences
  GROUP BY custid
),
LastOrders AS (
  SELECT
    O.custid,
    C.companyname AS CustomerName,
    MAX(O.orderdate) AS LastOrderDate
  FROM Sales.Orders O
  JOIN Sales.Customers C ON O.custid = C.custid
  GROUP BY O.custid, C.companyname
)
SELECT
  L.CustId,
  L.CustomerName,
  L.LastOrderDate,
  DATEADD(DAY, A.AvgDaysBetween, L.LastOrderDate) AS NextPredictedOrder
FROM LastOrders L
LEFT JOIN Averages A ON L.custid = A.custid";

            var predictions = await _context.Set<CustomerOrderPredictionDto>()
                .FromSqlRaw(sql)
                .AsNoTracking()
                .ToListAsync();

            return predictions;
        }

        public async Task<IEnumerable<CustomerOrderDto>> GetOrdersByCustomerAsync(int customerId)
        {
            var sql = @"
        SELECT
            orderid AS OrderId,
            requireddate AS RequiredDate,
            shippeddate AS ShippedDate,
            shipname AS ShipName,
            shipaddress AS ShipAddress,
            shipcity AS ShipCity
        FROM Sales.Orders
        WHERE custid = {0}";

            var orders = await _context.CustomerOrders
                .FromSqlInterpolated($@"
            SELECT
                orderid,
                requireddate,
                shippeddate,
                shipname,
                shipaddress,
                shipcity
            FROM Sales.Orders
            WHERE custid = {customerId}
        ")
                .AsNoTracking()
                .ToListAsync();

            return orders;
        }


    }


}
