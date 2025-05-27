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
    public partial class ProjectActivityFeed : Form
    {
        public ProjectActivityFeed()
        {
            InitializeComponent();
        }

        private void ProjectActivityFeed_Load(object sender, EventArgs e)
        {
            LoadListProject();
        }

        private void LoadListProject()
        {
            using (var context = new TaskManagementDBContext())
            {
                // LINQ query syntax để lấy tên các dự án
                var projectNames = from p in context.Projects
                                       //where p.AdminID == User.LoggedInUser.ID
                                   select p.projectName;

                // Gán vào ComboBox
                comboBoxProjectList.DataSource = projectNames.ToList();
            }
        }

        private void comboBoxProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string projectName = comboBoxProjectList.SelectedItem.ToString();

            using (var context = new TaskManagementDBContext())
            {
                var selectedProject = (from p in context.Projects
                                       where p.projectName == projectName
                                       select p).FirstOrDefault();

                var ProjectActivities = from a in context.ActivityLogs
                                        where a.ObjectType == "Project" && a.ObjectId == selectedProject.projectID
                                        select a;
                var TaskActivities =
                                        (from a in context.ActivityLogs
                                         where a.ObjectType == "Task"
                                         join t in context.Tasks on a.ObjectId equals t.taskID
                                         where t.ProjectID == selectedProject.projectID
                                         select a)
                                        .Concat(
                                            from a in context.ActivityLogs
                                            where a.ObjectType == "Meeting"
                                            join m in context.Meetings on a.ObjectId equals m.taskID
                                            where m.ProjectID == selectedProject.projectID
                                            select a)

                                        .Concat(
                                            from a in context.ActivityLogs
                                            where a.ObjectType == "MileStone"
                                            join ms in context.Milestones on a.ObjectId equals ms.taskID
                                            where ms.ProjectID == selectedProject.projectID
                                            select a);
                listViewActivityLog.Items.Clear();
                foreach (var pa in ProjectActivities)
                {
                    listViewActivityLog.Items.Add(pa.Details);
                }
                foreach (var ta in TaskActivities)
                {
                    listViewActivityLog.Items.Add(ta.Details);
                }
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
        }
    }
}
