using Excembly_vAlpha.Data;
using Excembly_vAlpha.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuraci�n de la base de datos
builder.Services.AddDbContext<ExcemblyDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddHttpContextAccessor();

// Inyecci�n de servicios
builder.Services.AddScoped<LoginService, LoginService>();
builder.Services.AddScoped<EmpresaService, EmpresaService>(); // Agrega esta l�nea para el EmpresaService

// Configuraci�n de autenticaci�n de cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Ruta de inicio de sesi�n
        options.LogoutPath = "/Logout"; // Ruta de cierre de sesi�n
        options.AccessDeniedPath = "/Home/AccessDenied"; // Ruta para acceso denegado
        options.SlidingExpiration = true; // Expiraci�n deslizante
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
