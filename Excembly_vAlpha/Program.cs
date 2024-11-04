using Excembly_vAlpha.Data;
using Excembly_vAlpha.Services;
using Microsoft.AspNetCore.Authentication.Cookies; // Aseg�rate de importar este espacio de nombres
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

// Registrar IHttpContextAccessor para permitir la inyecci�n en LoginService
builder.Services.AddHttpContextAccessor();

// Inyecci�n servicios
builder.Services.AddScoped<LoginService, LoginService>();

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

// Agrega el middleware de autenticaci�n
app.UseAuthentication(); // Aseg�rate de agregar esta l�nea
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
