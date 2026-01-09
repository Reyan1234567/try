using MailKit.Net.Smtp;
using MimeKit;
using MiniRedditCloneApi.Data.Configurations;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;

namespace MiniRedditCloneApi.Services.Implementations
{
    public class EmailService(EmailConfiguration emailConfig) : IEmailService
    {
        private readonly EmailConfiguration _emailConfig = emailConfig;

        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            await Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("NerdHerd", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private async Task Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.Username, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:23 PM
# Update Fri, Jan  9, 2026  9:26:06 PM
# Update Fri, Jan  9, 2026  9:34:56 PM
