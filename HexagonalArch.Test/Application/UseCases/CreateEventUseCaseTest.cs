using HexagonalArch.API.Application.Exceptions;
using HexagonalArch.API.Application.UseCases.EventUseCase;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;
using Moq;

namespace HexagonalArch.Test.Application.UseCases;

public class CreateEventUseCaseTest
{
    [Fact(DisplayName = "Deve criar um evento")]
    public async Task DeveCriarUmEvento()
    {
        // Arrange
        var expectedDate = "2021-01-01";
        var expectedName = "Disney on Ice";
        var expectedTotalSpots = 10;
        var expectedPartnerId = 1;

        var input = new CreateEventUseCase.Input(expectedDate, expectedName, expectedPartnerId, expectedTotalSpots);

        var partner = new Partner
        {
            Id = expectedPartnerId,
            Name = "John Doe",
            Cnpj = "41.536.538/0001-00",
            Email = "john.doe@gmail.com"
        };

        var mockPartnerService = new Mock<PartnerService>();
        mockPartnerService
            .Setup(p => p.FindByIdAsync(expectedPartnerId))
            .ReturnsAsync(partner);

        var mockEventService = new Mock<EventService>();
        mockEventService
            .Setup(e => e.SaveAsync(It.IsAny<Event>()))
            .ReturnsAsync((Event e) =>
            {
                e.Id = 1;
                return e;
            });

        var useCase = new CreateEventUseCase(mockEventService.Object, mockPartnerService.Object);

        // Act
        var output = await useCase.Execute(input);

        // Assert
        Assert.Equal(1, output.id);
        Assert.Equal(expectedDate, output.date);
        Assert.Equal(expectedName, output.name);
        Assert.Equal(expectedPartnerId, output.partnerId);
    }

    [Fact(DisplayName = "Não deve criar um evento quando o Partner não for encontrado")]
    public async Task NaoDeveCriarEvento_QuandoPartnerNaoExiste()
    {
        // Arrange
        var input = new CreateEventUseCase.Input("2021-01-01", "Disney on Ice", 1, 10);

        var mockPartnerService = new Mock<PartnerService>();
        mockPartnerService
            .Setup(p => p.FindByIdAsync(input.partnerId))
            .ReturnsAsync((Partner)null); // Simula não encontrado

        var mockEventService = new Mock<EventService>();

        var useCase = new CreateEventUseCase(mockEventService.Object, mockPartnerService.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() => useCase.Execute(input));
        Assert.Equal("Partner not found", exception.Message);
    }

}
