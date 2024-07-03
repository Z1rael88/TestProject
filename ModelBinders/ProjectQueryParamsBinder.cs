using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.ModelBinders;

public class ProjectQueryParamsModelBinder: IModelBinder
{
    
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var valueProvider = bindingContext.ValueProvider;
        var name = valueProvider.GetValue("name").FirstValue;
        var description = valueProvider.GetValue("description").FirstValue;

        var model = new ProjectDto
        {
            Name = name,
            Description = description
        };
        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}

public class ProjectQueryParamsBinder : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(ProjectDto))
        {
            return new ProjectQueryParamsModelBinder();
        }

        return null;
    }
}