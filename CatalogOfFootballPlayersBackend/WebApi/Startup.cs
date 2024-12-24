using Application;
using Microsoft.OpenApi.Models;
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
            options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed((host) => true));
        });
        services.AddSignalR();
        

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });
            // Если у вас есть другие версии, добавьте их здесь
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect("/swagger/index.html");
                return;
            }
            await next();
        });
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
            c.RoutePrefix = string.Empty; 
        });
        
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseWebSockets();
        app.UseCors("AllowSpecificOrigin");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<FootballPlayerHub>("/players");
        });
    }
}