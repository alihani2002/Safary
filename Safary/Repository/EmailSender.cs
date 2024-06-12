using Domain.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Safary.Repository
{
    public class EmailSender : IEmailSender
    {
        private readonly MailSettings mailSettings;
        private readonly IWebHostEnvironment webHostEnvironment;
        public EmailSender(IOptions<MailSettings> _mailSettings, IWebHostEnvironment _webHostEnvironment)
        {
            mailSettings = _mailSettings.Value;
            webHostEnvironment = _webHostEnvironment;
        }
        public async Task SendEmailAsync(string email, string subject, string content)
        {
            if (string.IsNullOrEmpty(email))
                return;

            MailMessage mailMessage = new()
            {
                From = new MailAddress(mailSettings.Email!, mailSettings.DisplayName),
                Body = content,
                Subject = subject,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            var smtpClient = new SmtpClient(mailSettings.Host)
            {
                Port = mailSettings.Port,
                Credentials = new NetworkCredential(mailSettings.Email, mailSettings.Password),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(mailMessage);
            smtpClient.Dispose();
        }
    }
}
