using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tests.User.Api;
using Tests.User.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var dbConnectionString = configuration.GetValue<string>("ConnectionStrings:InMemoryDatabaseString") ?? "";
// Add services to the container.
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddDbContext<DatabaseContext>(options =>{
    options.UseInMemoryDatabase(dbConnectionString);
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
