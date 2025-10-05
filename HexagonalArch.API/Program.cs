using HexagonalArch.API.Repositories;
using HexagonalArch.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<EventRepository>();
builder.Services.AddScoped<PartnerRepository>();
builder.Services.AddScoped<TicketRepository>();

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<PartnerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
