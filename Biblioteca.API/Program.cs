using Biblioteca.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Swagger UI
builder.Services.AddSwaggerGen(); // NUEVO
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Interfaz Swagger UI
    app.UseSwagger(); // NUEVO
    app.UseSwaggerUI(); // NUEVO
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
