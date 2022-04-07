using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Helpers;
using Transportation.Api.Model;
using Transportation.Api.Repositories;
using Transportation.Api.Services;
using Transportation.Api.Settings;
using Transportation.Api.Swagger;

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
services.AddEndpointsApiExplorer();
services.ConfigureSwaggerGenerator(config);

services
    .AddScoped<IAuthRepository, AuthRepository>()
    .AddScoped<UserAuthContext>()
    .AddAuthentication(x =>
    {
        x.DefaultChallengeScheme = "Basic";
        x.DefaultAuthenticateScheme = "Basic";
    })
    .AddScheme<UserAuthOptions, GatewayAuthHandler>("Basic", null);


var configurationSection = config.GetSection(DatabaseOptions.Config).GetChildren().FirstOrDefault();


if (configurationSection is not null)
    services.AddDbContext<transportationContext>(x => x.UseMySql(configurationSection.Value, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.15-mariadb")));

var app = builder.Build();
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
