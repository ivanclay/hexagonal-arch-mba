using HexagonalArch.API.Application.Exceptions;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;
using static HexagonalArch.API.Application.UseCases.EventUseCase.CreateEventUseCase;

namespace HexagonalArch.API.Application.UseCases.EventUseCase;

public class CreateEventUseCase : UseCase<Input, Output> 
{
    private readonly EventService eventService;
    private readonly PartnerService partnerService;

    public CreateEventUseCase(EventService _eventService, PartnerService _partnerService)
    {
        eventService = _eventService;
        partnerService = _partnerService;
    }

    public async override Task<Output> Execute(Input input)
    {
        var _event = new Event
        {
            Date = Convert.ToDateTime(input.date),
            Name = input.name,
            TotalSpots = input.totalSpots
        };

        var partner = await partnerService.FindByIdAsync(input.partnerId);
        if (partner is null)
            throw new ValidationException("Partner not found");

        _event.Partner = partner;

        var savedEvent = await eventService.SaveAsync(_event);

        return new Output(
            id: savedEvent.Id,
            date: savedEvent.Date.ToString("yyyy-MM-dd"),
            name: savedEvent.Name,
            totalSpots: savedEvent.TotalSpots,
            partnerId: savedEvent.Partner.Id
        );
    }

    public record Input(string date, string name, long partnerId, int totalSpots);

    public record Output(long id, string date, string name, int totalSpots, long partnerId);
}
