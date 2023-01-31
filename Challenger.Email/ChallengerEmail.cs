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

        /// <summary>
        /// Creates an instance of EmailHepler with gmail as smtp client
        /// </summary>
        public ChallengerEmail(
            IOptions<EmailSettings> settings,
            EmailBuilder emailBuilder)
        {
            _settings = settings.Value;
            _emailBuilder = emailBuilder;
        }

        public async Task SendEmailAsync(string email, string subjectType, string emailType)
        {
            await _emailBuilder.Configure();
            var client = GetSmtpClient();
            var message = GetFormattedMessage(email, subjectType, emailType);

            try
            {
                client.Send(message);
            }
            catch (System.Exception ex)
            {
                var mes = ex.ToString();
            }            
        }

        /// <summary>
        /// Set one of the formated messages with callbackUrl for register/resetpassword link. Check Admin/EmailFormatter.cshtml
        /// </summary>
        /// <param name="userMail">Users email</param>
        /// <param name="callbackUrl">Link for account management</param>
        /// <param name="emailType">Formated email type</param>
        public MailMessage GetFormattedMessage(string userMail, string subjectType, string emailType)
        {
            var m = new MailMessage(
                    new MailAddress(_settings.Address, "Challenger - no reply automated email."),
                    new MailAddress(userMail));

            m.Subject = _emailBuilder.BuildEmailSubject(subjectType);
            m.Body = _emailBuilder.BuildEmailMessage(emailType, new System.Collections.Generic.Dictionary<string, object>());
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