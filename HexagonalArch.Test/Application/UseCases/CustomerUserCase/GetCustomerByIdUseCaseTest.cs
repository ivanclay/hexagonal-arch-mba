using HexagonalArch.API.Application.UseCases.CustomerUserCase;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;
using Moq;

namespace HexagonalArch.Test.Application.UseCases;

public class GetCustomerByIdUseCaseTest
{
    [Fact(DisplayName = "Deve obter um cliente por ID")]
    public async Task DeveObterUmCustomerPorId()
    {
        // Arrange
        var expectedId = 1L;
        var expectedCpf = "12345678901";
        var expectedEmail = "john.doe@gmail.com";
        var expectedName = "John Doe";

        var customer = new Customer
        {
            Id = expectedId,
            Cpf = expectedCpf,
            Email = expectedEmail,
            Name = expectedName
        };

        var input = new GetCustomerByIdUseCase.Input(expectedId);

        var mockCustomerService = new Mock<CustomerService>();
        mockCustomerService
            .Setup(c => c.FindByIdAsync(expectedId))
            .ReturnsAsync(customer);

        var useCase = new GetCustomerByIdUseCase(mockCustomerService.Object);

        // Act
        var output = await useCase.Execute(input);

        // Assert
        Assert.Equal(expectedId, output.id);
        Assert.Equal(expectedCpf, output.cpf);
        Assert.Equal(expectedEmail, output.email);
        Assert.Equal(expectedName, output.name);
    }
}

