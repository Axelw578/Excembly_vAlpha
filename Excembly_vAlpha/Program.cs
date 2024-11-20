using Excembly_vAlpha.Data;
using Excembly_vAlpha.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ExcemblyDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")),
        mysqlOptions => mysqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
    )
);


builder.Services.AddHttpContextAccessor();

// Inyección de servicios
builder.Services.AddScoped<LoginService, LoginService>();
builder.Services.AddScoped<EmpresaService, EmpresaService>(); 
builder.Services.AddScoped<PlanesService, PlanesService>();
builder.Services.AddScoped<ServiciosService, ServiciosService>();
builder.Services.AddScoped<TecnicosService, TecnicosService>();
builder.Services.AddScoped<DireccionService, DireccionService>();
builder.Services.AddScoped<TarjetaService, TarjetaService>();
builder.Services.AddScoped<PoliticaService, PoliticaService>();
builder.Services.AddScoped<ContratacionService, ContratacionService>();
builder.Services.AddScoped<ServicioAdicionalService, ServicioAdicionalService>();
builder.Services.AddScoped<CitasService, CitasService>();
builder.Services.AddScoped<CuentaService, CuentaService>();


// Configuración de autenticación de cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Ruta de inicio de sesión
        options.LogoutPath = "/Logout"; // Ruta de cierre de sesión
        options.AccessDeniedPath = "/Home/AccessDenied"; // Ruta para acceso denegado
        options.SlidingExpiration = true; // Expiración deslizante
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
