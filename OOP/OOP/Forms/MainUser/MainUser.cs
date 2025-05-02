using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OOP.Models;
using OOP.Services;
using OOP.Usercontrols;

namespace OOP
{
    public partial class MainUser : BaseForm
    {
        public MainUser()
        {
            InitializeComponent();
            LoadUserActivities();
            lblUserName.Text = User.LoggedInUser.Username;
            lblEmail.Text = User.LoggedInUser.Email;
            if (User.LoggedInUser.Avatar != null && User.LoggedInUser.Avatar.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(User.LoggedInUser.Avatar))
                {
                    try
                    {
                        avatar.Image = Image.FromStream(ms);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi hiển thị ảnh đại diện: {ex.Message}");
                        avatar.Image = Properties.Resources.DefaultAvatar; // Ảnh mặc định nếu lỗi
                    }
                }
            }
            else
            {
                avatar.Image = Properties.Resources.DefaultAvatar; // Ảnh mặc định nếu không có ảnh
            }

        }
        private void LoadUserActivities()
        {
            int userId = User.LoggedInUser.ID;

            using (var context = new TaskManagementDBContext())
            {
                var activities = context.ActivityLogs
                    .Where(a => a.UserId == userId)
                    .OrderByDescending(a => a.Timestamp)
                    .ToList();

                listViewActivityLog.Items.Clear();

                if (activities.Any())
                {
                    foreach (var act in activities)
                    {
                        string display = $"[{act.Timestamp}] {act.Action} - {act.Details}";
                        listViewActivityLog.Items.Add(display);
                    }
                }
                else
                {
                    listViewActivityLog.Items.Add("You have no activity logs yet!");
                }
            }
        }


        private void btnHome_Click(object sender, EventArgs e)
        {
            SwitchForm(new Home());
        }
        private void btnTask_Click(object sender, EventArgs e)
        {
            SwitchForm(new Tasks());
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            SwitchForm(new MainUser());
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            SwitchForm(new Projects());
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitApplication(); // Gọi hàm chung để thoát
        }

        private void WelcomeName_Click(object sender, EventArgs e)
        {

        }
    }
}
