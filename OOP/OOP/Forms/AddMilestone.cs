using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Taskly.Models;
using Taskly.Presenter;
using Taskly.Presenters;

namespace Taskly.Forms
{
    public partial class AddMilestone : Form, IAddMilestoneView
    {
        private AddMilestonePresenter _presenter;

        public Milestone milestone { get; set; } // [cite: 3, 4]
        public List<Milestone> milestones = new List<Milestone>(); // [cite: 4]

        public AddMilestone() // [cite: 5]
        {
            InitializeComponent();
            _presenter = new AddMilestonePresenter(this); // Initialize presenter with this view
            _presenter.InitializeProjects(); // Call presenter to update project combobox
        }

        public AddMilestone(List<Milestone> list) // [cite: 6]
        {
            this.milestones = list; // [cite: 6, 7]
            InitializeComponent(); // Ensure components are initialized when using this constructor
            _presenter = new AddMilestonePresenter(this);
            _presenter.InitializeProjects();
        }

        // IAddMilestoneView implementation
        public string MilestoneName
        {
            get { return txtbMilestoneName.Text; } // [cite: 7]
            set { txtbMilestoneName.Text = value; }
        }

        public DateTime MilestoneDeadline
        {
            get { return dtpMilestonedate.Value; } // [cite: 8]
            set { dtpMilestonedate.Value = value; }
        }

        public string SelectedProjectName
        {
            get { return cbbSelectProject.Text; } // [cite: 8]
            set { cbbSelectProject.Text = value; }
        }

        public void SetProjects(List<string> projectNames)
        {
            cbbSelectProject.Items.Clear(); // [cite: 29]
            foreach (string projectName in projectNames)
            {
                cbbSelectProject.Items.Add(projectName); // [cite: 34]
            }
        }

        public void DisplayErrorMessage(string message)
        {
            MessageBox.Show(message); // [cite: 10, 21]
        }
        public void DisplaySuccessMessage(string message) // Implement phương thức này
        {
            MessageBox.Show(message);
        }
        public void CloseView(Milestone createdMilestone)
        {
            this.milestone = createdMilestone; // [cite: 25]
            this.DialogResult = DialogResult.OK; // [cite: 28]
            this.Close(); // [cite: 28]
        }

        public void SetMilestoneNameError(string message)
        {
            errMilestoneName.SetError(txtbMilestoneName, message); // [cite: 36, 39]
        }

        public void CancelValidation(CancelEventArgs e, bool cancel)
        {
            e.Cancel = cancel; // [cite: 36, 38]
        }

        // Event Handlers (delegates to Presenter)
        private void label1_Click(object sender, EventArgs e) // [cite: 7]
        {
            // Empty as per original code [cite: 7]
        }

        private void AddMilestone_Load(object sender, EventArgs e) // [cite: 7]
        {
            // Empty as per original code [cite: 7]
        }

        private void btnMilestoneConfirm_Click(object sender, EventArgs e) // [cite: 7]
        {
            _presenter.ConfirmMilestone();
        }

        private void btnMilestoneCancel_Click(object sender, EventArgs e) // [cite: 34]
        {
            _presenter.CancelMilestone();
        }

        private void txtbMilestoneName_Validating(object sender, CancelEventArgs e) // [cite: 35]
        {
            _presenter.ValidateMilestoneName(txtbMilestoneName.Text, e);
        }
    }

    // Interface for the View
    public interface IAddMilestoneView
    {
        string MilestoneName { get; set; }
        DateTime MilestoneDeadline { get; set; }
        string SelectedProjectName { get; set; }

        void SetProjects(List<string> projectNames);
        void DisplayErrorMessage(string message);
        void DisplaySuccessMessage(string message);
        void CloseView(Milestone createdMilestone);
        void SetMilestoneNameError(string message);
        void CancelValidation(CancelEventArgs e, bool cancel);
    }
}