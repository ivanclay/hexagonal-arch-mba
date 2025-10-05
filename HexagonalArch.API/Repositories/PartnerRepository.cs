using HexagonalArch.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArch.API.Repositories;

public class PartnerRepository
{
    private readonly AppDbContext _context;

    public PartnerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Partner?> FindByIdAsync(long id)
    {
        return await _context.Partners.FindAsync(id);
    }

    public async Task<Partner?> FindByCnpjAsync(string cnpj)
    {
        return await _context.Partners.FirstOrDefaultAsync(p => p.Cnpj == cnpj);
    }

    public async Task<Partner?> FindByEmailAsync(string email)
    {
        return await _context.Partners.FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task AddAsync(Partner partner)
    {
        await _context.Partners.AddAsync(partner);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Partner partner)
    {
        _context.Partners.Update(partner);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Partner partner)
    {
        _context.Partners.Remove(partner);
        await _context.SaveChangesAsync();
    }
}

