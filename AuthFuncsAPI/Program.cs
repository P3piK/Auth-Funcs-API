using AuthFuncsAPI.Extensions;
using AuthFuncsAPI.Middleware;
using AuthFuncsCore.Config;
using AuthFuncsRepository;
using AuthFuncsWorkerService;
using FluentValidation.AspNetCore;
using Microsoft.Azure.ServiceBus.Core;
using NLog.Web;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri("https://authfuncsvault.vault.azure.net/");
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Add services to the container.
builder.Services.AddHostedService<EmailWorker>();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AFContext>();

// service extensions 
builder.Services.RegisterConfiguration(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.RegisterMiddleware();
builder.Services.RegisterMiscs();

builder.Services.ConfigureCors();
var serviceProvider = builder.Services.BuildServiceProvider();
builder.Services.ConfigureJwtAuthentication(serviceProvider.GetService<AuthenticationConfig>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// NLog logger
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<RequestTimerMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
