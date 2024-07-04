using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Exceptions;

namespace WebApplication1.ModelBinders;

public class ProjectQueryParamsModelBinder : IModelBinder
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
            Name = name!,
            Description = description!
        };
        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}

public class ProjectQueryParamsBinder : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return context.Metadata.ModelType == typeof(ProjectDto)
            ? new ProjectQueryParamsModelBinder()
            : throw new NotFoundException();
    }
}