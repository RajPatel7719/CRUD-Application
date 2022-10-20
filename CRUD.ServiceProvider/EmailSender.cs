using CRUD.Models;
using CRUD.ServiceProvider.IService;
using System.Net;
using System.Net.Mail;

namespace CRUD.ServiceProvider
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MailMessage CreateEmailMessage(Message message)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailConfig.From);
            mailMessage.To.Add(message.To);

            mailMessage.Subject = "Error Log File";
            mailMessage.Body = message.Content;

            if (message.Attachments != null && message.Attachments.Any())
            {
                var attachment = new Attachment(message.Attachments);
                mailMessage.Attachments.Add(attachment);
            }
            return mailMessage;
        }

        private void Send(MailMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Host = _emailConfig.SmtpServer;
                    client.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(_emailConfig.UserName, _emailConfig.Password);
                    client.Credentials = NetworkCred;
                    client.Port = _emailConfig.Port;
                    client.Send(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
    }
}
