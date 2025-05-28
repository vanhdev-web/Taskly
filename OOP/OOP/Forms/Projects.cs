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
        private ProjectsService _projectsService;

        public Projects()
        {
            InitializeComponent();
            _projectsService = new ProjectsService();
            LoadProjectsFromFile();
            UpdateComboBox();
        }

        private void LoadProjectsFromFile()
        {
            projects = _projectsService.LoadProjects();
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
                confirmation.Click += new EventHandler(Confirmation_Click);
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;
                prompt.Tag = textBox;
                prompt.ShowDialog();

                return textBox.Text;
            }

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

            Project newProject = new Project
            {
                projectName = inputName,
                projectDescription = projectDescription,
                AdminID = User.LoggedInUser.ID
            };

            await _projectsService.CreateProjectAsync(newProject);
            comboBox1.Items.Add($"{newProject.projectID} - {newProject.projectName}");
            MessageBox.Show("Tạo dự án thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            string selectedText = comboBox1.SelectedItem.ToString();
            if (int.TryParse(selectedText.Split('-')[0].Trim(), out int selectedProjectID))
            {
                Addmember userForm = new Addmember(this);
                if (userForm.ShowDialog() == DialogResult.OK)
                {
                    string newMember = userForm.MemberName;
                    if (string.IsNullOrWhiteSpace(newMember))
                    {
                        MessageBox.Show("Thông tin thành viên không hợp lệ!");
                        return;
                    }

                    bool success = await _projectsService.AddMemberToProjectAsync(selectedProjectID, newMember, User.LoggedInUser);
                    if (success)
                    {
                        Project selectedProject = _projectsService.FindProjectById(selectedProjectID);
                        if (selectedProject != null)
                        {
                            DisplayMembers(selectedProject);
                        }

                        MessageBox.Show($"Đã thêm {newMember} vào dự án");
                    }
                }
            }
            else
            {
                MessageBox.Show("Dữ liệu project không hợp lệ!");
            }
        }

        private void UpdateComboBox()
        {
            comboBox1.Items.Clear();
            if (User.LoggedInUser == null) return;

            List<Project> allProjects = _projectsService.GetAllUserProjects(User.LoggedInUser.ID);

            foreach (var project in allProjects)
            {
                comboBox1.Items.Add($"{project.projectID} - {project.projectName}");
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
                    _projectsService.DeleteProject(selectedProjectID, User.LoggedInUser.ID);
                    LoadProjectsFromFile();
                    UpdateComboBox();

                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        if (comboBox1.Items[i].ToString().StartsWith($"{selectedProjectID} -"))
                        {
                            comboBox1.Items.RemoveAt(i);
                            break;
                        }
                    }

                    selectedProjectID = -1;
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
            if (comboBox1.SelectedIndex != -1)
            {
                string selectedProjectText = comboBox1.SelectedItem.ToString();
                if (int.TryParse(selectedProjectText.Split('-')[0].Trim(), out selectedProjectID))
                {
                    Project selectedProject = _projectsService.FindProjectById(selectedProjectID);

                    if (selectedProject != null)
                    {
                        description.Text = selectedProject.projectDescription;
                        DisplayMembers(selectedProject);
                        LoadTasks(selectedProject);
                    }
                }
                else
                {
                    selectedProjectID = -1;
                }
            }
        }

        private void description_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBox1.SelectedIndex != -1)
                {
                    string selectedProjectText = comboBox1.SelectedItem.ToString();
                    int selectedProjectID = int.Parse(selectedProjectText.Split('-')[0].Trim());

                    Project selectedProject = _projectsService.FindProjectById(selectedProjectID);

                    if (selectedProject != null)
                    {
                        selectedProject.projectDescription = description.Text;
                        _projectsService.UpdateProjectDescription(selectedProject.projectID, description.Text);
                        MessageBox.Show("Mô tả đã được lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void DisplayMembers(Project project)
        {
            memberPanel.Controls.Clear();
            List<User> members = _projectsService.GetProjectMembers(project.projectID, project.AdminID);

            foreach (var user in members)
            {
                MemberItem memberItem = new MemberItem(user.Username, user.ID == project.AdminID);
                memberItem.Dock = DockStyle.Left;
                memberPanel.Controls.Add(memberItem);
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
            ExitApplication();
        }

        private void description_Enter(object sender, EventArgs e)
        {
            description.ForeColor = Color.Gray;
        }

        private void description_TextChanged(object sender, EventArgs e)
        {
            description.ForeColor = Color.Gray;
        }

        private void LoadTasks(Project project)
        {
            taskContainer.Controls.Clear();
            List<AbaseTask> tasks = _projectsService.GetTasksByProjectName(project.projectName);
            foreach (AbaseTask task in tasks)
            {
                ProjectTaskUserControl taskItem = new ProjectTaskUserControl(task);
                taskItem.Dock = DockStyle.Top;
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