using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskly.Models;
using Taskly.Services;
using Taskly;
using Taskly.Forms; // For User.LoggedInUser

namespace Taskly.Services
{
    public class UserPresenter
    {
        private readonly IUserView view;
        private readonly TaskManagementDBContext db;

        public UserPresenter(IUserView view)
        {
            this.view = view;
            db = new TaskManagementDBContext();
        }

        public void LoadUserData()
        {
            var user = db.Users.FirstOrDefault(u => u.ID == view.CurrentUserID);
            if (user != null)
            {
                view.DisplayUser(user.Username, user.Email, user.Avatar);

                var projectNames = db.Projects
                    .Where(p => p.AdminID == view.CurrentUserID)
                    .Select(p => p.projectName)
                    .ToArray();
                view.DisplayProjects(projectNames);

                var activities = db.ActivityLogs // Thay đổi ở đây: db.Notifications thành db.ActivityLogs
                    .Where(a => a.UserId == view.CurrentUserID) // Sử dụng UserId thay vì UserID
                    .OrderByDescending(a => a.Timestamp) // Sử dụng Timestamp thay vì CreateAt
                    .Select(a => $"[{a.Timestamp}] {a.Action} - {a.Details}") // Điều chỉnh format string để khớp với ActivityLog
                    .ToArray();
                view.DisplayActivityHistory(activities);
            }
        }

        // Removed ChangePassword method as requested
        // public void ChangePassword()
        // {
        //     var user = db.Users.FirstOrDefault(u => u.ID == view.CurrentUserID);
        //     if (user != null && user.Password == view.OldPassword)
        //     {
        //         if (view.NewPassword == view.ConfirmPassword && !string.IsNullOrWhiteSpace(view.NewPassword))
        //         {
        //             user.Password = view.NewPassword;
        //             db.SaveChanges();
        //             view.ShowMessage("Password changed successfully.");
        //         }
        //         else
        //         {
        //             view.ShowMessage("New password confirmation does not match.");
        //         }
        //     }
        //     else
        //     {
        //         view.ShowMessage("Old password is incorrect.");
        //     }
        // }
    }
}