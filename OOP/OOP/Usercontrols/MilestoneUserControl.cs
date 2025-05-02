using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Taskly.Forms;
using Taskly.Models;
using Taskly.Services;

namespace Taskly.Usercontrols
{
    public partial class MilestoneUserControl : UserControl
    {
        private Milestone milestone;  // Tham chiếu đến Milestone gốc
        public event EventHandler<Milestone> OnTaskFinished;

        public Panel TaskPanel // Thuộc tính công khai để truy cập Panel
        {
            get { return panel9; } // panelContainer là tên Panel bên trong TaskControl
        }
        private void TaskPanel_Click(object sender, EventArgs e)
        {
            TaskReport report = new TaskReport(milestone.taskID, "Milestone");
            report.ShowDialog();
        }
        public MilestoneUserControl(Milestone milestone)
        {
            InitializeComponent();
            this.milestone = milestone;
            UpdateUI();
            // Bắt sự kiện click
            this.Click += TaskPanel_Click;
            panel9.Click += TaskPanel_Click;
            taskContent.Click += TaskPanel_Click;
            taskDeadline.Click += TaskPanel_Click;
        }

        private void UpdateUI()
        {
            taskContent.Text = milestone.taskName;
            //taskProject.Text = milestone.ProjectName;
            taskDeadline.Text = $"{milestone.deadline:dd/MM/yyyy}";
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            if (milestone.status == "Finished")
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
                bool isFinished = milestone.status == "Finished";

                // Toggle trạng thái task
                milestone.status = isFinished ? "Unfinished" : "Finished";

                // Cập nhật DB
                dbcontext.Milestones.Attach(milestone);
                dbcontext.Entry(milestone).State = EntityState.Modified;
                dbcontext.SaveChanges();

                // Nếu chuyển thành Finished thì ghi log
                if (!isFinished)
                {
                    ActivityLogService activityLogService = new ActivityLogService(dbcontext);
                    await activityLogService.LogActivityAsync(
                        userId: User.LoggedInUser.ID,
                        objectType: "Milestone",
                        objectId: milestone.taskID,
                        action: "Finish Milestone",
                        details: $"{User.LoggedInUser.Username} đã hoàn thành cột mốc \"{milestone.taskName}\" lúc {DateTime.Now}"
                    );


                }
            }

            UpdateButtonState();
            OnTaskFinished?.Invoke(this, milestone);
        }
    }
}