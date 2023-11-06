using FosterPartnersWebAPI.Enums;
using FosterPartnersWebAPI.Repository;
using FosterPartnersWebAPI.Services.Interfaces;

namespace FosterPartnersConsumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ISimulateService _simulateService;
    private readonly IMessageSubscriber _messageSubscriber;
    private readonly IMyTaskRepository _myTaskRepository;
    
    public Worker(ILogger<Worker> logger, ISimulateService simulateService, IMessageSubscriber messageSubscriber, IMyTaskRepository myTaskRepository)
    {
        _logger = logger;
        _messageSubscriber = messageSubscriber;
        _simulateService = simulateService;
        _myTaskRepository = myTaskRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageSubscriber.Subscribe(message =>
        {
            if (!string.IsNullOrEmpty(message))
            {
                Guid tempGuid;
                if (Guid.TryParse(message, out tempGuid))
                {
                    Task.Run(() => ProcessSimulate(tempGuid));
                }
            }
        });
    }

    private void ProcessSimulate(Guid taskId)
    {
        try
        {
            _myTaskRepository.UpdateMyTask(taskId,TaskStatuses.InProgress);
            _simulateService.Simulate();
            _myTaskRepository.UpdateMyTask(taskId,TaskStatuses.Done);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}