using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Clinicamedica.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly string _fromAddress;

        public EmailSender(string host, int port, string username, string password, string fromAddress)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _fromAddress = fromAddress;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var client = new SmtpClient
                {
                    Host = _host,
                    Port = _port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_username, _password)
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromAddress),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (puedes registrar el error o hacer algo al respecto)
                // Ejemplo: _logger.LogError(ex, "Error sending email.");
                throw new InvalidOperationException("Error sending email.", ex);
            }
        }
    }
}
