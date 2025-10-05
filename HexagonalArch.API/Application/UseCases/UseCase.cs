namespace HexagonalArch.API.Application.UseCases;

public abstract class UseCase<TInput, TOutput>
{
    public abstract Task<TOutput> Execute(TInput input);
}

