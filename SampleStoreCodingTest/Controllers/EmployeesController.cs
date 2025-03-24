using Microsoft.AspNetCore.Mvc;
using SampleStoreCodingTest.Models.Dtos;
using SampleStoreCodingTest.Services.Interfaces;

namespace SampleStoreCodingTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Obtiene todos los empleados con nombre completo.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var result = await _employeeService.GetAllEmployeesAsync();
            return Ok(result);
        }
    }
}
