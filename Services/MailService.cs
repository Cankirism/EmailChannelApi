using System.Net;
using System.Net.Mail;

namespace EmailChannelApi.Services;
public class MailService : IMailService
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _username;
    private readonly string _password;
    private readonly string _toAddress;
        public MailService(IConfiguration config)
        {
            _smtpHost = config.GetSection("MailInfo").GetSection("SmtpHost").Value;
            _smtpPort = int.Parse(config.GetSection("MailInfo").GetSection("SmtpPort").Value);
            _username = config.GetSection("MailInfo").GetSection("Username").Value;
            _password = config.GetSection("MailInfo").GetSection("Password").Value;
            _toAddress = config.GetSection("MailInfo").GetSection("ToAddress").Value;
        }
    public async Task Send(Mail mail)
    {
        using (var client = new SmtpClient(_smtpHost, _smtpPort))
        {
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_username, _password);
            MailMessage mailMesssage = new MailMessage();
            mailMesssage.From = new MailAddress(_username);
            var addresses = _toAddress.Split(";");
            foreach (var address in addresses){
                mailMesssage.To.Add(address);}
                mailMesssage.Subject = mail.Subject;
                mailMesssage.Body = mail.MailBody;
                mailMesssage.IsBodyHtml = true;
                await client.SendMailAsync(mailMesssage);
        }
    }
}