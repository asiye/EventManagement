using EventManagement.Api.Hubs;
using EventManagement.Application.MappingProfiles;
using EventManagement.Application.Services;
using EventManagement.Domain.Interfaces;
using EventManagement.Infrastructure.Data;
using EventManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace EventManagement.Api;

public static class Startup
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Your existing service configuration
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventService, EventService>();
        services.AddAutoMapper(typeof(EventMappingProfile));
        services.AddSignalR();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                policy =>
                {
                    policy.WithOrigins("http://localhost:3000") // Your client URL
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
        });

        return services;
    }

    public static IApplicationBuilder Configure(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        // Only use static files if `wwwroot` directory exists or if needed
        if (Directory.Exists(Path.Combine(env.ContentRootPath, "wwwroot")))
        {
            app.UseStaticFiles();
        }

        app.UseRouting();
        app.UseCors("AllowSpecificOrigins");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<EventHub>("/eventHub");
        });

        return app;
    }
}
