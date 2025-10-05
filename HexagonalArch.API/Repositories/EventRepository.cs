using HexagonalArch.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArch.API.Repositories;


public class EventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Event?> FindByIdAsync(long id)
    {
        return await _context.Events
            .Include(e => e.Partner)
            .Include(e => e.Tickets)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Event @event)
    {
        await _context.Events.AddAsync(@event);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Event @event)
    {
        _context.Events.Update(@event);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Event @event)
    {
        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
    }
}

