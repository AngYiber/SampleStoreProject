using Microsoft.AspNetCore.Mvc;
using SampleStoreCodingTest.Models.Dtos;
using SampleStoreCodingTest.Services.Interfaces;

namespace SampleStoreCodingTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : ControllerBase
    {
        private readonly IShipperService _shipperService;

        public ShippersController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        /// <summary>
        /// Retorna todos los shippers.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperDto>>> GetAll()
        {
            var result = await _shipperService.GetAllAsync();
            return Ok(result);
        }
    }
}
