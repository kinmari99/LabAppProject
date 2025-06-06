﻿using LabApp.Dtos;
using LabApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabApp.Controllers
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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
        {
            var created = await _orderService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDto dto)
        {
            var success = await _orderService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _orderService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
