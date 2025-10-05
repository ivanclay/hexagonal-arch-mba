using HexagonalArch.API.Application.Exceptions;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;
using static HexagonalArch.API.Application.UseCases.TiketUseCase.SubscribeCustomerToEventUseCase;

namespace HexagonalArch.API.Application.UseCases.TiketUseCase;

public class SubscribeCustomerToEventUseCase: UseCase<Input, Output>
{
    private readonly CustomerService customerService;
    private readonly EventService eventService;

    public SubscribeCustomerToEventUseCase(CustomerService _customerService, EventService _eventService)
    {
        customerService = _customerService;
        eventService = _eventService;
    }

    public override async Task<Output> Execute(Input input)
    {
        var maybeCustomer = await customerService.FindByIdAsync(input.customerId);
        if (maybeCustomer == null)
            throw new ValidationException("Customer not found!");

        var maybeEvent = await eventService.FindByIdAsync(input.eventId);
        if (maybeEvent == null)
            throw new ValidationException("Event not found!");

        var maybeTicket = await eventService.FindTicketByEventIdAndCustomerIdAsync(input.eventId, input.customerId);
        if(maybeTicket == null)
            throw new ValidationException("Ticket not found!");

        var customer = maybeCustomer;
        var _event = maybeEvent;

        if(_event.TotalSpots < _event.Tickets.Count + 1)
            throw new ValidationException("Event sold out!");

        var ticket = new Ticket();
        ticket.Event = _event;
        ticket.Customer = customer;
        ticket.ReservedAt = DateTime.Now;
        ticket.Status = TicketStatus.Pending;

        return new Output(
            eventId: _event.Id,
            ticketStatus: ticket.Status.ToString(),
            reservationDate: ticket.ReservedAt
        );
    }

    public record Input(long eventId, long customerId);

    public record Output(long eventId, string ticketStatus, DateTime? reservationDate);
}
