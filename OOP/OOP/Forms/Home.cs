using Microsoft.VisualBasic.ApplicationServices;
using OOP;
using OOP.Models;
using OOP.Services;
using OOP.Usercontrols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using User = OOP.Models.User;

namespace OOP
{
    public partial class Home : BaseForm, IHomeView
    {
        private HomePresenter _presenter;
        private bool sidebarExpand = true;

        public Home()
        {
            InitializeComponent();
            _presenter = new HomePresenter(this);
            //Mouse Hover
            ApplyMouseEvents(TopPanel);
            ApplyMouseEvents(projectPanel);
            ApplyMouseEvents(taskPanel);

            _presenter.LoadHomeData(); // Load all initial data via presenter
        }

        // IHomeView implementations
        public void SetTimeDetail(string time)
        {
            timeDetail.Text = time;
        }

        public void SetWelcomeName(string name)
        {
            WelcomeName.Text = name;
        }

        public void SetAvatarImage(Image image)
        {
            avatar.Image = image;
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ClearTaskContainer()
        {
            taskContainer.Controls.Clear();
        }

        public void AddTaskItem(AbaseTask task)
        {
            HomeTaskUserControl taskItem = new HomeTaskUserControl(task);
            taskItem.Dock = DockStyle.Top; // Stack tasks from top to bottom
            taskContainer.Controls.Add(taskItem);
            ApplyMouseEvents(taskItem.TaskPanel);
        }

        public void ClearProjectContainer()
        {
            projectContainer.Controls.Clear();
        }

        public void AddProjectItem(Project project)
        {
            HomeProjectUserControl projectItem = new HomeProjectUserControl(project);
            projectItem.Dock = DockStyle.Top; // Stack Project from top to bottom
            projectContainer.Controls.Add(projectItem);
            ApplyMouseEvents(projectItem.ProjectPanel);
        }

        public void ApplyMouseEvents(Control control)
        {
            // Existing implementation of ApplyMouseEvents
            // This method directly manipulates UI elements, so it stays in the View.
            // (The actual implementation of ApplyMouseEvents is not provided in the original extract,
            // but assuming it exists in BaseForm or directly in Home.cs)
        }

        public void SwitchForm(Form newForm)
        {
            // Existing implementation of SwitchForm
            // This method directly manipulates UI forms, so it stays in the View.
        }

        public void StartSidebarTransition()
        {
            sidebarTransition.Start();
        }

        // Event Handlers (Delegate to Presenter for logic)
        private void sidebarTransition_Tick(Object sender, EventArgs e)
        {
            _presenter.ToggleSidebar(ref sidebarExpand, sidebar, sidebarTransition);
        }

        private void Home_Load(object sender, EventArgs e)
        {
            // Initial data loading handled by presenter in constructor
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            StartSidebarTransition();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitApplication();
            // Gọi hàm chung để thoát
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            //
        }

        private void WelcomeName_Click(object sender, EventArgs e)
        {
            //
        }
    }
    public interface IHomeView
    {
        void SetTimeDetail(string time);
        void SetWelcomeName(string name);
        void SetAvatarImage(Image image);
        void ShowErrorMessage(string message);
        void ClearTaskContainer();
        void AddTaskItem(AbaseTask task);
        void ClearProjectContainer();
        void AddProjectItem(Project project);
        void ApplyMouseEvents(Control control); // This needs to be in the View as it manipulates UI elements directly.
        void SwitchForm(Form newForm); // This needs to be in the View as it manipulates UI forms directly.
        void StartSidebarTransition(); // To start the sidebar animation
    }
}