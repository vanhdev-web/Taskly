using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Taskly.Models; // Assuming Project and User are in Taskly.Models
using Taskly.Services; // For TaskManagementDBContext and User.LoggedInUser

namespace Taskly.Forms
{
    public partial class ProjectReport : Form, IProjectReportView
    {
        private ProjectReportPresenter _presenter;

        public ProjectReport()
        {
            InitializeComponent();
            // Assuming User.LoggedInUser.ID is available for the current user
            _presenter = new ProjectReportPresenter(this, User.LoggedInUser.ID);
        }

        private void ProjectReport_Load(object sender, EventArgs e)
        {
            _presenter.LoadProjects();
        }

        public void SetProjectList(List<Project> projects)
        {
            comboBoxProjectList.DataSource = projects;
            comboBoxProjectList.DisplayMember = "projectName";
            comboBoxProjectList.ValueMember = "projectID";
        }

        public void SetActivityLogs(List<ActivityLogEntry> logs)
        {
            listViewActivityLog.Items.Clear(); // Clear existing "No activities" message if any
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
            listViewActivityLog.Items.Clear();
            var noItem = new ListViewItem("This project does not have any activities!");
            noItem.SubItems.Add("");
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

        public void DisplayProjectNotFoundMessage()
        {
            listViewActivityLog.Items.Clear();
            listViewActivityLog.Items.Add("Project not found!");
        }

        private void comboBoxProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Safe check if nothing is selected
            if (comboBoxProjectList.SelectedItem == null)
                return;

            var selectedProject = comboBoxProjectList.SelectedItem as Project;
            _presenter.OnProjectSelected(selectedProject);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Existing empty handler
        }
    }
    public interface IProjectReportView
    {
        void SetProjectList(List<Project> projects);
        void SetActivityLogs(List<ActivityLogEntry> logs);
        void DisplayNoActivitiesFoundMessage();
        void ClearActivityLogView();
        void SetupActivityLogColumns();
        void DisplayProjectNotFoundMessage();
    }
}