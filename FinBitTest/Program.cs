using Dapper.FluentMap;
using FinBit.Persistence;
using FinBit.Services;
using FinBit.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DbConnectionString>(builder.Configuration.GetSection(nameof(DbConnectionString)));

builder.Services.AddScoped<IValuesHandler, ValuesHandler>();
builder.Services.AddScoped<IValueRepository, ValueRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

FluentMapper.Initialize(config => config.AddMap(new ValueMap()));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
