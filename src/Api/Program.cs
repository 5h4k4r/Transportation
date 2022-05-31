using System.Text.Json.Serialization;
using Api.Extensions;
using Api.Middlewares;
using Api.Swagger;
using Core.Converters;
using Core.Helpers;
using Infra.Authentication;
using Infra.Extensions;

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
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        x.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Default;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services
    .ConfigureDatabase(config)
    .AddEndpointsApiExplorer()
    .ConfigureSwaggerGenerator(config)
    .ConfigureRepositoryWrapper().AddTransient<ErrorHandlingMiddleware>()
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

if (app.Environment.IsProduction()) app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();