using System;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Tarker.Booking.Api;

public static class DependencyInjectionService
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>{
            options.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "Tarker Booking API", 
                Version = "v1",
                Description = "Tarker Booking API Documentation"
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
            var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));
        });
        return services;
    }
}
