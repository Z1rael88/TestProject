using Domain.SearchParams;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.ModelBinders;

public class ProjectSearchParamsModelBinder : IModelBinder
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
        var startDate = valueProvider.GetValue("StartDate").FirstValue;
        var startDateFrom = valueProvider.GetValue("StartDateFrom").FirstValue;
        var startDateTo = valueProvider.GetValue("StartDateTo").FirstValue;
        try
        {
            var model = new ProjectSearchParams
            {
                Name = name,
                Description = description,
                StartDate = GetDate(startDate),
                StartDateFrom = GetDate(startDateFrom),
                StartDateTo = GetDate(startDateTo)
            };
            bindingContext.Result = ModelBindingResult.Success(model);
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
        return Task.CompletedTask;
    }

    private DateOnly? GetDate(string? date)
    {
        return date != null ? DateOnly.Parse(date) : null;
    }
}

public class ProjectSearchParamsBinder : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return context.Metadata.ModelType == typeof(ProjectSearchParams) ? new ProjectSearchParamsModelBinder() : null;
    }
}