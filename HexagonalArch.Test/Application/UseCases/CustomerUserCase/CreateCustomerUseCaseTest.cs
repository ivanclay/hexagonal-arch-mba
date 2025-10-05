using HexagonalArch.API.Application.UseCases.CustomerUserCase;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;
using Moq;


namespace HexagonalArch.Test.Application.UseCases.CustomerUserCase;

public class CreateCustomerUseCaseTest
{
    [Fact]
    public async Task DeveCriarUmCustomer()
    {
        const string expectedCpf = "12345678901";
        const string expectedEmail = "john.doe@gmil.com";
        const string expectedName = "John Doe";

        var input = new CreateCustomerUseCase.Input(expectedCpf, expectedEmail, expectedName);

        var customer = new Customer
        {
            Cpf = input.cpf,
            Email = input.email,
            Name = input.name,
        };

        var mockCustomerService = new Mock<CustomerService>();
        mockCustomerService
            .Setup(c => c.SaveAsync(It.IsAny<Customer>()))
            .ReturnsAsync((Customer c) =>
            {
                c.Id = 1;
                return c;
            });

        var useCase = new CreateCustomerUseCase(mockCustomerService.Object);
        var output = await useCase.Execute(input);

        Assert.True(output.id > 0);
        Assert.Equal(expectedCpf, output.cpf);
        Assert.Equal(expectedEmail, output.email);
        Assert.Equal(expectedName, output.name);
    }
}
