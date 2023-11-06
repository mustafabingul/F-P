using FosterPartnersWebAPI.Enums;
using FosterPartnersWebAPI.Models;

namespace FosterPartnersWebAPI.Repository;

public interface IMyTaskRepository
{
    MyTask? GetMyTask(Guid taskId);
    List<MyTask> GetMyAllTask();
    MyTask AddMyTask(MyTask task);
    void UpdateMyTask(Guid taskId, TaskStatuses taskStatus);
}