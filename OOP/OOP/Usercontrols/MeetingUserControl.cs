using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Taskly.Models;
using Taskly.Services;
using Taskly.Forms;

namespace Taskly.Usercontrols
{
    public partial class MeetingUserControl : UserControl
    {
        private Meeting meeting;  // Tham chiếu đến Meeting gốc
        public event EventHandler<Meeting> OnMeetingFinished;

        public Panel TaskPanel // Thuộc tính công khai để truy cập Panel
        {
            get { return panel9; } // panelContainer là tên Panel bên trong MeetingUserControl
        }
        private void TaskPanel_Click(object sender, EventArgs e)
        {
            TaskReport report = new TaskReport(meeting.taskID, "Meeting");
            report.ShowDialog();
        }

        public MeetingUserControl(Meeting meeting)
        {
            InitializeComponent();
            this.meeting = meeting;
            UpdateUI();
            // Bắt sự kiện click
            this.Click += TaskPanel_Click;
            panel9.Click += TaskPanel_Click;
            taskContent.Click += TaskPanel_Click;
            taskDeadline.Click += TaskPanel_Click;
        }

        private void UpdateUI()
        {
            taskContent.Text = meeting.taskName;
            //taskProject.Text = meeting.ProjectName; // Dùng location thay vì project
            location.Text = "Tại " + meeting.Location;
            hour.Text = "Giờ: " + meeting.Hour;
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            if (meeting.status == "Finished")
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
                bool isFinished = meeting.status == "Finished";

                // Toggle trạng thái task
                meeting.status = isFinished ? "Unfinished" : "Finished";

                // Cập nhật DB
                dbcontext.Meetings.Attach(meeting);
                dbcontext.Entry(meeting).State = EntityState.Modified;
                dbcontext.SaveChanges();

                // Nếu chuyển thành Finished thì ghi log
                if (!isFinished)
                {
                    ActivityLogService activityLogService = new ActivityLogService(dbcontext);
                    await activityLogService.LogActivityAsync(
                        userId: User.LoggedInUser.ID,
                        objectType: "Meeting",
                        objectId: meeting.taskID,
                        action: "Finish Meeting",
                        details: $"{User.LoggedInUser.Username} đã hoàn thành meeting \"{meeting.taskName}\" lúc {DateTime.Now}"
                    );


                }
            }

            UpdateButtonState();
            OnMeetingFinished?.Invoke(this, meeting);
            TaskManager.GetInstance().UpdateTask(meeting);
        }


        private void taskContent_Click(object sender, EventArgs e)
        {

        }

        private void hour_Click(object sender, EventArgs e)
        {

        }
    }
}