using HexagonalArch.API.Models;
using System.Globalization;

namespace HexagonalArch.API.Dtos;

public class EventDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public int TotalSpots { get; set; }
    public PartnerDTO Partner { get; set; } = new();

    public EventDTO()
    {
        
    }

    public EventDTO(Event _event)
    {
        Id = _event.Id;
        Name = _event.Name;
        Date = _event.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        TotalSpots = _event.TotalSpots;
        Partner = new PartnerDTO(_event.Partner);
    }
}
