using HexagonalArch.API.Models;

namespace HexagonalArch.API.Dtos;

public class PartnerDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public PartnerDTO() { }

    public PartnerDTO(long id)
    {
        Id = id;
    }

    public PartnerDTO(Partner partner)
    {
        Id = partner.Id;
        Name = partner.Name;
        Cnpj = partner.Cnpj;
        Email = partner.Email;
    }
}
