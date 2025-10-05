using HexagonalArch.API.Application.UseCases.PartnerUserCase;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;
using Moq;

namespace HexagonalArch.Test.Application.UseCases.PartnerUseCase;

public class CreatePartnerUseCaseTest
{
    [Fact(DisplayName = "Deve criar um evento")]
    public async Task DeveCriarUmEvento()
    {
        // Arrange
        var expectedCnpj = "12345678901";
        var expectedEmail = "john.doe@gmail.com";
        var expectedName = "John Doe";

        var input = new CreatePartnerUseCase.Input(expectedCnpj,expectedEmail, expectedName);

        var partner = new Partner
        {
            Cnpj = input.cnpj,
            Email = input.email,
            Name = input.name,
        };

        var mockPartnerService = new Mock<PartnerService>();
        mockPartnerService
            .Setup(p => p.SaveAsync(It.IsAny<Partner>()))
            .ReturnsAsync((Partner p) =>
            {
                p.Id = 1;
                return p;
            });

        var useCase = new CreatePartnerUseCase(mockPartnerService.Object);

        // Act
        var output = await useCase.Execute(input);

        // Assert
        Assert.Equal(1, output.id);
        Assert.Equal(expectedCnpj, output.cnpj);
        Assert.Equal(expectedName, output.name);
    }

}
