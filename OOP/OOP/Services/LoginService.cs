using OOP.Models;
using OOP.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using Task = System.Threading.Tasks.Task; // Đặt bí danh cho System.Threading.Tasks.Task thành 'Task
using ModelTask = OOP.Models.Task; // Đặt bí danh cho OOP.Models.Task thành 'ModelTask

namespace OOP
{
    public class LoginService
    {
        public async Task<User> AuthenticateUser(string username, string password)
        {
            using (var context = new TaskManagementDBContext())
            {
                var user = await context.Users
                                        .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
                return user;
            }
        }

        public async Task LogActivity(int? userId, string objectType, int objectId, string action, string details)
        {
            using (var context = new TaskManagementDBContext())
            {
                ActivityLogService activityLogService = new ActivityLogService(context);
                await activityLogService.LogActivityAsync(userId, objectType, objectId, action, details);
            }
        }

        public void EnsureDatabaseCreated()
        {
            using (var context = new TaskManagementDBContext())
            {
                context.Database.EnsureCreated();
            }
        }

        // SeedData method is now part of the Model, as it deals with data initialization
        public void SeedData(TaskManagementDBContext context)
        {
            if (context.Users.Any() || context.Projects.Any() || context.Tasks.Any() || context.Meetings.Any() || context.Milestones.Any() || context.Notifications.Any())
            {
                return; // Dữ liệu đã được khởi tạo
            }
            var meetings = new List<Meeting>
            {
                new Meeting {  Location = "Room A", Hour = "09:00", taskName = "Design Meeting", status = "Finished", deadline = DateTime.Parse("2025-06-01"), AssignedTo = 1, ProjectID = 1 },
                new Meeting { Location = "Room B", Hour = "14:00", taskName = "Sprint Planning", status = "Unfinished", deadline = DateTime.Parse("2025-05-20"), AssignedTo = 2, ProjectID = 1 },
                new Meeting {  Location = "Room C", Hour = "10:30", taskName = "Client Meeting", status = "Finished", deadline = DateTime.Parse("2025-06-05"), AssignedTo = 3, ProjectID = 2 },
                new Meeting {  Location = "Room D", Hour = "16:00", taskName = "Retrospective", status = "Finished", deadline = DateTime.Parse("2025-05-15"), AssignedTo = 4, ProjectID = 2 },
                new Meeting {  Location = "Room E", Hour = "11:00", taskName = "Review Meeting", status = "Finished", deadline = DateTime.Parse("2025-06-10"), AssignedTo = 5, ProjectID = 3 },
            };
            var milestones = new List<Milestone>
            {
                new Milestone {  Description = "Complete UI Design", taskName = "UI Design", status = "Unfinished", deadline = DateTime.Parse("2025-06-01"), AssignedTo = 1, ProjectID = 1 },
                new Milestone {  Description = "Backend API ready", taskName = "API Development", status = "Finished", deadline = DateTime.Parse("2025-06-15"), AssignedTo = 2, ProjectID = 1 },
                new Milestone {  Description = "Deploy to staging", taskName = "Deployment", status = "Finished", deadline = DateTime.Parse("2025-06-20"), AssignedTo = 3, ProjectID = 2 },
                new Milestone { Description = "Testing completed", taskName = "Testing", status = "Finished", deadline = DateTime.Parse("2025-06-10"), AssignedTo = 4, ProjectID = 2 },
                new Milestone {  Description = "User training", taskName = "Training", status = "Unfinished", deadline = DateTime.Parse("2025-06-25"), AssignedTo = 5, ProjectID = 3 },
            };
            var notifications = new List<Notification>
            {
                new Notification { Title = "Welcome", UserID = 1, CreateAt = DateTime.Now.AddDays(-10), Content = "Welcome to the project management system." },
                new Notification { Title = "Task Assigned", UserID = 2, CreateAt = DateTime.Now.AddDays(-5), Content = "You have been assigned a new task." },
                new Notification { Title = "Meeting Reminder", UserID = 3, CreateAt = DateTime.Now.AddDays(-2), Content = "Reminder: Meeting scheduled tomorrow." },
                new Notification { Title = "Project Update", UserID = 4, CreateAt = DateTime.Now.AddDays(-1), Content = "Project deadline has been updated." },
                new Notification { Title = "System Maintenance", UserID = 5, CreateAt = DateTime.Now, Content = "System maintenance scheduled for this weekend." },
            };
            var projects = new List<Project>
            {
                new Project { projectName = "Project Alpha", projectDescription = "First project description", AdminID = 1 },
                new Project { projectName = "Project Beta", projectDescription = "Second project description", AdminID = 2 },
                new Project { projectName = "Project Gamma", projectDescription = "Third project description", AdminID = 3 },
                new Project { projectName = "Project Delta", projectDescription = "Fourth project description", AdminID = 4 },
                new Project { projectName = "Project Epsilon", projectDescription = "Fifth project description", AdminID = 5 },
            };
            var users = new List<User>
            {
                new User { Username = "user1", Password = "pass1", Email = "user1@example.com",  },
                new User { Username = "user2", Password = "pass2", Email = "user2@example.com", },
                new User { Username = "user3", Password = "pass3", Email = "user3@example.com",   },
                new User { Username = "user4", Password = "pass4", Email = "user4@example.com", },
                new User { Username = "user5", Password = "pass5", Email = "user5@example.com", },
            };
            var tasks = new List<OOP.Models.Task>
            {
                new ModelTask {  taskName = "Task One", status = "Finished", deadline = DateTime.Parse("2025-06-01"), AssignedTo = 1, ProjectID = 1 },
                new ModelTask {  taskName = "Task Two", status = "Unfinished", deadline = DateTime.Parse("2025-05-20"), AssignedTo = 2, ProjectID = 1 },
                new ModelTask {  taskName = "Task Three", status = "Finished", deadline = DateTime.Parse("2025-06-10"), AssignedTo = 3, ProjectID = 2 },
                new ModelTask {  taskName = "Task Four", status = "Finished", deadline = DateTime.Parse("2025-06-15"), AssignedTo = 4, ProjectID = 2 },
                new ModelTask {  taskName = "Task Five", status = "Unfinished", deadline = DateTime.Parse("2025-06-20"), AssignedTo = 5, ProjectID = 3 },
            };
            context.Users.AddRange(users);
            context.SaveChanges();
            context.Projects.AddRange(projects);
            context.SaveChanges();
            context.Tasks.AddRange(tasks);
            context.SaveChanges();
            context.Meetings.AddRange(meetings);
            context.SaveChanges();
            context.Milestones.AddRange(milestones); context.SaveChanges();
            context.Notifications.AddRange(notifications);
            context.SaveChanges();


            var meetingUsers = new List<MeetingMemberManagement>
            {
                new MeetingMemberManagement { MeetingID = 1, UserID = 1 },
                new MeetingMemberManagement { MeetingID = 1, UserID = 2 },
                new MeetingMemberManagement { MeetingID = 2, UserID = 2 },
                new MeetingMemberManagement { MeetingID = 2, UserID = 3 },
                new MeetingMemberManagement { MeetingID = 3, UserID = 3 },
                new MeetingMemberManagement { MeetingID = 3, UserID = 4 },
                new MeetingMemberManagement { MeetingID = 4, UserID = 4 },
                new MeetingMemberManagement { MeetingID = 4, UserID = 5 },
                new MeetingMemberManagement { MeetingID = 5, UserID = 1 },
                new MeetingMemberManagement { MeetingID = 5, UserID = 5 }
            };
            context.MeetingMemberManagements.AddRange(meetingUsers);
            context.SaveChanges();

            MessageBox.Show("Dữ liệu đã được khởi tạo thành công!");
        }
    }
}