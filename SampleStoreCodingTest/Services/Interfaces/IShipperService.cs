using SampleStoreCodingTest.Models.Dtos;

namespace SampleStoreCodingTest.Services.Interfaces
{
    public interface IShipperService
    {
        Task<IEnumerable<ShipperDto>> GetAllAsync();
    }
}
