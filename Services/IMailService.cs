namespace EmailChannelApi.Services;
public interface IMailService
{
       Task Send(Mail mail);
}