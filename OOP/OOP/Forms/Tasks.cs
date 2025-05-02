using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using OOP.Forms;
using OOP.Models;
using OOP.Services;
using OOP.Usercontrols;
using System.Linq;

namespace OOP
{
    public partial class Tasks : BaseForm
    {
        TaskManager taskManager = TaskManager.GetInstance();
        private ProjectManager projectManager = new ProjectManager();
        public List<AbaseTask> GetUserTasks()
        {
            List<Project> userProjects = projectManager.FindProjectsByMember(User.LoggedInUser);
            List<AbaseTask> userTasks = new List<AbaseTask>();

            if (userProjects.Count == 0)
            {
                Console.WriteLine("User không thuộc bất kỳ project nào.");
                return userTasks; // Trả về danh sách rỗng nếu user không có project
            }


            using (var db = new TaskManagementDBContext())
            {
                int userId = User.LoggedInUser.ID;

                // lấy các task mà được assign cho user
                var tasks = db.Tasks
                              .Where(t => t.AssignedTo == userId)
                              .ToList();

                userTasks.AddRange(tasks);

                // lấy các meeting mà được assign cho user
                var meetings = db.Meetings
                                 .Where(m => m.AssignedTo == userId)
                                 .ToList();

                userTasks.AddRange(meetings);

                // lấy các milestone mà được assign cho user
                var milestones = db.Milestones
                                   .Where(ms => ms.AssignedTo == userId)
                                   .ToList();

                userTasks.AddRange(milestones);
            }

            return userTasks;
        }
        public Tasks()
        {
            InitializeComponent();
            LoadTasks(GetUserTasks());
            // Apply mouse events
            ApplyMouseEvents(taskContainer);
            ApplyMouseEvents(sidebar);
            ApplyMouseEvents(TopPanel);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            foreach (ToolStripItem tsi in menuStrip1.Items)
            {
                if (tsi == e.ClickedItem)
                {
                    tsi.BackColor = SystemColors.ControlDark;

                }
                else
                {
                    tsi.BackColor = SystemColors.Control;
                }
            }
        }

        private void mnuList_Click(object sender, EventArgs e)
        {
            if (!mnuList.Checked)
            {
                mnuList.Checked = true;
            }
            else
            {
                mnuList.Checked = false;
            }
            Console.WriteLine(mnuList.Checked);
        }
        private List<Project> projects = new List<Project>();
        private List<User> users = new List<User>();

        private void LoadTasks(List<AbaseTask> tasks)
        {
            // Xóa các control cũ trước khi thêm mới
            taskContainer.Controls.Clear();

            foreach (AbaseTask task in tasks)
            {
                Console.WriteLine(task.taskName);
                if (task is Task t)
                {
                    TasksFullUserControl taskItem = new TasksFullUserControl(t);
                    taskItem.Dock = DockStyle.Top;
                    taskContainer.Controls.Add(taskItem);
                    ApplyMouseEvents(taskItem.TaskPanel);
                }
                else if (task is Meeting m)
                {
                    MeetingUserControl meetingItem = new MeetingUserControl(m);
                    meetingItem.Dock = DockStyle.Top;
                    taskContainer.Controls.Add(meetingItem);
                    ApplyMouseEvents(meetingItem.TaskPanel);
                }
                else if (task is Milestone ms)
                {
                    MilestoneUserControl milestoneItem = new MilestoneUserControl(ms);
                    milestoneItem.Dock = DockStyle.Top;
                    taskContainer.Controls.Add(milestoneItem);
                    ApplyMouseEvents(milestoneItem.TaskPanel);
                }
            }
        }



        private void btnMore_Click_1(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnMore, new Point(20, btnMore.Height));
        }

        private void btnAddTask_Click_1(object sender, EventArgs e)
        {
            Addtask addTaskForm = new Addtask(projects, taskManager.Tasks, users);
            if (addTaskForm.ShowDialog() == DialogResult.OK)
            {
                taskManager.AddTask(addTaskForm.NewTask);
                LoadTasks(GetUserTasks());
            }
        }

        private void ctmCloset_Click(object sender, EventArgs e)
        {
            List<AbaseTask> taskslistother = new List<AbaseTask>();
            foreach (AbaseTask task in (GetUserTasks()))
            {
                taskslistother.Add(task);
            }
            taskslistother.Sort();
            taskslistother.Reverse();
            LoadTasks(taskslistother);
            // RenderTasks(tasks);
        }

        private void ctmFarest_Click(object sender, EventArgs e)
        {
            List<AbaseTask> taskslistother = new List<AbaseTask>();
            foreach (AbaseTask task in (GetUserTasks()))
            {
                taskslistother.Add(task);
            }
            taskslistother.Sort();
            //RenderTasks(tasks);
            LoadTasks(taskslistother);
        }

        private void ctmFinished_Click(object sender, EventArgs e)
        {


            using (var dbcontext = new TaskManagementDBContext())
            {
                var task = (from t in dbcontext.Tasks
                            where t.AssignedTo == User.LoggedInUser.ID && t.status == "Finished"
                            select t).ToList<AbaseTask>();
                var meetings = (from m in dbcontext.Meetings
                                where m.AssignedTo == User.LoggedInUser.ID && m.status == "Finished"
                                select m).ToList<AbaseTask>();
                var milestones = (from ms in dbcontext.Milestones
                                  where ms.AssignedTo == User.LoggedInUser.ID && ms.status == "Finished"
                                  select ms).ToList<AbaseTask>();
                var taskslistother = task.Union(meetings).Union(milestones).ToList();



                LoadTasks(taskslistother);
            }
        }

        private void ctnSection_Click(object sender, EventArgs e)
        {

            using (var dbcontext = new TaskManagementDBContext())
            {
                var task = (from t in dbcontext.Tasks
                            where t.AssignedTo == User.LoggedInUser.ID && t.status != "Finished"
                            select t).ToList<AbaseTask>();
                var meetings = (from m in dbcontext.Meetings
                                where m.AssignedTo == User.LoggedInUser.ID && m.status != "Finished"
                                select m).ToList<AbaseTask>();
                var milestones = (from ms in dbcontext.Milestones
                                  where ms.AssignedTo == User.LoggedInUser.ID && ms.status != "Finished"
                                  select ms).ToList<AbaseTask>();
                var taskslistother = task.Union(meetings).Union(milestones).ToList();



                LoadTasks(taskslistother);
            }

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SwitchForm(new Home());
        }
        private void btnTask_Click(object sender, EventArgs e)
        {
            SwitchForm(new Tasks());
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            SwitchForm(new MainUser());
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            SwitchForm(new Projects());
        }

        private void lblAddoption_Click(object sender, EventArgs e)
        {
            Console.WriteLine(User.LoggedInUser.Username);
            ctmsAddoption.Show(btnAddTask, new Point(20, btnMore.Height));

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private async void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Addtask addTaskForm = new Addtask(projects, taskManager.Tasks, users);
            if (addTaskForm.ShowDialog() == DialogResult.OK && addTaskForm.NewTask != null)
            {
                await taskManager.AddTask(addTaskForm.NewTask);
                LoadTasks(GetUserTasks());
                addTaskForm.NewTask.Message();
            }
        }

        private async void milestoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMilestone addMilestone = new AddMilestone();
            if (addMilestone.ShowDialog() == DialogResult.OK && addMilestone.milestone != null)
            {
                await taskManager.AddTask(addMilestone.milestone);
                LoadTasks(GetUserTasks());
                addMilestone.milestone.Message();
            }
        }

        private async void meetingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMeeting addMeeting = new AddMeeting(users);
            if (addMeeting.ShowDialog() == DialogResult.OK && addMeeting.newMeeting != null)
            {
                await taskManager.AddTask(addMeeting.newMeeting);
                LoadTasks(GetUserTasks());
                addMeeting.newMeeting.Message();
            }
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitApplication(); // Gọi hàm chung để thoát
        }
    }
}