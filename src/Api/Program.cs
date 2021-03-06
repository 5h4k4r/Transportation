using System.Reflection;
using System.Text.Json.Serialization;
using Api.Extensions;
using Api.Swagger;
using AutoMapper;
using Core.Converters;
using Core.Extensions;
using Core.Helpers;
using Infra.Extensions;
using Infra.Mapper;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
// Add services to the container.
services
    .AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());

        x.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Default;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services
    .ConfigureDatabase(config)
    .AddEndpointsApiExplorer()
    .ConfigureSwaggerGenerator(config)
    .ConfigureRepositoryWrapper()
    .AddScoped<UserAuthContext>()
    .AddAuthentication(x =>
    {
        x.DefaultChallengeScheme = "Basic";
        x.DefaultAuthenticateScheme = "Basic";
    })
    .AddScheme<UserAuthOptions, UserAuthHandler>("Basic", null);

var app = builder.Build();

app.Services.ValidateOptions();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
