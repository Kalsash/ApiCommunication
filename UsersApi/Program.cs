using Microsoft.EntityFrameworkCore;
using UsersApi.Models;
using UsersApiData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var configuration = builder.Configuration;

builder.Services.AddDbContext<UserContext>(options =>
	options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();