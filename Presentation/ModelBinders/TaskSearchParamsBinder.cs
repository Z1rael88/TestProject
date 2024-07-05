using Domain.Enums;
using Domain.SearchParams;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.ModelBinders;

public class TaskSearchParamsModelBinder : IModelBinder
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
        var status = valueProvider.GetValue("status").FirstValue;
        var model = new TaskSearchParams
        {
            Name = name,
            Description = description,
            Status = status != null ? Enum.Parse<Status>(status) : null
        };
        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}

public class TaskSearchParamsBinder : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return context.Metadata.ModelType == typeof(TaskSearchParams) ? new TaskSearchParamsModelBinder() : null;
    }
}