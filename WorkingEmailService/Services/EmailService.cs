using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingEmailService.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _config;

        public EmailService(EmailConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(Message message)
        {
            var createdEmail = CreateEmailMessage(message);
            await SendEmailAsync(createdEmail);
        }



        private MimeMessage CreateEmailMessage(Message message)
        {
            var createdEmailMessage = new MimeMessage();
            createdEmailMessage.From.Add(new MailboxAddress(_config.From));
            createdEmailMessage.To.AddRange(message.To);
            createdEmailMessage.Subject = message.Title;
            createdEmailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Body
            };
            return createdEmailMessage;

        }

        private async Task SendEmailAsync(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_config.SmtpServer, _config.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH");
                await client.AuthenticateAsync(_config.Username, _config.Password);
                await client.SendAsync(message);

            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
