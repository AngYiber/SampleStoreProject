using SampleStoreCodingTest.Models.Dtos;

namespace SampleStoreCodingTest.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    }
}
