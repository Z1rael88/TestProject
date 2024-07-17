using System.Reflection;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Domain.ValidationOptions;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Web;
using Presentation.Middlewares;
using Presentation.ModelBinders;

namespace Presentation;

public class Program
{
    public static async Task Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthorization();
            builder.Services.AddControllers(options =>
                {
                    options.ModelBinderProviders.Insert(0, new ProjectSearchParamsBinder());
                    options.ModelBinderProviders.Insert(0, new TaskSearchParamsBinder());
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            builder.Services.AddAutoMapper(Assembly.Load("Application"));
            builder.Services.AddEndpointsApiExplorer();
            var configuration = builder.Configuration;
            builder.Services.Configure<ProjectValidationOptions>(configuration.GetSection("ProjectValidation"));
            builder.Services.Configure<TaskValidationOptions>(configuration.GetSection("TaskValidation"));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = builder.Configuration.GetConnectionString(
                    "DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "TestCache";
                options.DefaultSlidingExpiration = TimeSpan.FromSeconds(30);
            });
            builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddScoped<IDataSeeder, DataSeeder>();
            builder.Services.AddScoped<GlobalExceptionHandler>();

            builder.Services.AddValidatorsFromAssembly(Assembly.Load("Application"));

            builder.Services.AddSwaggerGen();
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policyBuilder => policyBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var app = builder.Build();

            await DatabaseInitializer.InitializeAsync(app.Services);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAllOrigins");
            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Stopped program because of exception");
            throw;
        }

        finally
        {
            LogManager.Shutdown();
        }
    }
}