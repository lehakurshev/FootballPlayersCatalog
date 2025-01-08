using Application;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using Persistence;
using WebApi.Hubs;

namespace WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddPersistence(Configuration);
        services.AddControllers();
        
        services.AddCors(options =>
        {
            
            options.AddPolicy("AllowSpecificOrigin", builder => 
                builder.WithOrigins(EnvironmentaVariables.FRONTEND_HOST ?? "http://localhost:8080", "http://localhost:3000")

                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed((host) => true));
        });
        services.AddSignalR();

        
        services.AddSwaggerGen(c =>
        {
            var apiVersion = EnvironmentaVariables.API_VERSION ?? "v1";
            c.SwaggerDoc("-", new OpenApiInfo { Title = "My API", Version = apiVersion });
        });
        
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("TelemetryExample"))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter())
            .WithMetrics(metrics => metrics
                .AddPrometheusExporter()
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter());

        
        services.AddLogging(builder =>
        {
            builder.AddOpenTelemetry(options =>
            {
                options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("TelemetryExample"))
                       .AddConsoleExporter();
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/-/swagger.json", "-");
            c.RoutePrefix = string.Empty; 
        });
        
        app.UseOpenTelemetryPrometheusScrapingEndpoint();
        
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseCors("AllowSpecificOrigin");
        app.UseWebSockets();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<FootballPlayerHub>("/players");
        });
    }
}