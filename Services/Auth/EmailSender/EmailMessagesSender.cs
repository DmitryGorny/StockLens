using MailKit.Net.Smtp;
using MimeKit;

namespace StockLens.Services.Auth.EmailSender
{
    public class EmailMessagesSender : IEmailMessagesSender
    {
        private readonly IConfiguration _configuration;

        public EmailMessagesSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string recieverEmail, string reciverNickname, string messageData, string subject)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Stock Lense", _configuration["Email:Sender"]!));
            message.To.Add(new MailboxAddress(reciverNickname, recieverEmail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = messageData
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_configuration["Email:SMTP"], int.Parse(_configuration["Email:Port"]!), MailKit.Security.SecureSocketOptions.Auto);
            await client.AuthenticateAsync(_configuration["Email:Sender"], _configuration["Email:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
