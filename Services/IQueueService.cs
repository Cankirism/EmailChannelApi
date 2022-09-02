namespace EmailChannelApi.Services;
public interface IQueueService
{
    Task AddQueue(Mail mail);
    Task<Mail> DeQueue(CancellationToken cancellationToken);
}