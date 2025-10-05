
using HexagonalArch.API.Application.UseCases.EventUseCase;
using HexagonalArch.API.Application.UseCases.PartnerUserCase;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;
using Moq;

namespace HexagonalArch.Test.Application.UseCases;

public class GetPartnerByIdUseCaseTest
{
    [Fact(DisplayName = "Deve obter um parceiro por ID")]
    public async Task DeveObterUmParceiroPorId()
    {
        // Arrange
        var expectedId = 1L;
        var expectedCnpj = "12345678901";
        var expectedEmail = "john.doe@gmail.com";
        var expectedName = "John Doe";

        var partner = new Partner
        {
            Id = expectedId,
            Cnpj = expectedCnpj,
            Email = expectedEmail,
            Name = expectedName
        };

        var input = new GetPartnerByIdUseCase.Input(expectedId);

        var mockPartnerService = new Mock<PartnerService>();
        mockPartnerService
            .Setup(p => p.FindByIdAsync(expectedId))
            .ReturnsAsync(partner);

        var useCase = new GetPartnerByIdUseCase(mockPartnerService.Object);

        // Act
        var output = await useCase.Execute(input);

        // Assert
        Assert.Equal(expectedId, output.id);
        Assert.Equal(expectedCnpj, output.cnpj);
        Assert.Equal(expectedEmail, output.email);
        Assert.Equal(expectedName, output.name);
    }
}

