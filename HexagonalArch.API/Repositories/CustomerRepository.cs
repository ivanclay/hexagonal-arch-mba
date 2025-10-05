using HexagonalArch.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArch.API.Repositories;


public class CustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> FindByIdAsync(long id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<Customer?> FindByCpfAsync(string cpf)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Cpf == cpf);
    }

    public async Task<Customer?> FindByEmailAsync(string email)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
}

