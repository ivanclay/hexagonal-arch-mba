using Microsoft.AspNetCore.Mvc;
using HexagonalArch.API.Dtos;
using HexagonalArch.API.Services;
using HexagonalArch.API.Application.UseCases.EventUseCase;
using HexagonalArch.API.Application.UseCases.TiketUseCase;
using HexagonalArch.API.Application.Exceptions;

namespace HexagonalArch.API.Controllers;


[ApiController]
[Route("events")]
public class EventController : ControllerBase
{
    private readonly CreateEventUseCase _createEventUseCase;
    private readonly SubscribeCustomerToEventUseCase _subscribeCustomerToEventUseCase;

    public EventController(CreateEventUseCase createEventUseCase, SubscribeCustomerToEventUseCase subscribeCustomerToEventUseCase)
    {
        _createEventUseCase = createEventUseCase;
        _subscribeCustomerToEventUseCase = subscribeCustomerToEventUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateEventUseCase.Output), StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateEventUseCase.Output>> Create([FromBody] EventDTO dto)
    {
        try
        {
            var partnerId = dto.Partner != null ? dto.Partner.Id : 0;
                        
            var output = await _createEventUseCase.Execute(new CreateEventUseCase.Input(dto.Date, dto.Name, partnerId, dto.TotalSpots));
            return Created("/events", output);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id:long}/subscribe")]
    public async Task<ActionResult<SubscribeCustomerToEventUseCase.Output>> Subscribe(long id, [FromBody] SubscribeDTO dto)
    {
        try
        {
            var output = await _subscribeCustomerToEventUseCase.Execute(new SubscribeCustomerToEventUseCase.Input(id, dto.CustomerId));

            return Ok(output);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}

