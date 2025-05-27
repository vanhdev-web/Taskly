using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP.Models;

namespace OOP
{
    public partial class NotiUserControl : UserControl
    {
        public NotiUserControl()
        {
            InitializeComponent();
        }
        public void SetNotificationData(Notification notification)
        {
            if (notification == null) return;
            Avatar.Image = Properties.Resources.defaultProjectPic; // Dùng avatar mặc định
            Title.Text = string.IsNullOrEmpty(notification.Title) ? "Không xác định" : notification.Title;
            SenderName.Text = string.IsNullOrEmpty(notification.Owner.Username) ? "Không xác định" : notification.Owner.Username;
            SendDate.Text = notification.CreateAt.ToString("dd/MM/yyyy HH:mm");
            Content.Text = string.IsNullOrEmpty(notification.Content) ? "Không có nội dung" : notification.Content;
        }
        private void Avatar_Click(object sender, EventArgs e)
        {

        }

        private void Content_Click(object sender, EventArgs e)
        {

        }
    }
}