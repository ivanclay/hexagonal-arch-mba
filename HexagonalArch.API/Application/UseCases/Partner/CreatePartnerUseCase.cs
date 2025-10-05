using HexagonalArch.API.Application.UseCases;
using HexagonalArch.API.Models;
using HexagonalArch.API.Services;

namespace HexagonalArch.API.Application.UseCases.PartnerUserCase;

public class CreatePartnerUseCase : UseCase<CreatePartnerUseCase.Input, CreatePartnerUseCase.Output>
{
    private readonly PartnerService partnerService;

    public CreatePartnerUseCase(PartnerService _partnerService)
    {
        partnerService = _partnerService;
    }

    public async override Task<Output> Execute(Input input)
    {
        var partner = new Partner
        {
           Cnpj = input.cnpj,
           Email = input.email,
           Name = input.name,
        };

        var savedPartner = await partnerService.SaveAsync(partner);

        return new Output(
           id: savedPartner.Id,
           name: savedPartner.Name,
           email: savedPartner.Email,
           cnpj: savedPartner.Cnpj
       );
    }

    public record Input(string cnpj, string email, string name);
    

    public record Output(long id, string cnpj, string email, string name);
    
}


