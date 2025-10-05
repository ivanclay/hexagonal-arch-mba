using HexagonalArch.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArch.API.Repositories;


public class TicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket?> FindByIdAsync(long id)
    {
        return await _context.Tickets
            .Include(t => t.Customer)
            .Include(t => t.Event)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Ticket?> FindByEventIdAndCustomerIdAsync(long eventId, long customerId)
    {
        return await _context.Tickets
            .Include(t => t.Customer)
            .Include(t => t.Event)
            .FirstOrDefaultAsync(t => t.Event!.Id == eventId && t.Customer!.Id == customerId);
    }

    public async Task AddAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }
}

