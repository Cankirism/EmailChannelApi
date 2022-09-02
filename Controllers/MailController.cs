using EmailChannelApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailChannelApi.Controllers;
[ApiController]
[Route("[controller]")]
public class MailController:ControllerBase
{   
    private readonly IQueueService _service;
    public MailController (IQueueService service)
    {
        _service=service;
        
    }
    [HttpPost]
    public IActionResult AddMail(List<Mail> mails)
    {
        foreach(var mail in mails){
            _service.AddQueue(mail);
        }
        return Ok("İşlem başarılı");
    }
}