using Web_gs_ZenFlow.Application.UseCase;
using Web_gs_ZenFlow.Domain.Repositories;
using Web_gs_ZenFlow.Infrastructure.Data;
using Web_gs_ZenFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext com Oracle
var connectionString = builder.Configuration.GetConnectionString("OracleDb");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(connectionString));

// Registrar Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRegistroRepository, RegistroRepository>();

// Registrar Use Cases
builder.Services.AddScoped<IUsuarioUseCase, UsuarioUseCase>();
builder.Services.AddScoped<IRegistroUseCase, RegistroUseCase>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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