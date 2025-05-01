using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
<<<<<<< HEAD
using System.Windows.Forms;
using OOP.Services;
using User = OOP.Models.User;
using OOP.Models;
=======
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP.Models; // ModelUser, Meeting
using ModelUser = OOP.Models.User; // Alias for clarity
using OOP.Presenter; // AddMeetingPresenter

>>>>>>> MinhTuan
namespace OOP.Forms
{
    public partial class AddMeeting : Form, IAddMeetingView
    {
        private AddMeetingPresenter _presenter;

        public Meeting newMeeting { get; set; } // [cite: 3]
        public List<ModelUser> users { get; set; } // [cite: 4]
        public List<Meeting> meetings { get; set; } // [cite: 5]

        public AddMeeting(List<ModelUser> users)
        {
            InitializeComponent();
            this.users = users; // [cite: 7]
            _presenter = new AddMeetingPresenter(this); // Initialize presenter with this view
            _presenter.InitializeProjects(); // Call presenter to update project combobox
        }

        // IAddMeetingView implementation
        public string MeetingLocation
        {
            get { return txtbMeetingLocation.Text; }
            set { txtbMeetingLocation.Text = value; }
        }

        public DateTime MeetingTime
        {
            get { return dtpMeetingTime.Value; }
            set { dtpMeetingTime.Value = value; }
        }

        public string MeetingHour
        {
            get { return lblHour.Text; }
            set { lblHour.Text = value; }
        }

        public string MeetingName
        {
            get { return txtbMeetingName.Text; }
            set { txtbMeetingName.Text = value; }
        }

        public string SelectedProjectName
        {
            get { return cbbSelectProject.Text; }
            set { cbbSelectProject.Text = value; }
        }

        public void SetProjects(List<string> projectNames)
        {
            cbbSelectProject.Items.Clear(); // [cite: 46]
            foreach (string projectName in projectNames)
            {
                cbbSelectProject.Items.Add(projectName);
            }
        }

        public void DisplayErrorMessage(string message)
        {
            MessageBox.Show(message); // [cite: 27, 38]
        }
        public void DisplaySuccessMessage(string message) // Implement phương thức này
        {
            MessageBox.Show(message);
        }
        public void CloseView(Meeting meeting)
        {
            this.newMeeting = meeting;
            this.DialogResult = DialogResult.OK; // [cite: 45]
            this.Close(); // [cite: 45]
        }

        // Event Handlers (minimal logic, delegates to Presenter)
        private void AddMeeting_Load(object sender, EventArgs e)
        {
            // No direct logic, presenter handles initialization if needed
        }

        private void btnMeetingConfirm_Click(object sender, EventArgs e)
        {
            _presenter.ConfirmMeeting();
        }

        private void txtbMeetingName_Validating(object sender, CancelEventArgs e)
        {
            _presenter.ValidateMeetingName(txtbMeetingName.Text, e); // Delegate validation to presenter
        }

        // Error Provider (remains in View as it's UI specific)
        public void SetMeetingNameError(string message)
        {
            errMeetingName.SetError(txtbMeetingName, message); // [cite: 52, 54]
        }

        public void CancelValidation(CancelEventArgs e, bool cancel)
        {
            e.Cancel = cancel; // [cite: 52, 53]
        }
    }

    // Interface for the View
    public interface IAddMeetingView
    {
        string MeetingLocation { get; set; }
        DateTime MeetingTime { get; set; }
        string MeetingHour { get; set; }
        string MeetingName { get; set; }
        string SelectedProjectName { get; set; }

        void SetProjects(List<string> projectNames);
        void DisplayErrorMessage(string message);
        void DisplaySuccessMessage(string message); // Thêm phương thức này
        void CloseView(Meeting meeting);
        void SetMeetingNameError(string message);
        void CancelValidation(CancelEventArgs e, bool cancel);
    }
}