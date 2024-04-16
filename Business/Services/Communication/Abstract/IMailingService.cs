using Core.Services;
using Core.Services.Result;

namespace Business.Services.Communication.Abstract;

public interface IMailingService : IService
{
    public ServiceObjectResult<bool> SendSmtp(string to, string subject, string body);
}