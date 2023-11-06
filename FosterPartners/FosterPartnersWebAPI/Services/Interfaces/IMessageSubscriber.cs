namespace FosterPartnersWebAPI.Services.Interfaces;

public interface IMessageSubscriber : IDisposable
{
    void Subscribe(Action<string> onProgressUpdated);
    void Dispose();
}