using System.Threading.Channels;

namespace EmailChannelApi.Services;
public class QueueService : IQueueService
{   
    private readonly Channel<Mail> queue;
    public QueueService(IConfiguration configuration)
    {
      int.TryParse(configuration["QueueLimit"],out int queueCapacity);
      BoundedChannelOptions options = new(queueCapacity){
        FullMode= BoundedChannelFullMode.Wait
        };
      queue = Channel.CreateBounded<Mail>(options); 
    }
    public async Task AddQueue(Mail mail)
    {
        // null kayıt gelirse hata fırlat 
        ArgumentNullException.ThrowIfNull(mail);
        await queue.Writer.WriteAsync(mail);
       
    }

    public async Task<Mail> DeQueue(CancellationToken cancellationToken)
    {
        var item =  await queue.Reader.ReadAsync(cancellationToken);
        return item;
    }
}
