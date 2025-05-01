using OOP.Models;
using OOP.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using OOP.Usercontrols;
using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OOP.Forms;

namespace OOP
{
    public partial class Projects : BaseForm
    {
        private int selectedProjectID = -1;
        List<Models.Project> projects = new List<Models.Project>();

        public Projects()
        {
            InitializeComponent();

            LoadProjectsFromFile();
            UpdateComboBox();

        }

        private void LoadProjectsFromFile()
        {
            using (var context = new TaskManagementDBContext())
            {
                var query = from p in context.Projects
                            select new Project
                            {
                                projectID = p.projectID,
                                projectName = p.projectName,
                                projectDescription = p.projectDescription,
                                AdminID = p.AdminID
                            };

                projects = query.ToList();
            }
        }
        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 300,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };

                Label textLabel = new Label() { Left = 10, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 10, Top = 50, Width = 260 };
                Button confirmation = new Button() { Text = "OK", Left = 100, Width = 100, Top = 80 };

                // Gán sự kiện Click mà không dùng lambda
                confirmation.Click += new EventHandler(Confirmation_Click);

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                // Lưu textBox vào Tag để truy xuất khi cần
                prompt.Tag = textBox;

                prompt.ShowDialog();

                return textBox.Text;
            }

            // Xử lý sự kiện Click của button
            private static void Confirmation_Click(object sender, EventArgs e)
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    Form prompt = btn.FindForm();
                    if (prompt != null)
                    {
                        prompt.Close();
                    }
                }
            }

        }
        private void panel1_Click(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void btnCreateProject_Click(object sender, EventArgs e)
        {
            string inputName = Prompt.ShowDialog("Nhập tên dự án:", "Tạo Project");

            if (string.IsNullOrWhiteSpace(inputName))
            {
                MessageBox.Show("Tên dự án không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string projectDescription = "What's this project about?";


            using (var dbContext = new TaskManagementDBContext())
            {

                Project newProject = new Project
                {
                    projectName = inputName,
                    projectDescription = projectDescription,
                    AdminID = User.LoggedInUser.ID
                };

                dbContext.Projects.Add(newProject);
                dbContext.SaveChanges();

                // Cập nhật ComboBox
                comboBox1.Items.Add($"{newProject.projectID} - {newProject.projectName}");
                ActivityLogService activityLogService = new ActivityLogService(dbContext);
                await activityLogService.LogActivityAsync(userId: User.LoggedInUser.ID, objectType: "Project", objectId: newProject.projectID, action: "Create Project", details: $"{User.LoggedInUser.Username} đã tạo dự án {newProject.projectName} lúc {DateTime.Now}");
                MessageBox.Show("Activitlog tạo project");

                MessageBox.Show("Tạo dự án thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }








        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Projects_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một project!");
                return;
            }

            // Tách lấy ID project từ comboBox
            string selectedText = comboBox1.SelectedItem.ToString();
            string[] parts = selectedText.Split('-');
            if (parts.Length < 2 || !int.TryParse(parts[0].Trim(), out int selectedProjectID))
            {
                MessageBox.Show("Dữ liệu project không hợp lệ!");
                return;
            }

            using (var dbContext = new TaskManagementDBContext())
            {
                // Dùng from-where-select để tìm project
                var selectedProject = (from p in dbContext.Projects
                                       where p.projectID == selectedProjectID
                                       select p).FirstOrDefault();

                if (selectedProject == null)
                {
                    MessageBox.Show("Không tìm thấy project!");
                    return;
                }

                // Hiện form thêm thành viên
                Addmember userForm = new Addmember(this);
                if (userForm.ShowDialog() == DialogResult.OK)
                {
                    string newMember = userForm.MemberName;

                    if (string.IsNullOrWhiteSpace(newMember))
                    {
                        MessageBox.Show("Thông tin thành viên không hợp lệ!");
                        return;
                    }

                    // Tìm user bằng from-where-select
                    var user = (from u in dbContext.Users
                                where u.Username.ToLower() == newMember.ToLower()
                                select u).FirstOrDefault();

                    if (user == null)
                    {
                        MessageBox.Show("Người dùng không tồn tại! Vui lòng nhập tên thành viên hợp lệ.");
                        return;
                    }

                    // Kiểm tra nếu user đã được gán task trong project
                    var assignedTasks = from t in dbContext.Tasks
                                        where t.ProjectID == selectedProjectID && t.AssignedTo == user.ID
                                        select t;

                    if (assignedTasks.Any())
                    {
                        MessageBox.Show($"Thành viên {newMember} đã được gán vào dự án này!");
                        return;
                    }

                    // Tạo một task mặc định để gán user vào project
                    Task newTask = new Task
                    {

                        taskName = $"Auto-Assigned Task for {newMember}",
                        status = "Unfinished",
                        deadline = DateTime.Now.AddDays(7),
                        AssignedTo = user.ID,
                        ProjectID = selectedProjectID
                    };

                    dbContext.Tasks.Add(newTask);
                    dbContext.SaveChanges();
                    //selectedProject.members.Add(user);
                    DisplayMembers(selectedProject); // cập nhật giao diện
                    ActivityLogService activityLogService = new ActivityLogService(dbContext);
                    await activityLogService.LogActivityAsync(userId: User.LoggedInUser.ID, objectType: "Project", objectId: selectedProject.projectID, action: "Add Member", details: $"{User.LoggedInUser.Username} đã mời {user.Username} vào dự án {selectedProject.projectName} ");
                    MessageBox.Show($"Đã thêm {newMember} vào dự án dưới dạng một nhiệm vụ mặc định.");
                }
            }
        }


        private ProjectManager projectManager = new ProjectManager();
        private void UpdateComboBox()
        {
            comboBox1.Items.Clear();

            if (User.LoggedInUser == null) return;

            using (var context = new TaskManagementDBContext())
            {
                int userId = User.LoggedInUser.ID;

                // lấy danh sách project mà người dùng là admin
                var adminProjects = from p in context.Projects
                                    where p.AdminID == userId
                                    select p;

                // lấy danh sách project từ Tasks mà user được gán
                var taskProjects = from t in context.Tasks
                                   where t.AssignedTo == userId
                                   join p in context.Projects on t.ProjectID equals p.projectID
                                   select p;

                // lấy danh sách project từ Meetings mà user được gán
                var meetingProjects = from m in context.Meetings
                                      where m.AssignedTo == userId
                                      join p in context.Projects on m.ProjectID equals p.projectID
                                      select p;

                // lấy danh sách project từ Milestones mà user được gán
                var milestoneProjects = from m in context.Milestones
                                        where m.AssignedTo == userId
                                        join p in context.Projects on m.ProjectID equals p.projectID
                                        select p;

                // hợp tất cả các project lại và loại bỏ trùng lặp
                var allProjects = adminProjects
                                  .Union(taskProjects)
                                  .Union(meetingProjects)
                                  .Union(milestoneProjects)
                                  .Distinct()
                                  .ToList();

                // thêm vào comboBox
                foreach (var project in allProjects)
                {
                    comboBox1.Items.Add($"{project.projectID} - {project.projectName}");
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedProjectID == -1)
            {
                MessageBox.Show("Vui lòng chọn một project để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa project ID {selectedProjectID}?", "Xác nhận xóa",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (projectManager == null)
                    {
                        MessageBox.Show("Hệ thống chưa khởi tạo project manager!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Xóa project theo ID
                    projectManager.DeleteProject(selectedProjectID);
                    projectManager.SaveProjectsToFile();
                    LoadProjectsFromFile();
                    UpdateComboBox();
                    // Xóa khỏi comboBox1
                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        if (comboBox1.Items[i].ToString().StartsWith($"{selectedProjectID} -"))
                        {
                            comboBox1.Items.RemoveAt(i);
                            break;
                        }
                    }

                    selectedProjectID = -1; // Reset ID sau khi xóa

                    MessageBox.Show("Project đã được xóa!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void projectContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (projects.Count == 0)
            {
                MessageBox.Show("Danh sách dự án trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            comboBox1.Items.Clear();

            foreach (Project project in projects)
            {
                comboBox1.Items.Add($"{project.projectID} - {project.projectName}");
            }
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1) // Kiểm tra nếu có project được chọn
            {
                string selectedProjectText = comboBox1.SelectedItem.ToString();

                if (int.TryParse(selectedProjectText.Split('-')[0].Trim(), out selectedProjectID)) // Gán ID vào biến toàn cục
                {
                    // Tìm project theo ID
                    Project selectedProject = null;
                    foreach (Project project in projects)
                    {
                        if (project.projectID == selectedProjectID)
                        {
                            selectedProject = project;
                            break; // Thoát vòng lặp khi tìm thấy project
                        }
                    }

                    if (selectedProject != null)
                    {
                        description.Text = selectedProject.projectDescription;
                        // Hiển thị mô tả của project
                        DisplayMembers(selectedProject);
                        LoadTasks(selectedProject);
                    }
                }
                else
                {
                    selectedProjectID = -1; // Nếu parse thất bại, reset ID
                }
            }
        }


        private void description_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Kiểm tra nếu nhấn Enter
            {
                if (comboBox1.SelectedIndex != -1) // Đảm bảo có project được chọn
                {
                    string selectedProjectText = comboBox1.SelectedItem.ToString();
                    int selectedProjectID = int.Parse(selectedProjectText.Split('-')[0].Trim());

                    // Tìm project theo ID bằng vòng lặp
                    Project selectedProject = null;
                    foreach (Project project in projects)
                    {
                        if (project.projectID == selectedProjectID)
                        {
                            selectedProject = project;
                            break; // Thoát vòng lặp ngay khi tìm thấy
                        }
                    }

                    if (selectedProject != null)
                    {
                        selectedProject.projectDescription = description.Text; // Cập nhật mô tả

                        //SaveProjectsToFile(); // Lưu vào file JSON
                        MessageBox.Show("Mô tả đã được lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void DisplayMembers(Project project)
        {
            memberPanel.Controls.Clear();

            using (var dbContext = new TaskManagementDBContext())
            {
                var taskMemberIds = dbContext.Tasks
                    .Where(t => t.ProjectID == project.projectID)
                    .Select(t => t.AssignedTo);

                var meetingMemberIds = dbContext.Meetings
                    .Where(m => m.ProjectID == project.projectID)
                    .Select(m => m.AssignedTo);

                var milestoneMemberIds = dbContext.Milestones
                    .Where(ms => ms.ProjectID == project.projectID)
                    .Select(ms => ms.AssignedTo);

                var allMemberIds = taskMemberIds
                    .Union(meetingMemberIds)
                    .Union(milestoneMemberIds)
                    .Distinct()
                    .ToList();

                var admin = dbContext.Users.FirstOrDefault(u => u.ID == project.AdminID);
                if (admin != null)
                {
                    MemberItem ownerItem = new MemberItem(admin.Username, true);
                    ownerItem.Dock = DockStyle.Left;
                    memberPanel.Controls.Add(ownerItem);
                }

                foreach (var userId in allMemberIds)
                {
                    if (userId == project.AdminID)
                        continue;

                    var user = dbContext.Users.FirstOrDefault(u => u.ID == userId);
                    if (user != null)
                    {
                        MemberItem memberItem = new MemberItem(user.Username, false);
                        memberItem.Dock = DockStyle.Left;
                        memberPanel.Controls.Add(memberItem);
                    }
                }



            }
        }
        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void memberItem1_Load(object sender, EventArgs e)
        {

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

        private void description_Enter(object sender, EventArgs e)
        {
            description.ForeColor = Color.Gray; // Giữ màu đúng khi nhập vào
        }

        private void description_TextChanged(object sender, EventArgs e)
        {
            description.ForeColor = Color.Gray; // Cập nhật màu khi nhập
        }

        TaskManager taskManager = TaskManager.GetInstance();
        private void LoadTasks(Project project)
        {
            // Xóa các control cũ trong panel trước khi thêm mới
            taskContainer.Controls.Clear();

            foreach (AbaseTask task in taskManager.GetTasksByProject(project.projectName))
            {
                ProjectTaskUserControl taskItem = new ProjectTaskUserControl(task);
                taskItem.Dock = DockStyle.Top; // Stack tasks from top to bottom
                taskContainer.Controls.Add(taskItem);
                ApplyMouseEvents(taskItem.TaskPanel);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ProjectReport feedForm = new ProjectReport();
            feedForm.StartPosition = FormStartPosition.CenterParent;
            feedForm.ShowDialog();
        }
    }

}