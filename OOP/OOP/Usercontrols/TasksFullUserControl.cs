using System;

using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using OOP.Models;
using OOP.Services;
using OOP.Usercontrols;
using Task = OOP.Models.Task;
namespace OOP.Usercontrols
{
    public partial class TasksFullUserControl : UserControl
    {
        private Task task;  // Tham chiếu đến Task gốc
        public event EventHandler<Task> OnTaskFinished;

        public Panel TaskPanel // Thuộc tính công khai để truy cập Panel
        {
            get { return panel9; } // panelContainer là tên Panel bên trong TaskControl
        }
        public TasksFullUserControl(Task task)
        {
            InitializeComponent();
            this.task = task;
            UpdateUI();
        }

        private void UpdateUI()
        {
            taskContent.Text = task.taskName;
            taskDeadline.Text = $"{task.deadline:dd/MM/yyyy}";
            //taskProject.Text = task.ProjectName;
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private async void checkBox_Click(object sender, EventArgs e)
        {
           using (var dbcontext = new TaskManagementDBContext())
            {
                MessageBox.Show(task.status);

                if (task.status == "Finished")
                {
    
                    task.status = "Unfinished"; // Cập nhật trạng thái Task gốc
             
                }
                else
                {


                    task.status = "Finished"; // Cập nhật trạng thái Task gốc
                    ActivityLogService activityLogService = new ActivityLogService(dbcontext);
                    await activityLogService.LogActivityAsync(userId: null, objectType: "Task", objectId:task.taskID, action: "Finish Task", details: $"{User.LoggedInUser.Username} đã hoàn thành task {task.taskName} lúc {DateTime.Now}");
                    MessageBox.Show("Activitlog hoàn thành task");
                }
                dbcontext.Tasks.Attach(task);
                dbcontext.Entry(task).State = EntityState.Modified;
                dbcontext.SaveChanges();
            }
            UpdateButtonState();
            MessageBox.Show(task.status);
            OnTaskFinished?.Invoke(this, task);
            //TaskManager.GetInstance().UpdateTask(task);
            // Chỉ gửi thông báo nếu trạng thái thực sự thay đổi thành "Finished"
            if (task.status == "Finished")
            {
                NotificationManager.Instance.SendTaskUpdateNotification(User.GetLoggedInUserName(), task.taskName, task.status);
            }
        }
    }
}
