using Core.Mappers;
using Core.Services;
using Core.Services.Hash;
using DataAccess.DatabaseContext;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Web.BasicAuthentication;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureServices(services =>
{
    services.AddDbContext<Context>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
});

builder.Services.AddTransient<IUserService, UserServiceWithAuthorization>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IRepository<User>, UserRepository>();
builder.Services.AddTransient<IHashService, HashService>();
builder.Services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddAuthentication("Basic")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", options => {});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();