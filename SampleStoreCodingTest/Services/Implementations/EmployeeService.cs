using Microsoft.EntityFrameworkCore;
using SampleStoreCodingTest.Data;
using SampleStoreCodingTest.Models.Dtos;
using SampleStoreCodingTest.Services.Interfaces;

namespace SampleStoreCodingTest.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly StoreSampleDbContext _context;

        public EmployeeService(StoreSampleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Select(e => new EmployeeDto
                {
                    EmpId = e.EmpId,
                    FullName = e.FirstName + " " + e.LastName
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
