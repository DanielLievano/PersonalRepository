using Authorization.Aplication.Interfaces;
using Authorization.Aplication.Services;
using Authorization.Domain.Interfaces;
using Authorization.Infrastructure.Context;
using Authorization.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AuthorizationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthorizationDbConnection")));
//add dependencies
builder.Services.AddTransient<IAuthorizationService,AuthorizationService>();
builder.Services.AddTransient<IAuthorizationRepository,AuthorizationRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
