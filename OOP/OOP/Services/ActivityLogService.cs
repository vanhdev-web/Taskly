using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskly.Models;
using System.Windows.Forms;

namespace Taskly.Services
{
    public class ActivityLogService: IActivityLogService
    {
        private readonly TaskManagementDBContext _context;


        public ActivityLogService(TaskManagementDBContext context)
        {
            _context = context;
         
        }

        public async System.Threading.Tasks.Task LogActivityAsync(
            int? userId,
            string objectType,
            int? objectId,
            string action,
            string details = null)
        {


            var logEntry = new ActivityLog
            {
                UserId = userId,
                ObjectType = objectType,
                ObjectId = objectId,
                Action = action,
                Details = details,
                Timestamp = DateTime.UtcNow,

            };

            _context.ActivityLogs.Add(logEntry);
            await _context.SaveChangesAsync();
        }
    }
}
