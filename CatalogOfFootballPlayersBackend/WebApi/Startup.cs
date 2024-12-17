using Application;
using Persistence;

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
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });

        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseCors("AllowAll");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}