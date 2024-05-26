using Microsoft.EntityFrameworkCore;
using UsersApiData;
using UsersApi.Services;
using Microsoft.AspNetCore.Identity;
using UsersApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();

var configuration = builder.Configuration;

//builder.Services.AddDbContext<UserContext>(options =>
//	options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
builder.Services.AddAuthorization();
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserContext>();

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGrpcService<UserApiService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();