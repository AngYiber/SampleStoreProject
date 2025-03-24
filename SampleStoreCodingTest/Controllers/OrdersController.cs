using Microsoft.AspNetCore.Mvc;
using SampleStoreCodingTest.Models.Dtos;
using SampleStoreCodingTest.Services.Interfaces;

namespace SampleStoreCodingTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Crea una nueva orden con detalle de producto.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<int>> CreateOrder(CreateOrderRequest request)
        {
            var newOrderId = await _orderService.CreateOrderAsync(request);
            return CreatedAtAction(nameof(CreateOrder), new { id = newOrderId }, newOrderId);
        }
    }
}
