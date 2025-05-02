using Taskly.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Taskly.Services
{
    public class TaskService
    {
        public Models.Task CreateTask(string taskName, DateTime deadline, int assignedToId, int projectId)
        {
            using (var db = new TaskManagementDBContext())
            {
                var newTask = new Models.Task // Specify Models.Task to avoid ambiguity with System.Threading.Tasks.Task
                {
                    taskName = taskName,
                    status = "Unfinished",
                    deadline = deadline,
                    AssignedTo = assignedToId,
                    ProjectID = projectId
                };
                db.Tasks.Add(newTask);
                db.SaveChanges();
                return newTask;
            }
        }

        public int GetProjectIdByName(string projectName)
        {
            using (var db = new TaskManagementDBContext())
            {
                return db.Projects.FirstOrDefault(p => p.projectName == projectName)?.projectID ?? 0;
            }
        }

        public List<User> GetProjectMembers(int projectId)
        {
            using (var db = new TaskManagementDBContext())
            {
                var taskUsers = db.Tasks
                                  .Where(t => t.ProjectID == projectId)
                                  .Select(t => t.AssignedTo);
                var meetingUsers = db.Meetings
                                     .Where(m => m.ProjectID == projectId)
                                     .Select(m => m.AssignedTo);
                var milestoneUsers = db.Milestones
                                       .Where(ms => ms.ProjectID == projectId)
                                       .Select(ms => ms.AssignedTo);

                var allUserIds = taskUsers
                              .Union(meetingUsers)
                              .Union(milestoneUsers)
                              .Distinct()
                              .ToList();

                var users = db.Users
                              .Where(u => allUserIds.Contains(u.ID))
                              .ToList();
                return users;
            }
        }

        public User GetUserByUsernameAndProject(string username, int projectId)
        {
            using (var db = new TaskManagementDBContext())
            {
                var projectMembers = GetProjectMembers(projectId);
                return projectMembers.FirstOrDefault(u => u.Username.Trim() == username);
            }
        }
    }
}