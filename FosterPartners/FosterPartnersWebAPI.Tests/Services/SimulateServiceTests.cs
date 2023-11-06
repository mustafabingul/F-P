using FosterPartnersWebAPI.Services;
using Xunit;


namespace FosterPartnersWebAPI.Tests.Services;

public class SimulateServiceTests
{
    [Fact]
    public void Simulate_DoesNotThrowException()
    {
        //Arrange
        var service = new SimulateService();

        //Act&Assert
        try
        {
            service.Simulate();
        }
        catch (Exception ex)
        {
            Assert.True(false, $"Simulate threw an exception: {ex}");
        }
    }
    
}