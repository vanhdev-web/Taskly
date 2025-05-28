using System.Collections.Generic;
using System.Linq;
using OOP.Models; // Assuming TaskManagementDBContext is in OOP.Models

namespace OOP.Models
{
    public class TaskReportService
    {
        private readonly TaskManagementDBContext _dbContext; // Assuming TaskManagementDBContext is your EF Core DbContext

        public TaskReportService(TaskManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ActivityLogEntry> GetActivityLogsForTask(int taskId)
        {
            // Join với Users để lấy username của người action
            var logs = _dbContext.ActivityLogs
                                 .Where(log => log.ObjectType == "Task" && log.ObjectId == taskId)
                                 .Join(_dbContext.Users,
                                       log => log.UserId,
                                       user => user.ID,
                                       (log, user) => new
                                       {
                                           log.Timestamp,
                                           log.Action,
                                           log.Details,
                                           Username = user.Username ?? "Unknown"
                                       })
                                 .OrderByDescending(x => x.Timestamp)
                                 .ToList();

            var activityLogEntries = new List<ActivityLogEntry>();
            foreach (var log in logs)
            {
                activityLogEntries.Add(new ActivityLogEntry
                {
                    Timestamp = log.Timestamp,
                    Username = log.Username,
                    Action = log.Action,
                    Details = log.Details
                });
            }
            return activityLogEntries;
        }
    }

    public class ActivityLogEntry
    {
        public System.DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
    }
}