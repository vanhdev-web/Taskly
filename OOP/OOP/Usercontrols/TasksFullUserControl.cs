﻿using System;

using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Taskly.Forms;
using Taskly.Models;
using Taskly.Services;
using Taskly.Usercontrols;
using Task = Taskly.Models.Task;
namespace Taskly.Usercontrols
{
    public partial class TasksFullUserControl : UserControl
    {
        private Task task;  // Tham chiếu đến Task gốc
        public event EventHandler<Task> OnTaskFinished;
        public ProjectManager projectManager = new ProjectManager(); // Khởi tạo projectManager để truy cập dự án

        private void TaskPanel_Click(object sender, EventArgs e)
        {
            TaskReport report = new TaskReport(task.taskID, "Task");
            report.ShowDialog();
        }
        public Panel TaskPanel // Thuộc tính công khai để truy cập Panel
        {
            get { return panel9; } // panelContainer là tên Panel bên trong TaskControl
        }
        public TasksFullUserControl(Task task)
        {
            InitializeComponent();
            this.task = task;
            UpdateUI();
            // Bắt sự kiện click
            this.Click += TaskPanel_Click;
            panel9.Click += TaskPanel_Click;
            taskContent.Click += TaskPanel_Click;
            taskDeadline.Click += TaskPanel_Click;
        }

        private void UpdateUI()
        {
            taskContent.Text = task.taskName;
            taskDeadline.Text = $"{task.deadline:dd/MM/yyyy}";

            var project = projectManager.FindProjectById(task.ProjectID);
            taskProject.Text = project != null ? project.projectName : "(Không rõ dự án)";

            UpdateButtonState();
        }


        private void UpdateButtonState()
        {
            if (task.status == "Finished")
            {

                checkBox.Image = Properties.Resources.check;
            }
            else
            {

                checkBox.Image = Properties.Resources.checkUnfinished;
            }
            
        }


        private async void checkBox_Click(object sender, EventArgs e)
        {
            using (var dbcontext = new TaskManagementDBContext())
            {
                bool isFinished = task.status == "Finished";

                // Toggle trạng thái task
                task.status = isFinished ? "Unfinished" : "Finished";

                // Cập nhật DB
                dbcontext.Tasks.Attach(task);
                dbcontext.Entry(task).State = EntityState.Modified;
                dbcontext.SaveChanges();

                // Nếu chuyển thành Finished thì ghi log
                if (!isFinished)
                {
                    ActivityLogService activityLogService = new ActivityLogService(dbcontext);
                    await activityLogService.LogActivityAsync(
                        userId: User.LoggedInUser.ID,
                        objectType: "Task",
                        objectId: task.taskID,
                        action: "Finish Task",
                        details: $"{User.LoggedInUser.Username} đã hoàn thành task \"{task.taskName}\" lúc {DateTime.Now}"
                    );

                  
                }
            }

            UpdateButtonState();
            OnTaskFinished?.Invoke(this, task);
        }

    }
}
