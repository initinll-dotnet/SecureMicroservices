using Microsoft.IdentityModel.Tokens;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

var authenticationProviderKey = "IdentityApiKey";

builder.Configuration.AddJsonFile("ocelot.json");

builder.Services
    .AddAuthentication()
    .AddJwtBearer(authenticationProviderKey, x =>
    {
        x.Authority = "http://localhost:5005"; // IDENTITY SERVER URL
        //x.RequireHttpsMetadata = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddOcelot();

var app = builder.Build();

app.UseRouting();

app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();

app.Run();
