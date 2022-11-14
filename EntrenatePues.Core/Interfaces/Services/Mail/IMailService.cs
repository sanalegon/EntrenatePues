using EntrenatePues.Core.Common.Responses;

namespace EntrenatePues.Core.Interfaces.Services.Mail
{
    public interface IMailService
    {
        ResponseCode SendMail(string to, string subject, string body);
    }
}
