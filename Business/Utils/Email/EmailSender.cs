using System.Net;
using System.Net.Mail;
using System.Text;

namespace Business.Utils.Email;

public class EmailSender(string host, int port, string sender, string name, string pass, bool useSsl, bool isHtml)
{
    public void SendSmtpMail(string toAddress, string subject, string body, string ccAddress = "",
        string bccAddress = "", string[]? attachments = null)
    {
        if (string.IsNullOrEmpty(toAddress))
            throw new Exception("To address is required.");

        List<string> toAddresses = [];
        InitList(toAddresses, toAddress);

        List<string> ccAddresses = [];
        InitList(ccAddresses, ccAddress);

        List<string> bccAddresses = [];
        InitList(bccAddresses, bccAddress);

        SendSmtpMail(toAddresses.ToArray(), subject, body, ccAddresses.ToArray(), bccAddresses.ToArray(), attachments);
    }

    public void SendSmtpMail(string[] toAddresses, string subject, string body, string[]? ccAddresses = null,
        string[]? bccAddresses = null, string[]? attachments = null)
    {
        if (toAddresses == null || toAddresses.Length == 0)
            throw new Exception("To address is required.");

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        using var client = new SmtpClient(host, port);
        if (!string.IsNullOrWhiteSpace(sender) && !string.IsNullOrWhiteSpace(pass))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(sender, pass);
        }

        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = useSsl;

        // Specify the email sender.
        var from = new MailAddress(sender, name, Encoding.UTF8);

        // Specify the message content.
        using var message = new MailMessage();
        message.From = from;
        message.IsBodyHtml = isHtml;

        foreach (var toAddress in toAddresses)
            if (!string.IsNullOrWhiteSpace(toAddress))
                message.To.Add(toAddress);

        if (ccAddresses != null && ccAddresses.Any())
            foreach (var ccAddress in ccAddresses)
                if (!string.IsNullOrWhiteSpace(ccAddress))
                    message.CC.Add(ccAddress);

        if (bccAddresses != null && bccAddresses.Any())
            foreach (var bccAddress in bccAddresses)
                if (!string.IsNullOrWhiteSpace(bccAddress))
                    message.Bcc.Add(bccAddress);

        if (attachments != null && attachments.Any())
            foreach (var attachment in attachments)
                message.Attachments.Add(new Attachment(attachment));

        message.Subject = subject;
        message.SubjectEncoding = Encoding.UTF8;

        message.Body = body;
        message.BodyEncoding = Encoding.UTF8;

        client.Send(message);
    }

    private void InitList(List<string> addresses, string addressStr)
    {
        if (string.IsNullOrWhiteSpace(addressStr)) return;
        if (addressStr.Contains(";"))
        {
            var splittedAddresses = addressStr.Split(";");
            addresses.AddRange(splittedAddresses.Where(splittedAddress => !string.IsNullOrWhiteSpace(splittedAddress)));
        }
        else
        {
            addresses.Add(addressStr);
        }
    }
}