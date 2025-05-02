using System.Collections.Generic;
using System.Linq;
using Taskly.Models; // Assuming TaskManagementDBContext and Project are in Taskly.Models

namespace Taskly.Models
{
    public class ProjectReportService
    {
        private readonly TaskManagementDBContext _dbContext;

        public ProjectReportService(TaskManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Project> GetProjectsForUser(int userId)
        {
            // Lấy project mà user là admin
            var adminProjects = from p in _dbContext.Projects
                                where p.AdminID == userId
                                select p;

            // Lấy project từ các Task được gán cho user
            var taskProjects = from t in _dbContext.Tasks
                               where t.AssignedTo == userId
                               join p in _dbContext.Projects on t.ProjectID equals p.projectID
                               select p;

            // Lấy project từ các Meeting được gán cho user
            var meetingProjects = from m in _dbContext.Meetings
                                  where m.AssignedTo == userId
                                  join p in _dbContext.Projects on m.ProjectID equals p.projectID
                                  select p;

            // Lấy project từ các Milestone được gán cho user
            var milestoneProjects = from m in _dbContext.Milestones
                                    where m.AssignedTo == userId
                                    join p in _dbContext.Projects on m.ProjectID equals p.projectID
                                    select p;

            // Hợp nhất tất cả các project và loại bỏ trùng
            var allProjects = adminProjects
                              .Union(taskProjects)
                              .Union(meetingProjects)
                              .Union(milestoneProjects)
                              .Distinct()
                              .ToList();
            return allProjects;
        }

        public List<ActivityLogEntry> GetProjectAndRelatedActivities(int projectId)
        {
            // Get activities for the selected project
            var projectActivities = _dbContext.ActivityLogs
                .Where(a => a.ObjectType == "Project" && a.ObjectId == projectId)
                .ToList();

            var taskActivities = _dbContext.ActivityLogs
                .Where(a => a.ObjectType == "Task")
                .Join(_dbContext.Tasks, a => a.ObjectId, t => t.taskID, (a, t) => new { a, t })
                .Where(x => x.t.ProjectID == projectId)
                .Select(x => x.a)
                .Concat(
                    _dbContext.ActivityLogs
                    .Where(a => a.ObjectType == "Meeting")
                    .Join(_dbContext.Meetings, a => a.ObjectId, m => m.taskID, (a, m) => new { a, m })
                    .Where(x => x.m.ProjectID == projectId)
                    .Select(x => x.a)
                )
                .Concat(
                    _dbContext.ActivityLogs
                    .Where(a => a.ObjectType == "Milestone")
                    .Join(_dbContext.Milestones, a => a.ObjectId, ms => ms.taskID, (a, ms) => new { a, ms })
                    .Where(x => x.ms.ProjectID == projectId)
                    .Select(x => x.a)
                )
                .ToList();

            var allActivities = new List<ActivityLogEntry>();

            foreach (var pa in projectActivities)
            {
                allActivities.Add(new ActivityLogEntry
                {
                    Timestamp = pa.Timestamp,
                    Username = _dbContext.Users.FirstOrDefault(u => u.ID == pa.UserId)?.Username ?? "Unknown",
                    Action = pa.Action,
                    Details = pa.Details
                });
            }

            foreach (var ta in taskActivities)
            {
                allActivities.Add(new ActivityLogEntry
                {
                    Timestamp = ta.Timestamp,
                    Username = _dbContext.Users.FirstOrDefault(u => u.ID == ta.UserId)?.Username ?? "Unknown",
                    Action = ta.Action,
                    Details = ta.Details
                });
            }

            return allActivities.OrderByDescending(a => a.Timestamp).ToList();
        }
    }
}