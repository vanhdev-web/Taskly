using OOP.Models;
using OOP.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;

using System.IO;
using System.Xml;
using OOP.Usercontrols;
using System.Reflection;

using System.Linq;
using Microsoft.EntityFrameworkCore;
using OOP.Forms;

namespace OOP.Forms
{
    public partial class TaskReport : Form
    {
        private int taskId;

        public TaskReport(int taskId)
        {
            InitializeComponent();
            this.taskId = taskId;
            LoadActivities();
        }

        private void LoadActivities()
        {
            using (var db = new TaskManagementDBContext())
            {
                // Join với Users để lấy username của người action
                var logs = db.ActivityLogs
                             .Where(log => log.ObjectType == "Task" && log.ObjectId == taskId)
                             .Join(db.Users,
                                   log => log.UserId,
                                   user => user.ID,
                                   (log, user) => new
                                   {
                                       log.Timestamp,
                                       log.Action,
                                       log.Details,
                                       Username = user.Username ?? "Unknown"
                                   })
                             .OrderByDescending(x => x.Timestamp)
                             .ToList();

                listViewActivityLog.Items.Clear();
                listViewActivityLog.Columns.Clear();

                // Set kiểu hiển thị là Details
                listViewActivityLog.View = View.Details;

                // Thêm các cột
                listViewActivityLog.Columns.Add("Time", 150);
                listViewActivityLog.Columns.Add("User", 120);
                listViewActivityLog.Columns.Add("Action", 120);
                listViewActivityLog.Columns.Add("Details", 300);

                if (logs.Count == 0)
                {
                    var noItem = new ListViewItem("No activities found for this task.");
                    noItem.SubItems.Add(""); // giữ cột trống cho khớp
                    noItem.SubItems.Add("");
                    noItem.SubItems.Add("");
                    listViewActivityLog.Items.Add(noItem);
                    return;
                }

                // Load dữ liệu vào từng dòng
                foreach (var log in logs)
                {
                    var item = new ListViewItem(log.Timestamp.ToString("dd/MM/yyyy HH:mm")); // Time
                    item.SubItems.Add(log.Username); // User
                    item.SubItems.Add(log.Action);   // Action
                    item.SubItems.Add(log.Details);  // Details

                    listViewActivityLog.Items.Add(item);
                }
            }
        }

        private void TaskReport_Load(object sender, EventArgs e)
        {
            
    }
}
}
