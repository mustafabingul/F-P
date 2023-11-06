using FosterPartnersWebAPI.Repository;
using FosterPartnersWebAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FosterPartnersConsumer.Tests;

public class WorkerTests
{
    [Fact]
    public async Task ExecuteAsync_SubscribesToMessageSubscriber()
    {
        //Arrange
        var simulateServiceMock = new Mock<ISimulateService>();
        var messageSubscriberMock = new Mock<IMessageSubscriber>();
        var myTaskRepositoryMock = new Mock<IMyTaskRepository>();
        var loggerMock = new Mock<ILogger<Worker>>();

        var worker = new Worker(loggerMock.Object, simulateServiceMock.Object, messageSubscriberMock.Object, myTaskRepositoryMock.Object);

        //Act
        await worker.StartAsync(CancellationToken.None);

        //Assert
        messageSubscriberMock.Verify(m => m.Subscribe(It.IsAny<Action<string>>()), Times.Once);
    }
}