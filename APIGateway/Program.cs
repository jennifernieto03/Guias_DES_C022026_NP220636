using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Definir la clave que conectará este esquema con ocelot.json
var authenticationProviderKey = "IdentityApiKey";

// 2. Configurar la validación del Token JWT
builder.Services.AddAuthentication()
    .AddJwtBearer(authenticationProviderKey, options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("TuClaveSecretaMuySeguraYLarga123!")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();

var app = builder.Build();

// 3. Habilitar el middleware de autenticación ANTES de ejecutar Ocelot
app.UseAuthentication();
app.UseOcelot().Wait();

app.Run();
