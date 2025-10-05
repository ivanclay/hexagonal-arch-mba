using HexagonalArch.API.Application.UseCases;

namespace HexagonalArch.API.Application.UseCases.Customer;

public class CreateCustomerUseCase : UseCase<CreateCustomerUseCase.Input, CreateCustomerUseCase.Output>
{
    public override Task<Output> Execute(Input input)
    {
        throw new NotImplementedException();
    }

    public record Input(string cpf, string email, string name);

    public record Output(long id, string cpf, string email, string name);
}


