namespace StockLens.Services.Auth.EmailSender
{
    public interface IEmailMessagesSender
    {
        public Task SendEmailAsync(string recieverEmail, string reciverNickname, string messageData, string subject);
    }
}
