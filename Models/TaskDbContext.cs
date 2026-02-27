using System.Collections.Generic;
using System.Data.Entity;

namespace TaskTracker.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext() : base("TaskDbContext")
        {
        }

        public DbSet<TaskModel> Tasks { get; set; }

        // YE WALI LINE ADD KAREIN
        public DbSet<UserModel> Users { get; set; }
    }
}