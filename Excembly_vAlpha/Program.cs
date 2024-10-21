using Excembly_vAlpha.Data;
using Excembly_vAlpha.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuración de la base de datos
builder.Services.AddDbContext<ExcemblyDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Inyección servicios "tal vez haga mas en un futuro"
builder.Services.AddScoped<ServicioService, ServicioService>();
builder.Services.AddScoped<PlanService, PlanService>();
builder.Services.AddScoped<UsuarioService, UsuarioService>();
builder.Services.AddScoped<CitaService, CitaService>();
builder.Services.AddScoped<TrabajoService, TrabajoService>();
builder.Services.AddScoped<PagoService, PagoService>();
builder.Services.AddScoped<HistorialService, HistorialService>();
builder.Services.AddScoped<DispositivoService, DispositivoService>();
builder.Services.AddScoped<TarjetaService, TarjetaService>();
builder.Services.AddScoped<BitacoraService, BitacoraService>();
builder.Services.AddScoped<AcercaDeService, AcercaDeService>();
builder.Services.AddScoped<PoliticaPrivacidadService, PoliticaPrivacidadService>();
builder.Services.AddScoped<TecnicoService, TecnicoService>();
builder.Services.AddScoped<CambioContratacionService, CambioContratacionService>();



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
