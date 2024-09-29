using Core.Extensions;

namespace Core.Services.Result;

/// <summary>
/// Represents a collection result of a service operation.
/// </summary>
/// <typeparam name="T">The type of data in the collection.</typeparam>
public class ServiceCollectionResult<T> : ServiceResult
{
    #region Collection Properties

    public IEnumerable<T> Data { get; private set; } = [];

    public int CurrentPage { get; private set; }

    public int PageSize { get; private set; }

    public int TotalItemCount { get; private set; }

    public int TotalPageCount { get; private set; }

    public int? NextPage { get; private set; }

    #endregion Collection Properties

    #region Set Data Overloads

    /// <summary>
    /// Sets the data for the collection result.
    /// </summary>
    /// <param name="data">The data to be set.</param>
    /// <param name="successMessage">The success message to be displayed.</param>
    /// <param name="page">The current page number.</param>
    /// <param name="pageSize">The maximum number of items per page.</param>
    public void SetData(IEnumerable<T> data, string successMessage, int page = 1, int pageSize = int.MaxValue)
    {
        TotalItemCount = data.Count();
        TotalPageCount = (int)Math.Ceiling((double)TotalItemCount / pageSize);
        CurrentPage = page;
        PageSize = pageSize;

        if (PageSize < int.MaxValue && TotalItemCount > PageSize && CurrentPage < TotalPageCount)
            NextPage = CurrentPage + 1;
        else
            NextPage = null;

        SetData(data.Paginate(page, pageSize), successMessage);
    }

    private void SetData(IEnumerable<T>? data, string successMessage)
    {
        if (data == null || !data.Any())
        {
            Warning(ServiceResultConstants.NoItemsFound);
        }
        else
        {
            Data = data;
            HasData = true;
            Success(successMessage);
        }

        DataType = typeof(T).Name;
        IsList = true;
    }

    #endregion Set Data Overloads
}