using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HexagonalArch.API.Models;


[Table("tickets")]
public class Ticket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }

    [ForeignKey("EventId")]
    public Event? Event { get; set; }

    [Required]
    [EnumDataType(typeof(TicketStatus))]
    public TicketStatus Status { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime? ReservedAt { get; set; }

    public Ticket() { }

    public Ticket(long id, Customer? customer, Event? @event, TicketStatus status, DateTime? paidAt, DateTime? reservedAt)
    {
        Id = id;
        Customer = customer;
        Event = @event;
        Status = status;
        PaidAt = paidAt;
        ReservedAt = reservedAt;
    }

    public override bool Equals(object? obj)
    {
        return obj is Ticket other &&
               EqualityComparer<Customer?>.Default.Equals(Customer, other.Customer) &&
               EqualityComparer<Event?>.Default.Equals(Event, other.Event);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Customer, Event);
    }
}

