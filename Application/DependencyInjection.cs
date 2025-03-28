using Application.Common.Behaviour;
using Application.Common.Interface;
using Application.Common.Middleware;
using Application.Common.Service;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication (this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddFluentValidation(options =>
            {
                options.AutomaticValidationEnabled = true;
                options.DisableDataAnnotationsValidation = true;
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddTransient<IRandomPasswordString, RandomPasswordString>();
        }
        public static void AddApplication (this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMiddleware<TracingMiddleware>();
        }    
    }
}
