using FosterPartnersWebAPI.Enums;
using FosterPartnersWebAPI.Models;

namespace FosterPartnersWebAPI.Repository;

public class MyTaskRepository : IMyTaskRepository
{
    public MyTask? GetMyTask(Guid taskId)
    {
        using (var context = new MyDbContext())
        {
            var myTask = context.MyTasks.Find(taskId);
            return myTask;
        }
    }
    
    public List<MyTask> GetMyAllTask()
    {
        using (var context = new MyDbContext())
        {
            var myTask = context.MyTasks.ToList();
            return myTask;
        }
    }

    public MyTask AddMyTask(MyTask task)
    {
        using (var context = new MyDbContext())
        {
            var res = context.MyTasks.Add(task);
            context.SaveChangesAsync();
            return res.Entity;
        }
    }

    public void UpdateMyTask(Guid taskId, TaskStatuses taskStatus)
    {
        using (var context = new MyDbContext())
        {
            var existing = context.MyTasks.First(t=> t.Id == taskId);
            existing.TaskStatus = taskStatus;
            existing.TaskUpdatedTime = DateTime.Now;
            context.MyTasks.Update(existing);
            context.SaveChangesAsync();
        } 
    }
}