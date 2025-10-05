using HexagonalArch.API.Application.UseCases.Customer;
using static HexagonalArch.API.Application.UseCases.Customer.CreateCustomerUseCase;

namespace HexagonalArch.Test.Application.UseCases;

public class CreateCustomerUseCaseTest
{
    [Fact]
    public async Task DeveCriarUmCustomer()
    {
        const string expectedCpf = "12345678901";
        const string expectedEmail = "john.doe@gmil.com";
        const string expectedName = "John Doe";

        var createInput = new Input(expectedCpf, expectedEmail, expectedName);

        var useCase = new CreateCustomerUseCase();
        var output = await useCase.Execute(createInput);

        Assert.True(output.id > 0);
        Assert.Equal(expectedCpf, output.cpf);
        Assert.Equal(expectedEmail, output.email);
        Assert.Equal(expectedName, output.name);
    }
}
