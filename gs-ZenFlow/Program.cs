using gs_ZenFlow.Application.UseCase;
using gs_ZenFlow.Domain.Repositories;
using gs_ZenFlow.Infrastructure.Data;
using gs_ZenFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace gs_ZenFlow;

public class Program
{
    public static void Main(string[] args)
    {
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

        // Configurar Controllers
        builder.Services.AddControllers();

        // Configurar Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = builder.Configuration["Swagger:Title"] ?? "GS - ZenFlow",
                Description = builder.Configuration["Swagger:Description"] ?? "Projeto do Global Solution",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = builder.Configuration["Swagger:Contact:Name"] ?? "Amanda Galdino",
                    Email = builder.Configuration["Swagger:Contact:Email"] ?? "RM560066@fiap.com.br"
                }
            });

            // Habilitar anotações do Swagger
            c.EnableAnnotations();

            // Incluir comentários XML (se houver)
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
        });

        // Configurar CORS (se necessário)
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        // Configurar Authorization (se necessário no futuro)
        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GS - ZenFlow API v1");
                c.RoutePrefix = string.Empty; // Swagger na raiz
            });
        }

        app.UseHttpsRedirection();

        app.UseCors();

        app.UseAuthorization();

        // Mapear Controllers
        app.MapControllers();

        app.Run();
    }
}