using SampleStoreCodingTest.Models.Dtos;

namespace SampleStoreCodingTest.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerOrderPredictionDto>> GetCustomerPredictedOrdersAsync();
        Task<IEnumerable<CustomerOrderDto>> GetOrdersByCustomerAsync(int customerId);
    }
}
