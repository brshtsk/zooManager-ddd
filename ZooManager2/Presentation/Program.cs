using Infrastructure.DI;
// using Application.DI;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
// builder.Services.AddApplication();

builder.Services.AddSingleton<IAnimalTransferService, AnimalTransferService>();
builder.Services.AddSingleton<IFeedingOrganizationService, FeedingOrganizationService>();
builder.Services.AddSingleton<IZooStatisticsService, ZooStatisticsService>();

builder.Services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
builder.Services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
builder.Services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// Использую Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();