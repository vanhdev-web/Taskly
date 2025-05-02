using Microsoft.Win32;
using System;
using System.Windows.Forms;
using Taskly.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Taskly.Services;

namespace Taskly
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập Username.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập Password.");
                txtPassword.Focus();
                return;
            }
            using (var context = new TaskManagementDBContext())
            {
                var query = from u in context.Users
                            where u.Username == username && u.Password == password
                            select u;

                var user = query.FirstOrDefault();

                if (user != null)
                {
                    User.LoggedInUser = user;

                    Home mainForm = new Home();
                    mainForm.Show();
                    this.Hide();

                    //activitylog đăng nhập
                    ActivityLogService activityLogService = new ActivityLogService(context);
                    await activityLogService.LogActivityAsync(userId: null, objectType: "User", objectId: User.LoggedInUser.ID, action: "LoggedIn", details: $"User Name : {User.LoggedInUser.Username}");
                  
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    MessageBox.Show("Vui lòng nhập Username.");
                    txtUsername.Focus();
                    e.Handled = true;
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập Password.");
                    txtPassword.Focus();
                    e.Handled = true;
                    return;
                }

                btnLogin_Click(sender, e);
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    MessageBox.Show("Vui lòng nhập Username.");
                    txtUsername.Focus();
                    e.Handled = true;
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập Password.");
                    txtPassword.Focus();
                    e.Handled = true;
                    return;
                }

                btnLogin_Click(sender, e);
                e.Handled = true;
            }
        }

        private void Logintext_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            var context = new TaskManagementDBContext();
            context.Database.EnsureCreated();

            //SeedData(context);

            //context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'Tasks'");
            //context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'Meetings'");
            //context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'Milestones'");
            //context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'Users'");
            //context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'Projects'");
            //context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'Notifications'");
            //context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'MeetingMemberManagement'");
            //context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'MilestoneMemberManagement'");
            //MessageBox.Show("Cơ sở dữ liệu đã được làm mới. Vui lòng đăng nhập lại để tiếp tục.");





        }

        private void SeedData(TaskManagementDBContext context)
        {
            if (context.Users.Any() || context.Projects.Any() || context.Tasks.Any() || context.Meetings.Any() || context.Milestones.Any())
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

            var tasks = new List<Task>
            {
                new Task {  taskName = "Task One", status = "Finished", deadline = DateTime.Parse("2025-06-01"), AssignedTo = 1, ProjectID = 1 },
                new Task {  taskName = "Task Two", status = "Unfinished", deadline = DateTime.Parse("2025-05-20"), AssignedTo = 2, ProjectID = 1 },
                new Task {  taskName = "Task Three", status = "Finished", deadline = DateTime.Parse("2025-06-10"), AssignedTo = 3, ProjectID = 2 },
                new Task {  taskName = "Task Four", status = "Finished", deadline = DateTime.Parse("2025-06-15"), AssignedTo = 4, ProjectID = 2 },
                new Task {  taskName = "Task Five", status = "Unfinished", deadline = DateTime.Parse("2025-06-20"), AssignedTo = 5, ProjectID = 3 },
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