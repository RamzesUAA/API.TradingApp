using API.TradingApp.Hubs;
using Domain.Services.Implementation;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt => opt.AddPolicy("AllowLocalWeb", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(host => true)));

// Just thought it would be easier for you not to run the DB instance... this is definitely something I wouldn't do in production =)
builder.Services.AddDbContext<TradingContext>(options =>
{
    options.UseInMemoryDatabase("TradingData");
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<IOrderBookService, OrderBookService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

builder.Services.AddScoped<IRepository<MarketDepthAuditLog>, Repository<MarketDepthAuditLog>>();


// Add SignalR
builder.Services.AddSignalR();

// Self hosted function (my amazing soulution for real-life loading...)
builder.Services.AddHostedService<OrderBookUpdateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalWeb");
app.UseRouting();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpints =>
{
    endpints.MapControllers();
    endpints.MapHub<OrderBookHub>("/orderbookhub");
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
