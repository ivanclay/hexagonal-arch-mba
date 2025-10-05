using Microsoft.AspNetCore.Mvc;
using HexagonalArch.API.Dtos;
using HexagonalArch.API.Application.UseCases.CustomerUserCase;

namespace HexagonalArch.API.Controllers;


[ApiController]
[Route("customers")]
public class CustomerController : ControllerBase
{
    private readonly CreateCustomerUseCase _createCustomerUseCase;
    private readonly GetCustomerByIdUseCase _getCustomerByIdUseCase;

    public CustomerController(CreateCustomerUseCase createCustomerUseCase, GetCustomerByIdUseCase getCustomerByIdUseCase)    
    {
        _createCustomerUseCase = createCustomerUseCase;
        _getCustomerByIdUseCase = getCustomerByIdUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomerDTO dto)
    {
        var output = await _createCustomerUseCase.Execute(new CreateCustomerUseCase.Input(dto.Cpf, dto.Email, dto.Name));
        return Created($"/customers/{output.id}", output);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var output = await _getCustomerByIdUseCase.Execute(new GetCustomerByIdUseCase.Input(id));
        
        if (output is null)
        {
            return NotFound();
        }

        return Ok(output);
    }
}

