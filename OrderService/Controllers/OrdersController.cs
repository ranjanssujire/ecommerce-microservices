using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Domain;
using OrderService.DTOs;
using OrderService.Application.Services;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService.Application.Services.OrderService _service;

    public OrdersController(OrderService.Application.Services.OrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllOrders();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var order = new Order
        {
            ProductName = dto.ProductName,
            Quantity = dto.Quantity,
            Price = dto.Price
        };

        await _service.CreateOrder(order);

        return Ok(order);
    }
}