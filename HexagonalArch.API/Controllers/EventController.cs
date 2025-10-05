using Microsoft.AspNetCore.Mvc;
using HexagonalArch.API.Dtos;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;
using System.Globalization;

namespace HexagonalArch.API.Controllers;


[ApiController]
[Route("events")]
public class EventController : ControllerBase
{
    private readonly CustomerService _customerService;
    private readonly EventService _eventService;
    private readonly PartnerService _partnerService;

    public EventController(
        CustomerService customerService,
        EventService eventService,
        PartnerService partnerService)
    {
        _customerService = customerService;
        _eventService = eventService;
        _partnerService = partnerService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Event), StatusCodes.Status201Created)]
    public async Task<ActionResult<Event>> Create([FromBody] EventDTO dto)
    {
        var eventDate = DateTime.ParseExact(dto.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        var partner = await _partnerService.FindByIdAsync(dto.Partner.Id);
        if (partner is null)
        {
            return BadRequest("Partner not found");
        }

        var @event = new Event
        {
            Name = dto.Name,
            Date = eventDate,
            TotalSpots = dto.TotalSpots,
            Partner = partner
        };

        var savedEvent = await _eventService.SaveAsync(@event);
        return Created($"/events/{savedEvent.Id}", savedEvent);
    }

    [HttpPost("{id:long}/subscribe")]
    public async Task<IActionResult> Subscribe(long id, [FromBody] SubscribeDTO dto)
    {
        var customer = await _customerService.FindByIdAsync(dto.CustomerId);
        if (customer is null)
        {
            return UnprocessableEntity("Customer not found");
        }

        var @event = await _eventService.FindByIdAsync(id);
        if (@event is null)
        {
            return NotFound();
        }

        var existingTicket = await _eventService.FindTicketByEventIdAndCustomerIdAsync(id, dto.CustomerId);
        if (existingTicket is not null)
        {
            return UnprocessableEntity("Email already registered");
        }

        if (@event.TotalSpots <= @event.Tickets.Count)
        {
            return BadRequest("Event sold out");
        }

        var ticket = new Ticket
        {
            Event = @event,
            Customer = customer,
            ReservedAt = DateTime.UtcNow,
            Status = TicketStatus.Pending
        };

        @event.Tickets.Add(ticket);
        await _eventService.SaveAsync(@event);

        return Ok(new EventDTO(@event));
    }
}

