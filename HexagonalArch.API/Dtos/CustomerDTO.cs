using HexagonalArch.API.Models;

namespace HexagonalArch.API.Dtos;


public class CustomerDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public CustomerDTO() { }

    public CustomerDTO(Customer customer)
    {
        Id = customer.Id;
        Name = customer.Name;
        Cpf = customer.Cpf;
        Email = customer.Email;
    }
}

