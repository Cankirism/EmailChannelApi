using EmailChannelApi.Services;
namespace EmailChannelApi;
public class QueueReaderService:BackgroundService
{
    private readonly IQueueService _queueService;
    private readonly IMailService _mailService;
    private ILogger<MailService> _logger;
    public QueueReaderService(IQueueService queueService,IMailService mailService,ILogger<MailService> logger)
    {
        _queueService = queueService;
        _mailService=mailService;
        _logger=logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested){

           var mail =  await _queueService.DeQueue(stoppingToken);
           await _mailService.Send(mail);
           _logger.LogInformation($"{mail.Subject} konulu mail gönderildi");


        }
    }
}