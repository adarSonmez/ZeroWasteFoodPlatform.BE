using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Core.Utils.Date;

public class CustomDateTimeModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var displayFormatString = bindingContext.ModelMetadata.DisplayFormatString;
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (string.IsNullOrEmpty(displayFormatString) || string.IsNullOrEmpty(value.FirstValue))
            return Task.CompletedTask;
        displayFormatString = displayFormatString.Replace("{0:", "").Replace("}", "");
        DateTime date;
        try
        {
            date = DateTime.ParseExact(value.FirstValue, displayFormatString, CultureInfo.InvariantCulture);
        }
        catch
        {
            date = DateTime.Parse(value.FirstValue, CultureInfo.InvariantCulture);
        }

        bindingContext.Result = ModelBindingResult.Success(date);
        return Task.CompletedTask;
    }
}