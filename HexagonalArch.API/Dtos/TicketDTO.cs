using HexagonalArch.API.Models;

namespace HexagonalArch.API.Dtos;

public class TicketDTO
{
    public long Id { get; set; }
    public int Spot { get; set; }
    public CustomerDTO Customer { get; set; } = new();
    public EventDTO Event { get; set; } = new();
    public TicketStatus Status { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime? ReservedAt { get; set; }

    public TicketDTO() { }

    public TicketDTO(Ticket ticket)
    {
        Id = ticket.Id;
        Customer = new CustomerDTO(ticket.Customer);
        Event = new EventDTO(ticket.Event);
        Status = ticket.Status;
        PaidAt = ticket.PaidAt;
        ReservedAt = ticket.ReservedAt;
    }
}

