using FosterPartnersWebAPI.Services.Interfaces;

namespace FosterPartnersWebAPI.Services;

public class SimulateService : ISimulateService
{
    public async Task Simulate()
    {
        var random = new Random();
        int delaySleep = random.Next(5, 15);
        Thread.Sleep(TimeSpan.FromSeconds(delaySleep));
    }
}