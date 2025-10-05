using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HexagonalArch.API.Models;


[Table("partners")]
public class Partner
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Cnpj { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public Partner() { }

    public Partner(long id, string name, string cnpj, string email)
    {
        Id = id;
        Name = name;
        Cnpj = cnpj;
        Email = email;
    }
}

