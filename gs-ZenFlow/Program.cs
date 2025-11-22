using gs_ZenFlow.Application.UseCase;
using gs_ZenFlow.Domain.Repositories;
using gs_ZenFlow.Infrastructure.Data;
using gs_ZenFlow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Linq;
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

        // Configurar Controllers com ProblemDetails
        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                // Configurar ProblemDetails para validação de modelo
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                        Title = "Erro de validação",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Um ou mais erros de validação ocorreram.",
                        Instance = context.HttpContext.Request.Path
                    };

                    // Adicionar erros de validação ao ProblemDetails
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    problemDetails.Extensions.Add("errors", errors);

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });
        
        // Configurar ProblemDetails
        builder.Services.AddProblemDetails();

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
        
        // Configurar tratamento global de exceções com ProblemDetails
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/problem+json";

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Erro interno do servidor",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = app.Environment.IsDevelopment() ? exception?.Message : "Ocorreu um erro ao processar sua solicitação.",
                    Instance = context.Request.Path
                };

                if (app.Environment.IsDevelopment() && exception != null)
                {
                    problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
                    problemDetails.Extensions.Add("stackTrace", exception.StackTrace);
                }

                await context.Response.WriteAsJsonAsync(problemDetails);
            });
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GS - ZenFlow API v1");
                c.RoutePrefix = string.Empty; // Swagger na raiz
            });
        }

        // Aplicar redirecionamento HTTPS apenas se houver porta HTTPS configurada
        // Isso evita o aviso quando a aplicação roda apenas em HTTP
        var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "";
        var httpsPort = Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT");
        var hasHttps = !string.IsNullOrEmpty(httpsPort) || urls.Contains("https://", StringComparison.OrdinalIgnoreCase);
        
        if (hasHttps)
        {
            app.UseHttpsRedirection();
        }

        app.UseCors();

        app.UseAuthorization();

        // Mapear Controllers
        app.MapControllers();

        app.Run();
    }
}