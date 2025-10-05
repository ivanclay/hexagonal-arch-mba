using HexagonalArch.API.Application.Exceptions;
using HexagonalArch.API.Application.UseCases;
using HexagonalArch.API.Services;

namespace HexagonalArch.API.Application.UseCases.CustomerUserCase;

public class GetCustomerByIdUseCase : UseCase<GetCustomerByIdUseCase.Input, GetCustomerByIdUseCase.Output>
{
    private readonly CustomerService customerService;

    public GetCustomerByIdUseCase(CustomerService _customerService)
    {
        this.customerService = _customerService;
    }

    public async override Task<Output> Execute(Input input)
    {
        var customer = await customerService.FindByIdAsync(input.id);
        if (customer is null)
            throw new ValidationException("Customer not found");

        return new Output(customer.Id, customer.Cpf, customer.Email, customer.Name);
    }

    public record Input(long id);

    public record Output(long id, string cpf, string email, string name);
}


