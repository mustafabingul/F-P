using FosterPartnersWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FosterPartnersWebAPI.Repository;

public class MyDbContext : DbContext
{
    protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"DataSource=./../tasks.db;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
 
        modelBuilder.Entity<MyTask>().HasKey(t => new { TaskId = t.Id });
    } 
    public DbSet<MyTask> MyTasks { get; set; }
}