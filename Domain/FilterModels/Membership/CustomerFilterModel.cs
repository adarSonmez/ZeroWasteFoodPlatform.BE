using Core.Services;
using Domain.Entities.Membership;

namespace Domain.FilterModels.Membership;

public class CustomerFilterModel : IServiceFilterModel<Customer>
{
    public string? NameQuery { get; set; } = null;
    public bool? EmailVerified { get; set; } = null;
    public bool? PhoneNumberVerified { get; set; } = null;
}