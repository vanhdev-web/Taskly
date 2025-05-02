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
using Taskly.Models;
using Taskly.Services;
using Taskly.Usercontrols;
namespace Taskly
{
    public partial class MainUser : BaseForm, IUserView
    {
        private UserPresenter _presenter;

        public MainUser()
        {
            InitializeComponent();
            _presenter = new UserPresenter(this);
            _presenter.LoadUserData(); // Load user data via presenter
        }

        // IUserView implementation
        public int CurrentUserID
        {
            get { return User.LoggedInUser.ID; }
        }

        public void DisplayActivityHistory(string[] activities)
        {
            listViewActivityLog.Items.Clear();
            if (activities.Any())
            {
                foreach (string activity in activities)
                {
                    listViewActivityLog.Items.Add(activity);
                }
            }
            else
            {
                listViewActivityLog.Items.Add("You have no activity logs yet!");
            }
        }

        public void DisplayUser(string username, string email, byte[] avatarData)
        {
            lblUserName.Text = username;
            lblEmail.Text = email;
            if (avatarData != null && avatarData.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(avatarData))
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

        public void DisplayProjects(string[] projectNames)
        {
            // You'll need a UI element (e.g., a ListBox or DataGridView) to display project names.
            // For now, I'll just add a placeholder for where you would implement this.
            // Example: listBoxProjects.Items.Clear();
            //          foreach (string projectName in projectNames)
            //          {
            //              listBoxProjects.Items.Add(projectName);
            //          }
        }

        // Removed password related properties as per request
        public string OldPassword { get { return string.Empty; } }
        public string NewPassword { get { return string.Empty; } }
        public string ConfirmPassword { get { return string.Empty; } }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
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
            // Event handler for WelcomeName click, if any specific action is needed.
        }

        private void lblUserName_Click(object sender, EventArgs e)
        {

        }
    }
}