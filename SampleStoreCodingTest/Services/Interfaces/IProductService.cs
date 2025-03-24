using SampleStoreCodingTest.Models.Dtos;

namespace SampleStoreCodingTest.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
    }
}