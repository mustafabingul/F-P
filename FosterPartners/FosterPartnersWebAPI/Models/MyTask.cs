
using System.ComponentModel.DataAnnotations;
using FosterPartnersWebAPI.Enums;

namespace FosterPartnersWebAPI.Models;

public class MyTask
{ 
    [Key]
    public Guid Id { get; set; }
    public TaskStatuses TaskStatus { get; set; }
    public DateTime TaskUpdatedTime { get; set; }
}