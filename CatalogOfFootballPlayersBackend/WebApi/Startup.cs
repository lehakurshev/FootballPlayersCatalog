using Application;
using Microsoft.OpenApi.Models;
using Persistence;
using WebApi.Hubs;
using SignalRSwaggerGen;

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
            
            
            // не знаю зачем, но пусть будет
            c.AddSignalRSwaggerGen(_ =>
            {
                _.UseHubXmlCommentsSummaryAsTagDescription =  true;
                _.UseHubXmlCommentsSummaryAsTag = true;
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
        
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseCors("AllowSpecificOrigin");
        app.UseWebSockets();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<FootballPlayerHub>("/players");
            endpoints.MapHub<TeamHub>("/teams");
        });
    }
}