using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Payroll.PaygridApi.Helpers;
using Transportation.Api;
using Transportation.Api.Helpers;
using Transportation.Api.Model;
using Transportation.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

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
services.AddSwaggerGen();

services
    .AddScoped<UserAuthContext>()
    .AddAuthentication(x =>
    {
        x.DefaultChallengeScheme = "Basic";
        x.DefaultAuthenticateScheme = "Basic";
    })
    .AddScheme<UserAuthOptions, GatewayAuthHandler>("Basic", null);

// services.AddSingleton<GatewayAuthHandler>();
services.AddDbContext<transportationContext>(x => x.UseMySql(builder.Configuration["MariaDb:ConnectionString"], Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.15-mariadb")));


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
