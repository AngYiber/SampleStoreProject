using Microsoft.AspNetCore.Mvc;
using SampleStoreCodingTest.Models.Dtos;
using SampleStoreCodingTest.Services.Interfaces;

namespace SampleStoreCodingTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Obtiene los clientes y la prediccion de fecha de nueva orden.
        /// </summary>
        [HttpGet("predicted-orders")]
        public async Task<ActionResult<IEnumerable<CustomerOrderPredictionDto>>> GetPredictedOrders()
        {
            var result = await _customerService.GetCustomerPredictedOrdersAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtiene las ordenes de un cliente
        /// </summary>
        [HttpGet("{id}/orders")]
        public async Task<ActionResult<IEnumerable<CustomerOrderDto>>> GetOrdersByCustomer(int id)
        {
            var orders = await _customerService.GetOrdersByCustomerAsync(id);

            if (!orders.Any())
                return NotFound($"No orders found for customer ID {id}");

            return Ok(orders);
        }

    }
}
