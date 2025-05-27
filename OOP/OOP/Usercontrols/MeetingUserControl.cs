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
using OOP.Models;
using OOP.Services;

namespace OOP.Usercontrols
{
    public partial class MeetingUserControl : UserControl
    {
        private Meeting meeting;  // Tham chiếu đến Meeting gốc
        public event EventHandler<Meeting> OnMeetingFinished;

        public Panel TaskPanel // Thuộc tính công khai để truy cập Panel
        {
            get { return panel9; } // panelContainer là tên Panel bên trong MeetingUserControl
        }

        public MeetingUserControl(Meeting meeting)
        {
            InitializeComponent();
            this.meeting = meeting;
            UpdateUI();
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
            MessageBox.Show(meeting.status);
            using(var dbcontext = new TaskManagementDBContext())
            {
                if (meeting.status == "Finished")
                {
                    meeting.status = "UnFinished"; // Cập nhật trạng thái Meeting gốc
                }
                else
                {
                    meeting.status = "Finished"; // Cập nhật trạng thái Meeting gốc
                    ActivityLogService activityLogService = new ActivityLogService(dbcontext);
                    await activityLogService.LogActivityAsync(userId: null, objectType: "Task", objectId: meeting.taskID, action: "Finish Task", details: $"{User.LoggedInUser.Username} đã tham gia cuộc họp {meeting.taskName} lúc {DateTime.Now}");
                    MessageBox.Show("Activitlog hoàn thành task");
                }
                dbcontext.Meetings.Attach(meeting);
                dbcontext.Entry(meeting).State = EntityState.Modified;
                dbcontext.SaveChanges();

            }
            MessageBox.Show(meeting.status);

            UpdateButtonState();
            OnMeetingFinished?.Invoke(this, meeting);
            TaskManager.GetInstance().UpdateTask(meeting);

            // Chỉ gửi thông báo nếu trạng thái thực sự thay đổi thành "Finished"
            //if (meeting.status == "Finished")
            //{
            //    NotificationManager.Instance.SendTaskUpdateNotification(User.GetLoggedInUserName(), meeting.taskName, meeting.status);
            //}
        }


        private void taskContent_Click(object sender, EventArgs e)
        {

        }

        private void hour_Click(object sender, EventArgs e)
        {

        }
    }
}