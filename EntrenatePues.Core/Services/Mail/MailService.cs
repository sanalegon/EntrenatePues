using EntrenatePues.Core.Common.Responses;
using EntrenatePues.Core.Interfaces.Services.Mail;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Net;

namespace EntrenatePues.Core.Services.Mail
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ResponseCode SendMail(string to, string subject, string body)
        {
            try
            {
                string from = _configuration["Mail:From"];
                string smtp = _configuration["Mail:Smtp"];
                string port = _configuration["Mail:Port"];
                string password = _configuration["Mail:Password"];

                MimeMessage message = new();
                message.From.Add(new MailboxAddress("Entrenate Pues", from));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;
                BodyBuilder bodyBuilder = new()
                {
                    HtmlBody = body
                };
                message.Body = bodyBuilder.ToMessageBody();

                using (SmtpClient client = new())
                {
                    client.Connect(smtp, int.Parse(port), false);
                    client.Authenticate(from, password);
                    _ = client.Send(message);
                    client.Disconnect(true);
                }

                return new ResponseCode(HttpStatusCode.OK, "Email send to: " + to);

            }
            catch (Exception ex)
            {
                return new ResponseCode(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
