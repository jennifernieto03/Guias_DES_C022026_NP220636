using Microsoft.EntityFrameworkCore;
using MVCPeliculas.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ---> REGISTRO DEL CONTEXTO DE BASE DE DATOS
builder.Services.AddDbContext<PeliculasDbContext>(item =>
item.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// ---> REGISTRO DEL CONTEXTO DE BASE DE DATOS

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "ejercicio1",
    pattern: "{controller=HelloWorld}/{action=Welcome}/{nombre}/{apellido}/{numVeces?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "saludar",
    pattern: "{controller}/{action}/{nombre}/{id}");

app.Run();
