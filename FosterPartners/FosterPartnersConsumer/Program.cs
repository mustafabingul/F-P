using FosterPartnersConsumer;
using FosterPartnersWebAPI.Repository;
using FosterPartnersWebAPI.Services;
using FosterPartnersWebAPI.Services.Interfaces;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<ISimulateService, SimulateService>();
        services.AddSingleton<IMessageSubscriber, MessageSubscriber>();
        services.AddTransient<IMyTaskRepository, MyTaskRepository>();
        
    })
    .Build();

host.Run();