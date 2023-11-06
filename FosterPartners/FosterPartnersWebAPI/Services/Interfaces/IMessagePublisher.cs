using FosterPartnersWebAPI.Models;

namespace FosterPartnersWebAPI.Services.Interfaces;

public interface IMessagePublisher
{
    void PublishStartMessage(Guid taskId);
    void Dispose();
}