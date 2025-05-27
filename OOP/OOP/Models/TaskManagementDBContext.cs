using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace OOP.Models
{
    public class TaskManagementDBContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<MeetingMemberManagement> MeetingMemberManagements { get; set; }    
        public DbSet<MilestoneMemberManagement> MilestoneMemberManagements { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            //optionsBuilder.UseSqlite($"Data Source=CHOHAO123.db");
            //optionsBuilder.UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);

            var path = Path.Combine(AppContext.BaseDirectory, "TaskManagement.db");
            optionsBuilder.UseSqlite($"Data Source={path}");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Task>().ToTable("Tasks").UseTpcMappingStrategy();
            modelBuilder.Entity<Meeting>().ToTable("Meetings").UseTpcMappingStrategy();
            modelBuilder.Entity<Milestone>().ToTable("Milestones").UseTpcMappingStrategy();
        }
    }
}
