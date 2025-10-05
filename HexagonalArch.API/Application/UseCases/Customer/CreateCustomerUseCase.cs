using HexagonalArch.API.Application.UseCases;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;

namespace HexagonalArch.API.Application.UseCases.CustomerUserCase;

public class CreateCustomerUseCase : UseCase<CreateCustomerUseCase.Input, CreateCustomerUseCase.Output>
{
    private readonly CustomerService customerService;

    public CreateCustomerUseCase(CustomerService _customerService)
    {
        customerService = _customerService;
    }

    public async override Task<Output> Execute(Input input)
    {
        var customer = new Customer
        {
            Cpf = input.cpf,
            Email = input.email,
            Name = input.name,
        };

        var savedCustomer = await customerService.SaveAsync(customer);

        return new Output(
           id: savedCustomer.Id,
           name: savedCustomer.Name,
           email: savedCustomer.Email,
           cpf: savedCustomer.Cpf
       );
    }

    public record Input(string cpf, string email, string name);

    public record Output(long id, string cpf, string email, string name);
}


