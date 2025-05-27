using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OOP.Models; // Ensure this points to your Models namespace for Task, Project, User
using ModelUser = OOP.Models.User; // Alias for clarity
using OOP.Presenter; // AddtaskPresenter
using System.Linq; // Ensure LINQ is available for queries


namespace OOP.Forms
{
    public partial class Addtask : Form, IAddtaskView
    {
        private AddtaskPresenter _presenter;

        // Keeping these properties as they were in the original constructor,
        // though in a strict MVP, the Presenter would ideally manage data directly
        // via services and pass only necessary info to the View.
        // However, user requested to keep old methods/variables.
        public Models.Task NewTask { get; set; } // [cite: 2]
        List<Project> projects; // [cite: 3]
        List<AbaseTask> tasks; // [cite: 3] (Assuming AbaseTask is the base type, will use Models.Task internally for clarity)
        List<ModelUser> users; // [cite: 3]

        public Addtask(List<Project> projects, List<AbaseTask> tasks, List<ModelUser> users) // [cite: 4]
        {
            InitializeComponent(); // [cite: 4]
            this.projects = projects; // [cite: 5]
            this.tasks = tasks; // [cite: 5]
            this.users = users; // [cite: 5]
            _presenter = new AddtaskPresenter(this);
            _presenter.InitializeData(); // Call presenter to initialize UI elements
        }

        // IAddtaskView implementation
        public string TaskName
        {
            get { return txtbInputNameTask.Text.Trim(); } // [cite: 23]
            set { txtbInputNameTask.Text = value; }
        }

        public DateTime TaskDeadline
        {
            get { return dtpNewTask.Value; } // [cite: 24]
            set { dtpNewTask.Value = value; }
        }

        public string SelectedProjectName
        {
            get { return cbbSelectProject.Text; } // [cite: 24]
            set { cbbSelectProject.Text = value; }
        }

        public string SelectedAssignedUser
        {
            get { return cbbAssignedUser.Text.Trim(); } // [cite: 24]
            set { cbbAssignedUser.Text = value; }
        }

        public void SetProjectOptions(List<string> projectNames)
        {
            cbbSelectProject.Items.Clear(); // [cite: 8]
            foreach (string projectName in projectNames) // [cite: 9]
            {
                cbbSelectProject.Items.Add(projectName); // [cite: 9]
            }
        }

        public void SetAssignedUserOptions(List<string> userNames)
        {
            cbbAssignedUser.Items.Clear(); // [cite: 19]
            cbbAssignedUser.Items.Add("Myself"); // [cite: 20]
            foreach (var user in userNames) // [cite: 20]
            {
                cbbAssignedUser.Items.Add(user); // [cite: 20]
            }
        }

        public void DisplayErrorMessage(string message)
        {
            MessageBox.Show(message); // [cite: 25, 27, 38]
        }

        public void CloseView(Models.Task createdTask)
        {
            this.NewTask = createdTask; // [cite: 42]
            this.DialogResult = DialogResult.OK; // [cite: 43]
            this.Close(); // [cite: 43]
        }

        public void SetTaskNameError(string message)
        {
            errLoginTask.SetError(txtbInputNameTask, message); // [cite: 62, 64]
        }

        public void CancelValidation(CancelEventArgs e, bool cancel)
        {
            e.Cancel = cancel; // [cite: 61, 63]
        }

        // Event Handlers (delegates to Presenter)
        private void Addtask_Load(object sender, EventArgs e) // [cite: 6]
        {
            // Initial data loading is now handled by _presenter.InitializeData() in constructor.
            // This method might be left empty or used for other view-specific setup not related to data.
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // [cite: 22]
        {
            // Empty as per original code
        }

        private void label1_Click(object sender, EventArgs e) // [cite: 21]
        {
            // Empty as per original code
        }

        private void button1_Click(object sender, EventArgs e) // [cite: 22] (This was the Cancel button)
        {
            _presenter.CancelTask();
        }

        private void btnConfirm_Click(object sender, EventArgs e) // [cite: 22]
        {
            _presenter.ConfirmTask();
        }

        private void cbbSelectProject_SelectionChangeCommitted(object sender, EventArgs e) // [cite: 49]
        {
            // savedProjectName was a private field in the original view.
            // Now, we pass the selected project name to the presenter.
            _presenter.HandleProjectSelectionChanged(cbbSelectProject.Text);
        }

        private void cbbAssignedUser_Click(object sender, EventArgs e) // [cite: 60]
        {
            // Empty as per original code, or potentially trigger _presenter.UpdateMembers() if needed.
            // Original comment was: // UpdateMember();
        }

        private void txtbInputNameTask_Validating(object sender, CancelEventArgs e) // [cite: 61]
        {
            _presenter.ValidateTaskName(txtbInputNameTask.Text, e);
        }

        private void btnConfirm_Validating(object sender, CancelEventArgs e) // [cite: 65]
        {
            // Empty as per original code
        }
    }

    // Interface for the View
    public interface IAddtaskView
    {
        string TaskName { get; set; }
        DateTime TaskDeadline { get; set; }
        string SelectedProjectName { get; set; }
        string SelectedAssignedUser { get; set; }

        void SetProjectOptions(List<string> projectNames);
        void SetAssignedUserOptions(List<string> userNames);
        void DisplayErrorMessage(string message);
        void CloseView(Models.Task createdTask);
        void SetTaskNameError(string message);
        void CancelValidation(CancelEventArgs e, bool cancel);
    }
}