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

namespace OOP.Usercontrols
{
    public partial class HomeTaskUserControl : UserControl
    {
        private AbaseTask task;  // Tham chiếu đến Task gốc
        public event EventHandler<AbaseTask> OnTaskCompleted;  // Fix kiểu event

        public Panel TaskPanel // Thuộc tính công khai để truy cập Panel
        {
            get { return panel9; } // panelContainer là tên Panel bên trong TaskControl
        }

        public HomeTaskUserControl(AbaseTask task)
        {
            InitializeComponent();
            this.task = task;
            UpdateUI();
        }

        private void UpdateUI()
        {
            taskContent.Text = task.taskName;
            taskDeadline.Text = $"{task.deadline:dd/MM}";  // Fix typo từ dealine -> deadline
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

        private void checkBox_Click_1(object sender, EventArgs e)
        {
            if (task.status == "Finished")
            {
                task.status = "Unfinished"; // Cập nhật trạng thái Task gốc
                TaskManager.GetInstance().UpdateTask(task);
            }
            else
            {
                task.status = "Finished"; // Cập nhật trạng thái Task gốc
                TaskManager.GetInstance().UpdateTask(task);
            }

            UpdateButtonState();
            OnTaskCompleted?.Invoke(this, task); // Đúng kiểu event
                                                 // Chỉ gửi thông báo nếu trạng thái thực sự thay đổi thành "Finished"
            //if (task.status == "Finished")
            //{
            //    NotificationManager.Instance.SendTaskUpdateNotification(User.GetLoggedInUserName(), task.taskName, task.status);
            //}
        }
    }
}
