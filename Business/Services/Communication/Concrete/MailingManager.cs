using Business.Constants.Messages.Services.Communication;
using Business.Services.Communication.Abstract;
using Business.Utils.Email;
using Core.Services.Result;
using Core.Utils.IoC;
using Core.Utils.Rules;
using Microsoft.Extensions.Configuration;

namespace Business.Services.Communication.Concrete;

public class MailingManager : IMailingService
{
    public ServiceObjectResult<bool> SendSmtp(string to, string subject, string body)
    {
        var result = new ServiceObjectResult<bool>();

        try
        {
            BusinessRules.Run(
                ("MAIL-721753", BusinessRules.CheckEmail(to)),
                ("MAIL-948634",
                    BusinessRules.CheckStringNullOrEmpty(subject, MailingServiceMessages.SubjectCannotBeEmpty)),
                ("MAIL-701975",
                    BusinessRules.CheckStringNullOrEmpty(body, MailingServiceMessages.BodyCannotBeEmpty))
            );

            var configuration = ServiceTool.GetService<IConfiguration>()!;

            var from = configuration["MailingSettings:From"]!;
            var password = configuration["MailingSettings:Password"]!;
            var host = configuration["MailingSettings:Host"]!;
            var port = int.Parse(configuration["MailingSettings:Port"]!);
            var name = configuration["MailingSettings:Name"]!;
            var enableSsl = bool.Parse(configuration["MailingSettings:EnableSsl"]!);

            var emailSender = new EmailSender(host, port, from, name, password, enableSsl, false);

            emailSender.SendSmtpMail(to, subject, body, MailingServiceMessages.SentSmtp);
        }
        catch (Exception ex)
        {
            result.Fail("MAIL-392388", ex.Message);
        }

        return result;
    }
}