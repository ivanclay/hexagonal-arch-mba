using HexagonalArch.API.Application.UseCases.PartnerUserCase;
using HexagonalArch.API.Dtos;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HexagonalArch.API.Controllers;

[ApiController]
[Route("partners")]
public class PartnerController : ControllerBase
{
    private readonly CreatePartnerUseCase _createPartnerUseCase;
    private readonly GetPartnerByIdUseCase _getPartnerByIdUseCase;

    public PartnerController(CreatePartnerUseCase createPartnerUseCase, GetPartnerByIdUseCase getPartnerByIdUseCase )
    {
        _createPartnerUseCase = createPartnerUseCase;
        _getPartnerByIdUseCase = getPartnerByIdUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PartnerDTO dto)
    {
        var output = await _createPartnerUseCase.Execute(new CreatePartnerUseCase.Input(dto.Cnpj, dto.Email, dto.Name));
        return Created($"/partners/{output.id}", output);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var output = await _getPartnerByIdUseCase.Execute(new GetPartnerByIdUseCase.Input(id));
        if (output is null)
        {
            return NotFound();
        }

        return Ok(output);
    }
}

