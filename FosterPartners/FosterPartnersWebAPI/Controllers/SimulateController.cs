using FosterPartnersWebAPI.Enums;
using FosterPartnersWebAPI.Models;
using FosterPartnersWebAPI.Repository;
using FosterPartnersWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FosterPartnersWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimulateController : ControllerBase
{
    private readonly IMessagePublisher _messagePublisher;
    private readonly IMyTaskRepository _myTaskRepository;
    
    public SimulateController(IMessagePublisher messagePublisher, IMyTaskRepository myTaskRepository)
    {
        _messagePublisher = messagePublisher;
        _myTaskRepository = myTaskRepository;
    }

    [HttpPost("start")]
    public IActionResult Start()
    {
        var tempTaks = _myTaskRepository.AddMyTask(new MyTask { Id = Guid.NewGuid(), TaskStatus = TaskStatuses.InQueue, TaskUpdatedTime = DateTime.Now });
        _messagePublisher.PublishStartMessage(tempTaks.Id);
        return Ok(tempTaks);
    }

    [HttpGet("checkProgress")]
    public IActionResult CheckProgress()
    {
        var result = _myTaskRepository.GetMyAllTask();
        return Ok(result);
    }

    [HttpGet("results")]
    public IActionResult GetResults()
    {
        var result = _myTaskRepository.GetMyAllTask().Where(t => t.TaskStatus == TaskStatuses.Done).ToList();
        return Ok(result);
    }
}