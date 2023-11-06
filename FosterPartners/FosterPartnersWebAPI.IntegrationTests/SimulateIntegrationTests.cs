using FosterPartnersWebAPI.Controllers;
using FosterPartnersWebAPI.Enums;
using FosterPartnersWebAPI.Models;
using FosterPartnersWebAPI.Repository;
using FosterPartnersWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Xunit;
namespace FosterPartnersWebAPI.IntegrationTests;

public class SimulateIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SimulateIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Start_ReturnsOk()
    {
        // Arrange
        var messagePublisherMock = new Mock<IMessagePublisher>();
        var myTaskRepositoryMock = new Mock<IMyTaskRepository>();
        var controller = new SimulateController(messagePublisherMock.Object, myTaskRepositoryMock.Object);

        var taskId = Guid.NewGuid();
        var myTask = new MyTask { Id = taskId, TaskStatus = TaskStatuses.InQueue, TaskUpdatedTime = DateTime.Now };
        myTaskRepositoryMock.Setup(repo => repo.AddMyTask(It.IsAny<MyTask>())).Returns(myTask);
        
        // Act
        var response = await _client.PostAsync("/api/simulation/start", null);

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task CheckProgress_ReturnsOk()
    {
        // Arrange
        var myTaskRepositoryMock = new Mock<IMyTaskRepository>();
        var controller = new SimulateController(null, myTaskRepositoryMock.Object);

        var taskList = new List<MyTask>
        {
            new MyTask { Id = Guid.NewGuid(), TaskStatus = TaskStatuses.InQueue, TaskUpdatedTime = DateTime.Now },
            new MyTask { Id = Guid.NewGuid(), TaskStatus = TaskStatuses.InProgress, TaskUpdatedTime = DateTime.Now },
            new MyTask { Id = Guid.NewGuid(), TaskStatus = TaskStatuses.Done, TaskUpdatedTime = DateTime.Now }
        };

        myTaskRepositoryMock.Setup(repo => repo.GetMyAllTask()).Returns(taskList);
        
        // Act
        var response = await _client.GetAsync("/api/Simulate/checkProgress");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetResults_ReturnsOk()
    {
        // Arrange
        var myTaskRepositoryMock = new Mock<IMyTaskRepository>();
        var controller = new SimulateController(null, myTaskRepositoryMock.Object);

        var taskList = new List<MyTask>
        {
            new MyTask { Id = Guid.NewGuid(), TaskStatus = TaskStatuses.InQueue, TaskUpdatedTime = DateTime.Now },
            new MyTask { Id = Guid.NewGuid(), TaskStatus = TaskStatuses.Done, TaskUpdatedTime = DateTime.Now },
            new MyTask { Id = Guid.NewGuid(), TaskStatus = TaskStatuses.Done, TaskUpdatedTime = DateTime.Now },
            new MyTask { Id = Guid.NewGuid(), TaskStatus = TaskStatuses.Done, TaskUpdatedTime = DateTime.Now },
            new MyTask { Id = Guid.NewGuid(), TaskStatus = TaskStatuses.InProgress, TaskUpdatedTime = DateTime.Now }
        };

        myTaskRepositoryMock.Setup(repo => repo.GetMyAllTask()).Returns(taskList);
        
        // Act
        var response = await _client.GetAsync("/api/Simulate/results");

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
}