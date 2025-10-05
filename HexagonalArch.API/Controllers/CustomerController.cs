using Microsoft.AspNetCore.Mvc;
using HexagonalArch.API.Dtos;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;

namespace HexagonalArch.API.Controllers;


[ApiController]
[Route("customers")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomerDTO dto)
    {
        var existingByCpf = await _customerService.FindByCpfAsync(dto.Cpf);
        if (existingByCpf is not null)
        {
            return UnprocessableEntity("Customer already exists");
        }

        var existingByEmail = await _customerService.FindByEmailAsync(dto.Email);
        if (existingByEmail is not null)
        {
            return UnprocessableEntity("Customer already exists");
        }

        var customer = new Customer
        {
            Name = dto.Name,
            Cpf = dto.Cpf,
            Email = dto.Email
        };

        customer = await _customerService.SaveAsync(customer);

        return Created($"/customers/{customer.Id}", customer);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var customer = await _customerService.FindByIdAsync(id);
        if (customer is null)
        {
            return NotFound();
        }

        return Ok(customer);
    }
}

