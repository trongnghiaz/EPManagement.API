using Application.Common.DelegateHandler;
using Application.Common.Interface;
using Domain.Helper;
using Domain.ValueObject;
using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Infrastructure.Authentications;
using Infrastructure.AWS.SQS;
using Infrastructure.Persistence.Data;
using Infrastructure.Persistence.JwtGenerator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<SQSSettings>(configuration.GetSection("SQS"));
            
            services.AddDbContext<ManageWriteDbContext>(options => 
                options.UseMySql(configuration.GetConnectionString("DefaultConnect"),
                 new MySqlServerVersion(new Version(8, 0, 0)), option => option.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)))
                    .AddScoped<IManageWriteDbContext, ManageWriteDbContext>();
                
            services.AddDbContext<ManageReadDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnect"), new MySqlServerVersion(new Version(8, 0, 0))))
                .AddScoped<IManageReadDbContext, ManageReadDbContext>();

            services.AddScoped<ISQSService, SQSService>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>(); 
            
            services.AddTransient<TracingHttpMessageHandler>();
        }
        public static void AddSerilog(this IHostBuilder host, IConfiguration configuration)
        {
            host.UseSerilog((context, config) =>
                config.ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)                    
                    .Enrich.WithMachineName()
                    .Enrich.WithProperty(StringConst.TraceIdentifier, () => TraceIdLogging.TraceId)
                    //.WriteTo.Elasticsearch(new[] { new Uri(configuration["Elasticsearch:Uri"]!) }, opts =>
                    //{
                    //    opts.MinimumLevel = Serilog.Events.LogEventLevel.Error;                        
                    //    opts.DataStream = new DataStreamName("logs-dotnet-default");
                    //    opts.BootstrapMethod = BootstrapMethod.Failure;
                    //    opts.ConfigureChannel = channelOpts =>
                    //    {
                    //        channelOpts.BufferOptions = new BufferOptions
                    //        {
                    //            ExportMaxConcurrency = 10,
                    //        };
                    //    };
                    //}, transport =>
                    //{
                    //    transport.Authentication(new BasicAuthentication
                    //    (
                    //        configuration["Elasticsearch:username"]!,
                    //        configuration["Elasticsearch:password"]!)
                    //    );
                    //}
                //)
            );
        }
        public static void AddAuthen(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = config.Issuer,
                    ValidAudience = config.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key!))
                };
            });
            services.AddAuthorization();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddScoped<IPermissionService, PermissionService>();            
        }
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
        });
            });
        }

    }
}
