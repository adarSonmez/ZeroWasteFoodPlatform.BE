using System.ComponentModel.DataAnnotations;
using Core.Services;
using Core.Utils.Date;
using Domain.Entities.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace Domain.FilterModels.Analytics;

public sealed class ReportFilterModel : IServiceFilterModel<Report>
{
    public string? ReportNameQuery { get; set; } = null;
    public string? ProductNameQuery { get; set; } = null;
    public string? ManufacturerQuery { get; set; } = null;
    public string? LocationQuery { get; set; } = null;

    [ModelBinder(BinderType = typeof(CustomDateTimeModelBinder))]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime? StartDateLow { get; set; } = null;

    [ModelBinder(BinderType = typeof(CustomDateTimeModelBinder))]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime? StartDateHigh { get; set; } = null;

    [ModelBinder(BinderType = typeof(CustomDateTimeModelBinder))]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime? EndDateLow { get; set; } = null;

    [ModelBinder(BinderType = typeof(CustomDateTimeModelBinder))]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime? EndDateHigh { get; set; } = null;

    public int? SuppliedAmountLow { get; set; } = null;
    public int? SuppliedAmountHigh { get; set; } = null;
    public int? SoldAmountLow { get; set; } = null;
    public int? SoldAmountHigh { get; set; } = null;
}