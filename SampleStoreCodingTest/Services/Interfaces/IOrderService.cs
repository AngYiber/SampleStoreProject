using SampleStoreCodingTest.Models.Dtos;

namespace SampleStoreCodingTest.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CreateOrderRequest request);
    }
}
