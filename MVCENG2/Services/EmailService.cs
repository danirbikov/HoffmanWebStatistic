
using MimeKit;
using MailKit.Net.Smtp;

namespace HoffmanWebstatistic.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "eolhofmann@kamaz.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("s0019.kamaz.ru", 587, true);
                await client.AuthenticateAsync("eolhofmann@kamaz.ru", "%J#6Gi#v!EBVprbAwmA6");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}