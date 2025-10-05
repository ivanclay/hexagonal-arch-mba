using HexagonalArch.API.Application.Exceptions;
using HexagonalArch.API.Services;

namespace HexagonalArch.API.Application.UseCases.PartnerUserCase;

public class GetPartnerByIdUseCase : UseCase<GetPartnerByIdUseCase.Input, GetPartnerByIdUseCase.Output>
{
    private readonly PartnerService partnerService;

    public GetPartnerByIdUseCase(PartnerService partnerService)
    {
        this.partnerService = partnerService;
    }

    public async override Task<Output> Execute(Input input)
    {
        var partner = await partnerService.FindByIdAsync(input.id);
        if (partner is null)
            throw new ValidationException("Partner not found");

        return new Output(partner.Id, partner.Cnpj, partner.Email, partner.Name);
    }

    public record Input(long id);

    public record Output(long id, string cnpj, string email, string name);

}


