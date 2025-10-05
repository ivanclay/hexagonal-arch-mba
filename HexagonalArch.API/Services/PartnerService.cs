using HexagonalArch.API.Models;
using HexagonalArch.API.Repositories;

namespace HexagonalArch.API.Services;


public class PartnerService
{
    private readonly PartnerRepository _repository;

    public PartnerService(PartnerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Partner> SaveAsync(Partner partner)
    {
        await _repository.AddAsync(partner);
        return partner;
    }

    public async Task<Partner?> FindByIdAsync(long id)
    {
        return await _repository.FindByIdAsync(id);
    }

    public async Task<Partner?> FindByCnpjAsync(string cnpj)
    {
        return await _repository.FindByCnpjAsync(cnpj);
    }

    public async Task<Partner?> FindByEmailAsync(string email)
    {
        return await _repository.FindByEmailAsync(email);
    }
}

