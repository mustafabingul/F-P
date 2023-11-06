using FosterPartnersWebAPI.Controllers;
using FosterPartnersWebAPI.Enums;
using FosterPartnersWebAPI.Models;
using FosterPartnersWebAPI.Repository;
using FosterPartnersWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class SimulateControllerTests
{
    [Fact]
    public void Start_ReturnsOk()
    {
        // Arrange
        var messagePublisherMock = new Mock<IMessagePublisher>();
        var myTaskRepositoryMock = new Mock<IMyTaskRepository>();
        var controller = new SimulateController(messagePublisherMock.Object, myTaskRepositoryMock.Object);

        var taskId = Guid.NewGuid();
        var myTask = new MyTask { Id = taskId, TaskStatus = TaskStatuses.InQueue, TaskUpdatedTime = DateTime.Now };
        myTaskRepositoryMock.Setup(repo => repo.AddMyTask(It.IsAny<MyTask>())).Returns(myTask);

        // Act
        var result = controller.Start();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var taskResult = Assert.IsType<MyTask>(okResult.Value);
        Assert.Equal(taskId, taskResult.Id);

        messagePublisherMock.Verify(publisher => publisher.PublishStartMessage(taskId), Times.Once);
        myTaskRepositoryMock.Verify(repo => repo.AddMyTask(It.IsAny<MyTask>()), Times.Once);
    }

    [Fact]
    public void CheckProgress_ReturnsOk()
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
        var result = controller.CheckProgress();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var taskResult = Assert.IsType<List<MyTask>>(okResult.Value);
        Assert.Equal(taskList.Count, taskResult.Count);
    }

    [Fact]
    public void GetResults_ReturnsOk()
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
        var result = controller.GetResults();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var taskResult = Assert.IsType<List<MyTask>>(okResult.Value);
        Assert.Equal(3, taskResult.Count);
    }
}
