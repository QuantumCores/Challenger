using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Challenger.Email
{
    public class ChallengerEmail : IEmailSender
    {
        private readonly EmailSettings _settings;
        private readonly EmailBuilder _emailBuilder;

        public ChallengerEmail(
            IOptions<EmailSettings> settings,
            EmailBuilder emailBuilder)
        {
            _settings = settings.Value;
            _emailBuilder = emailBuilder;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await _emailBuilder.Configure();
            var client = GetSmtpClient();
            var message = PrepareMessage(email, subject, htmlMessage);

            try
            {
                client.Send(message);
            }
            catch (System.Exception ex)
            {
                var mes = ex.ToString();
            }            
        }

        private MailMessage PrepareMessage(string userMail, string subject, string htmlMessage)
        {
            var m = new MailMessage(
                    new MailAddress(_settings.Address, "Challenger - no reply automated email."),
                    new MailAddress(userMail));

            m.Subject = subject;
            m.Body = htmlMessage;
            m.IsBodyHtml = true;

            return m;
        }

        private SmtpClient GetSmtpClient()
        {
            var smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(_settings.Address, _settings.GetPassword());
            smtpClient.Host = _settings.Host;
            smtpClient.Port = _settings.Port;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            return smtpClient;
        }
    }
}