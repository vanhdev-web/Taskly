using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP.Models;
using OOP.Services;

namespace OOP.Services
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

                var activities = db.Notifications
                    .Where(n => n.UserID == view.CurrentUserID)
                    .OrderByDescending(n => n.CreateAt)
                    .Select(n => $"[{n.CreateAt:dd/MM/yyyy}] {n.Title}: {n.Content}")
                    .ToArray();

                view.DisplayActivityHistory(activities);
            }
        }

        public void ChangePassword()
        {
            var user = db.Users.FirstOrDefault(u => u.ID == view.CurrentUserID);
            if (user != null && user.Password == view.OldPassword)
            {
                if (view.NewPassword == view.ConfirmPassword && !string.IsNullOrWhiteSpace(view.NewPassword))
                {
                    user.Password = view.NewPassword;
                    db.SaveChanges();
                    view.ShowMessage("Password changed successfully.");
                }
                else
                {
                    view.ShowMessage("New password confirmation does not match.");
                }
            }
            else
            {
                view.ShowMessage("Old password is incorrect.");
            }
        }
    }
}
