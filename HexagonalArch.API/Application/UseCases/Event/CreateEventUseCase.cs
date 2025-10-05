using HexagonalArch.API.Application.UseCases;
using static HexagonalArch.API.Application.UseCases.Event.CreateEventUseCase;

namespace HexagonalArch.API.Application.UseCases.Event;

public class CreateEventUseCase : UseCase<Input, Output> 
{
    public override Output Execute(Input input)
    {
        throw new NotImplementedException();
    }

    public record Input(string date, string name, long partnerId, int totalSpots);

    public record Output(long id, string date, string name, long partnerId);
}
