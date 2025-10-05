using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace HexagonalArch.API.Models;


[Table("events")]
public class Event
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int TotalSpots { get; set; }

    [ForeignKey("PartnerId")]
    public Partner? Partner { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

    public Event() { }

    public Event(long id, string name, DateTime date, int totalSpots, ICollection<Ticket>? tickets)
    {
        Id = id;
        Name = name;
        Date = date;
        TotalSpots = totalSpots;
        Tickets = tickets ?? new HashSet<Ticket>();
    }

    public override bool Equals(object? obj)
    {
        return obj is Event other && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

