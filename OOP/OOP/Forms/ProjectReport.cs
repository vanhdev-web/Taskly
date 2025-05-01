using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using OOP.Models;
using OOP.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace OOP.Forms
{
    public partial class ProjectReport : Form
    {
        public ProjectReport()
        {
            InitializeComponent();
        }

        private void ProjectReport_Load(object sender, EventArgs e)
        {
            LoadListProject();
        }

        private void LoadListProject()
        {
            using (var context = new TaskManagementDBContext())
            {
                int userId = User.LoggedInUser.ID;

                // Lấy project mà user là admin
                var adminProjects = from p in context.Projects
                                    where p.AdminID == userId
                                    select p;

                // Lấy project từ các Task được gán cho user
                var taskProjects = from t in context.Tasks
                                   where t.AssignedTo == userId
                                   join p in context.Projects on t.ProjectID equals p.projectID
                                   select p;

                // Lấy project từ các Meeting được gán cho user
                var meetingProjects = from m in context.Meetings
                                      where m.AssignedTo == userId
                                      join p in context.Projects on m.ProjectID equals p.projectID
                                      select p;

                // Lấy project từ các Milestone được gán cho user
                var milestoneProjects = from m in context.Milestones
                                        where m.AssignedTo == userId
                                        join p in context.Projects on m.ProjectID equals p.projectID
                                        select p;

                // Hợp nhất tất cả các project và loại bỏ trùng
                var allProjects = adminProjects
                                  .Union(taskProjects)
                                  .Union(meetingProjects)
                                  .Union(milestoneProjects)
                                  .Distinct()
                                  .ToList();

                // Gán vào ComboBox (chỉ lấy tên)
                comboBoxProjectList.DataSource = allProjects;
                comboBoxProjectList.DisplayMember = "projectName";
                comboBoxProjectList.ValueMember = "projectID";
            }
        }


        private void comboBoxProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Safe check if nothing is selected
            if (comboBoxProjectList.SelectedItem == null)
                return;

            // Cast SelectedItem to Project directly
            var selectedProject = comboBoxProjectList.SelectedItem as Project;
            if (selectedProject == null)
            {
                listViewActivityLog.Items.Clear();
                listViewActivityLog.Items.Add("Project not found!");
                return;
            }

            using (var context = new TaskManagementDBContext())
            {
                var projectId = selectedProject.projectID;

                // Get activities for the selected project
                var ProjectActivities = context.ActivityLogs
                    .Where(a => a.ObjectType == "Project" && a.ObjectId == projectId)
                    .ToList();

                var TaskActivities = context.ActivityLogs
                    .Where(a => a.ObjectType == "Task")
                    .Join(context.Tasks, a => a.ObjectId, t => t.taskID, (a, t) => new { a, t })
                    .Where(x => x.t.ProjectID == projectId)
                    .Select(x => x.a)

                    .Concat(
                        context.ActivityLogs
                        .Where(a => a.ObjectType == "Meeting")
                        .Join(context.Meetings, a => a.ObjectId, m => m.taskID, (a, m) => new { a, m })
                        .Where(x => x.m.ProjectID == projectId)
                        .Select(x => x.a)
                    )
                    .Concat(
                        context.ActivityLogs
                        .Where(a => a.ObjectType == "Milestone") // double-check spelling again
                        .Join(context.Milestones, a => a.ObjectId, ms => ms.taskID, (a, ms) => new { a, ms })
                        .Where(x => x.ms.ProjectID == projectId)
                        .Select(x => x.a)
                    )
                    .ToList();

                listViewActivityLog.Items.Clear();

                if (!ProjectActivities.Any() && !TaskActivities.Any())
                {
                    listViewActivityLog.Items.Add("This project does not have any activities!");
                }
                else
                {
                    foreach (var pa in ProjectActivities)
                        listViewActivityLog.Items.Add(pa.Details);

                    foreach (var ta in TaskActivities)
                        listViewActivityLog.Items.Add(ta.Details);
                }
            }
        }




        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
        }
    }
}
