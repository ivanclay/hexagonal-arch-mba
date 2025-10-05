using HexagonalArch.API.Dtos;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HexagonalArch.API.Controllers;

[ApiController]
[Route("partners")]
public class PartnerController : ControllerBase
{
    private readonly PartnerService _partnerService;

    public PartnerController(PartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PartnerDTO dto)
    {
        var existingByCnpj = await _partnerService.FindByCnpjAsync(dto.Cnpj);
        if (existingByCnpj is not null)
        {
            return UnprocessableEntity("Partner already exists");
        }

        var existingByEmail = await _partnerService.FindByEmailAsync(dto.Email);
        if (existingByEmail is not null)
        {
            return UnprocessableEntity("Partner already exists");
        }

        var partner = new Partner
        {
            Name = dto.Name,
            Cnpj = dto.Cnpj,
            Email = dto.Email
        };

        partner = await _partnerService.SaveAsync(partner);

        return Created($"/partners/{partner.Id}", partner);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var partner = await _partnerService.FindByIdAsync(id);
        if (partner is null)
        {
            return NotFound();
        }

        return Ok(partner);
    }
}

