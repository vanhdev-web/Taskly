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
    public partial class TaskReport : Form, ITaskReportView
    {
        private int taskId;
        private TaskReportPresenter _presenter;

        public TaskReport(int taskId)
        {
            InitializeComponent();
            this.taskId = taskId;
            _presenter = new TaskReportPresenter(this, taskId);
            _presenter.LoadActivities();
        }

        public void SetActivityLogs(List<ActivityLogEntry> logs)
        {
            foreach (var log in logs)
            {
                var item = new ListViewItem(log.Timestamp.ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(log.Username);
                item.SubItems.Add(log.Action);
                item.SubItems.Add(log.Details);
                listViewActivityLog.Items.Add(item);
            }
        }

        public void DisplayNoActivitiesFoundMessage()
        {
            var noItem = new ListViewItem("No activities found for this task.");
            noItem.SubItems.Add(""); // giữ cột trống cho khớp
            noItem.SubItems.Add("");
            noItem.SubItems.Add("");
            listViewActivityLog.Items.Add(noItem);
        }

        public void ClearActivityLogView()
        {
            listViewActivityLog.Items.Clear();
            listViewActivityLog.Columns.Clear();
            listViewActivityLog.View = View.Details;
        }

        public void SetupActivityLogColumns()
        {
            listViewActivityLog.Columns.Add("Time", 150);
            listViewActivityLog.Columns.Add("User", 120);
            listViewActivityLog.Columns.Add("Action", 120);
            listViewActivityLog.Columns.Add("Details", 300);
        }

        private void TaskReport_Load(object sender, EventArgs e)
        {
            // Initial load handled by constructor, but if there were other load logic, it would go here.
        }
    }
    public interface ITaskReportView
    {
        void SetActivityLogs(List<ActivityLogEntry> logs);
        void DisplayNoActivitiesFoundMessage();
        void ClearActivityLogView();
        void SetupActivityLogColumns();
    }
}