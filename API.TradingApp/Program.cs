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

// Just thought it would be easier for you not to run the DB instance... this is definitely something I wouldn't do in production =)
builder.Services.AddDbContext<TradingContext>(options =>
{
    options.UseInMemoryDatabase("TradingData");
});

builder.Services.AddScoped<IRepository<MarketDepthAuditLog>, Repository<MarketDepthAuditLog>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(conf =>
      conf.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod()
        );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
