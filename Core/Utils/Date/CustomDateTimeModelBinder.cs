using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Core.Utils.Date;

/// <summary>
/// Custom model binder for DateTime type.
/// </summary>
public class CustomDateTimeModelBinder : IModelBinder
{
    /// <summary>
    /// Binds the model asynchronously.
    /// </summary>
    /// <param name="bindingContext">The model binding context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var displayFormatString = bindingContext.ModelMetadata.DisplayFormatString;
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        // If display format or value is empty, return completed task
        if (string.IsNullOrEmpty(displayFormatString) || string.IsNullOrEmpty(value.FirstValue))
            return Task.CompletedTask;

        // Remove unnecessary characters from display format string
        displayFormatString = displayFormatString.Replace("{0:", "").Replace("}", "");

        DateTime date;
        try
        {
            // Parse the value using the specified display format
            date = DateTime.ParseExact(value.FirstValue, displayFormatString, CultureInfo.InvariantCulture);
        }
        catch
        {
            // If parsing fails, parse the value using the default format
            date = DateTime.Parse(value.FirstValue, CultureInfo.InvariantCulture);
        }

        // Set the binding result to the parsed date
        bindingContext.Result = ModelBindingResult.Success(date);
        return Task.CompletedTask;
    }
}