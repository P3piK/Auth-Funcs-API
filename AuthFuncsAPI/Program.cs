using AuthFuncsAPI.Extensions;
using AuthFuncsAPI.Middleware;
using AuthFuncsCore.Config;
using AuthFuncsRepository;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddDbContext<AFContext>();

// service extensions 
builder.Services.RegisterConfiguration(builder.Configuration);
builder.Services.RegisterServices();

builder.Services.ConfigureCors();
var serviceProvider = builder.Services.BuildServiceProvider();
builder.Services.ConfigureJwtAuthentication(serviceProvider.GetService<AuthenticationConfig>());

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

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
