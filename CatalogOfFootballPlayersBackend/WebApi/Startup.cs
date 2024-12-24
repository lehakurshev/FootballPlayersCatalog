using Application;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;
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
        
        // политику надо будет поменять
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed((host) => true));
        });
        services.AddSignalR();

        
        services.AddVersionedApiExplorer(options =>
            options.GroupNameFormat = "'v'VVV");
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
            ConfigureSwaggerOptions>();
        services.AddSwaggerGen();
        services.AddApiVersioning();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
        IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                config.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                config.RoutePrefix = string.Empty;
            }
        });
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseWebSockets();
        app.UseCors("AllowSpecificOrigin");
        app.UseApiVersioning();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<FootballPlayerHub>("/players");
        });
    }
}