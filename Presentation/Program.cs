using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Presentation.Middlewares;
using Presentation.ModelBinders;
using WebApplication1.Interfaces;

namespace Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
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

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
       
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<GlobalExceptionHandler>();
        
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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
}