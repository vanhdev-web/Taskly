using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP.Models;
using OOP.Services;
using Task = OOP.Models.Task;

namespace OOP
{
    public partial class Addtask : Form
    {
        public Task NewTask { get; set; }
        List<Project> projects;
        List<AbaseTask> tasks;
        List<User> users;


        public Addtask(List<Project> projects, List<AbaseTask> tasks, List<User> users)
        {
            InitializeComponent();
            this.projects = projects;
            this.tasks = tasks;
            this.users = users;
            UpdateComboBox();

        }

        private void Addtask_Load(object sender, EventArgs e)
        {

            using (var db = new TaskManagementDBContext())
            {
                // giả sử có thuộc tính adminId trong form này

                // lấy danh sách project có adminid phù hợp
                var filteredProjects = db.Projects
                                         .Where(p => p.AdminID == User.LoggedInUser.ID)
                                         .ToList();

                // đổ tên project vào combobox
                cbbSelectProject.Items.Clear();
                foreach (var project in filteredProjects)
                {
                    cbbSelectProject.Items.Add(project.projectName);
                }

                // nếu có project thì chọn mặc định là project đầu tiên
                if (cbbSelectProject.Items.Count > 0)
                {
                    cbbSelectProject.SelectedIndex = 0;
                    string selectedProjectName = cbbSelectProject.SelectedItem.ToString();

                    // tìm lại projectid từ tên
                    var selectedProject = filteredProjects
                                          .FirstOrDefault(p => p.projectName == selectedProjectName);
                    if (selectedProject == null) return;

                    int selectedProjectId = selectedProject.projectID;

                    // lấy danh sách userid từ các bảng task, meeting, milestone thuộc projectid
                    var taskUsers = db.Tasks
                                      .Where(t => t.ProjectID == selectedProjectId)
                                      .Select(t => t.AssignedTo);

                    var meetingUsers = db.Meetings
                                         .Where(m => m.ProjectID == selectedProjectId)
                                         .Select(m => m.AssignedTo);

                    var milestoneUsers = db.Milestones
                                           .Where(ms => ms.ProjectID == selectedProjectId)
                                           .Select(ms => ms.AssignedTo);

                    var userIds = taskUsers
                                  .Union(meetingUsers)
                                  .Union(milestoneUsers)
                                  .Distinct()
                                  .ToList();

                    // lấy thông tin người dùng từ userids
                    var users = db.Users
                                  .Where(u => userIds.Contains(u.ID))
                                  .ToList();

                    // đổ username vào combobox
                    cbbAssignedUser.Items.Clear();
                    cbbAssignedUser.Items.Add("Myself"); 
                    foreach (var user in users)
                    {
                        cbbAssignedUser.Items.Add(user.Username);
                    }
                }
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            string taskName = txtbInputNameTask.Text.Trim();
            DateTime deadline = dtpNewTask.Value;
            string projectName = cbbSelectProject.Text;
            string receiverUsername = cbbAssignedUser.Text.Trim();

            if (string.IsNullOrWhiteSpace(taskName) || string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(receiverUsername))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            using (var db = new TaskManagementDBContext())
            {
                // tìm project theo tên
                var selectedProject = db.Projects.FirstOrDefault(p => p.projectName == projectName);

                if (selectedProject == null)
                {
                    MessageBox.Show("Project not found.");
                    return;
                }

                int receiverID = 0;

                if (receiverUsername == "Myself")
                {
                    receiverID = User.LoggedInUser.ID;
                }
                else
                {
                    // lấy tất cả task, meeting, milestone có cùng projectid
                    var taskUserIds = db.Tasks
                                        .Where(t => t.ProjectID == selectedProject.projectID)
                                        .Select(t => t.AssignedTo);

                    var meetingUserIds = db.Meetings
                                           .Where(m => m.ProjectID == selectedProject.projectID)
                                           .Select(m => m.AssignedTo);

                    var milestoneUserIds = db.Milestones
                                             .Where(ms => ms.ProjectID == selectedProject.projectID)
                                             .Select(ms => ms.AssignedTo);

                    var allUserIds = taskUserIds
                                    .Union(meetingUserIds)
                                    .Union(milestoneUserIds)
                                    .Distinct()
                                    .ToList();

                    // tìm user trong database theo username và thuộc danh sách trên
                    var matchedUser = db.Users
                                        .Where(u => allUserIds.Contains(u.ID))
                                        .FirstOrDefault(u => u.Username.Trim() == receiverUsername);

                    if (matchedUser == null)
                    {
                        MessageBox.Show($"{receiverUsername} is not a member of this project.");
                        return;
                    }

                    receiverID = matchedUser.ID;
                }

                // tạo task mới
                var newTask = new Task
                {
                    taskName = taskName,
                    status = "Unfinished",
                    deadline = deadline,
                    AssignedTo = receiverID,
                    ProjectID = selectedProject.projectID
                };

                // lưu vào database


                // gán vào thuộc tính
                NewTask = newTask;

                DialogResult = DialogResult.OK;
                Close();
            }
        } 



           
        


        private ProjectManager projectManager = new ProjectManager();
        private void UpdateComboBox()
        {
            cbbSelectProject.Items.Clear();

            if (User.LoggedInUser == null) return; // Kiểm tra user đăng nhập

            foreach (Project project in projectManager.Projects)
            {
                if (project == null || project.members == null) continue; // Kiểm tra null tránh lỗi

                Console.WriteLine($"Project: {project.projectID} - {project.projectName}, AdminID: {project.AdminID}, Members: {string.Join(", ", project.members)}");

                bool isMember = false;
                //foreach (string member in project.members)
                //{
                //    string memberUsername = member.Split('(')[0].Trim(); // Lấy username trước dấu "(" và Trim()
                //    if (memberUsername == User.LoggedInUser.Username)
                //    {
                //        isMember = true;
                //        break;
                //    }
                //}

                if (project.AdminID == User.LoggedInUser.ID || isMember)
                {
                    cbbSelectProject.Items.Add($"{project.projectName}");
                }
            }
        }

        private void cbbSelectProject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            savedProjectName = cbbSelectProject.Text;
            UpdateMember();
        }
        string savedProjectName;
        private void UpdateMember()
        {
            cbbAssignedUser.Items.Clear();
            cbbAssignedUser.Items.Add("Myself");

            using (var db = new TaskManagementDBContext())
            {
                var selectedProject = db.Projects.FirstOrDefault(p => p.projectName == savedProjectName);
                if (selectedProject == null) return;

                int selectedProjectId = selectedProject.projectID;

                // lấy danh sách userid từ các bảng task, meeting, milestone thuộc projectid
                var taskUsers = db.Tasks
                                  .Where(t => t.ProjectID == selectedProjectId)
                                  .Select(t => t.AssignedTo);

                var meetingUsers = db.Meetings
                                     .Where(m => m.ProjectID == selectedProjectId)
                                     .Select(m => m.AssignedTo);

                var milestoneUsers = db.Milestones
                                       .Where(ms => ms.ProjectID == selectedProjectId)
                                       .Select(ms => ms.AssignedTo);

                var userIds = taskUsers
                              .Union(meetingUsers)
                              .Union(milestoneUsers)
                              .Distinct()
                              .ToList();

                var users = db.Users
                              .Where(u => userIds.Contains(u.ID))
                              .ToList();

                foreach (var user in users)
                {
                    cbbAssignedUser.Items.Add(user.Username);
                }
            }
        }

        private void cbbAssignedUser_Click(object sender, EventArgs e)
        {
            //  UpdateMember();
        }

        private void txtbInputNameTask_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbInputNameTask.Text))
            {
                e.Cancel = true; // Chặn chuyển focus nếu input trống
                errLoginTask.SetError(txtbInputNameTask, "Please enter task name!");
            }
            else
            {
                e.Cancel = false; // Cho phép focus rời khỏi control
                errLoginTask.SetError(txtbInputNameTask, null);
            }
        }

        private void btnConfirm_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}