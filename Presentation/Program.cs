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
    public static void Main(string[] args)
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
                        NamingStrategy = new CamelCaseNamingStrategy(),
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

            builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<GlobalExceptionHandler>();
            builder.Services.AddMemoryCache();

            builder.Services.AddValidatorsFromAssembly(Assembly.Load("Application"));

            builder.Services.AddSwaggerGen();
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
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