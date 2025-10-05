using HexagonalArch.API.Models;
using HexagonalArch.API.Repositories;

namespace HexagonalArch.API.Services;

public class EventService
{
    private readonly CustomerService _customerService;
    private readonly EventRepository _eventRepository;
    private readonly TicketRepository _ticketRepository;

    public EventService(
        CustomerService customerService,
        EventRepository eventRepository,
        TicketRepository ticketRepository)
    {
        _customerService = customerService;
        _eventRepository = eventRepository;
        _ticketRepository = ticketRepository;
    }

    public EventService(){  }

    public virtual async Task<Event> SaveAsync(Event @event)
    {
        await _eventRepository.AddAsync(@event);
        return @event;
    }

    public async Task<Event?> FindByIdAsync(long id)
    {
        return await _eventRepository.FindByIdAsync(id);
    }

    public async Task<Ticket?> FindTicketByEventIdAndCustomerIdAsync(long eventId, long customerId)
    {
        return await _ticketRepository.FindByEventIdAndCustomerIdAsync(eventId, customerId);
    }
}

