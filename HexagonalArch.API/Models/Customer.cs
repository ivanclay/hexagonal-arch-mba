using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HexagonalArch.API.Models;


[Table("customers")]
public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Cpf { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public Customer() { }

    public Customer(long id, string name, string cpf, string email)
    {
        Id = id;
        Name = name;
        Cpf = cpf;
        Email = email;
    }

    public override bool Equals(object? obj)
    {
        return obj is Customer other && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

