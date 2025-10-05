using HexagonalArch.API.Application.UseCases;

namespace HexagonalArch.API.Application.UseCases.CustomerUserCase;

public class GetCustomerByIdUseCase : UseCase<GetCustomerByIdUseCase.Input, GetCustomerByIdUseCase.Output>
{
    public override Task<Output> Execute(Input input)
    {
        throw new NotImplementedException();
    }

    public record Input(long id);

    public record Output(long id, string cpf, string email, string name);
}


