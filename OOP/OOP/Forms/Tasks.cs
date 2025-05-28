using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OOP.Forms;
using OOP.Models;
using OOP.Services;
using OOP.Usercontrols;

namespace OOP
{
    public partial class Tasks : BaseForm
    {
        TaskManager taskManager = TaskManager.GetInstance();
        private ProjectManager projectManager = new ProjectManager();

        public Tasks()
        {
            InitializeComponent();
            LoadTasks(taskManager.GetUserTasks(User.LoggedInUser, projectManager));
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
                if (task is Models.Task t)
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
                LoadTasks(taskManager.GetUserTasks(User.LoggedInUser, projectManager));
            }
        }

        private void ctmCloset_Click(object sender, EventArgs e)
        {
            List<AbaseTask> taskslistother = new List<AbaseTask>();
            foreach (AbaseTask task in (taskManager.GetUserTasks(User.LoggedInUser, projectManager)))
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
            foreach (AbaseTask task in (taskManager.GetUserTasks(User.LoggedInUser, projectManager)))
            {
                taskslistother.Add(task);
            }
            taskslistother.Sort();
            //RenderTasks(tasks);
            LoadTasks(taskslistother);
        }

        private void ctmFinished_Click(object sender, EventArgs e)
        {
            LoadTasks(taskManager.GetFinishedTasks(User.LoggedInUser));
        }

        private void ctnSection_Click(object sender, EventArgs e)
        {
            LoadTasks(taskManager.GetUnfinishedTasks(User.LoggedInUser));
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
                LoadTasks(taskManager.GetUserTasks(User.LoggedInUser, projectManager));
                addTaskForm.NewTask.Message();
            }
        }

        private async void milestoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMilestone addMilestone = new AddMilestone();
            if (addMilestone.ShowDialog() == DialogResult.OK && addMilestone.milestone != null)
            {
                await taskManager.AddTask(addMilestone.milestone);
                LoadTasks(taskManager.GetUserTasks(User.LoggedInUser, projectManager));
                addMilestone.milestone.Message();
            }
        }

        private async void meetingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMeeting addMeeting = new AddMeeting(users);
            if (addMeeting.ShowDialog() == DialogResult.OK && addMeeting.newMeeting != null)
            {
                await taskManager.AddTask(addMeeting.newMeeting);
                LoadTasks(taskManager.GetUserTasks(User.LoggedInUser, projectManager));
                addMeeting.newMeeting.Message();
            }
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitApplication();
            // Gọi hàm chung để thoát
        }
    }
}