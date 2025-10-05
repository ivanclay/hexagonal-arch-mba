using HexagonalArch.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArch.API.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TicketStatus enum as string
        modelBuilder.Entity<Ticket>()
            .Property(t => t.Status)
            .HasConversion<string>();

        // Optional: Configure relationships explicitly if needed
        modelBuilder.Entity<Event>()
            .HasMany(e => e.Tickets)
            .WithOne(t => t.Event)
            .HasForeignKey("EventId");

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        modelBuilder.Entity<Event>()
            .HasOne(e => e.Partner)
            .WithMany()
            .HasForeignKey("PartnerId");

        base.OnModelCreating(modelBuilder);
    }
}
