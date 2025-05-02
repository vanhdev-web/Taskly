using System;
using System.ComponentModel;
using System.Windows.Forms;
using Taskly.Models;
using Taskly.Presenter;
using Taskly.Presenters; // AddmemberPresenter

namespace Taskly.Forms
{
    public partial class Addmember : Form, IAddmemberView
    {
        private AddmemberPresenter _presenter;
        private Projects parentForm; // Reference to the parent form

        public Addmember(Projects parent)
        {
            if (parent == null)
            {
                MessageBox.Show("Error: Parent form is null!");
                return;
            }
            this.parentForm = parent;
            InitializeComponent();
            this.Load += Addmember_Load;
            _presenter = new AddmemberPresenter(this); // Initialize presenter with this view
        }

       

        // IAddmemberView implementation
        public string MemberName
        {
            get { return txtUsername?.Text.Trim() ?? string.Empty; }
        }

      

        public void DisplayErrorMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void CloseViewOk()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void SetRoleOptions(string[] roles)
        {
            comboBox1.Items.Clear();
         
        }

        // Event Handlers (delegates to Presenter)
        private void Addmember_Load(object sender, EventArgs e)
        {
            // _presenter.InitializeRoles(); // If roles are reintroduced
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Nothing specific here, presenter doesn't need to react to every text change unless validation is real-time
        }

        public void panel1_Click(object sender, EventArgs e)
        {
            // Nothing specific here
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nothing specific here
        }

        public void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Don't check parentForm directly here, Presenter will handle if parentForm is needed
            _presenter.AddMember(parentForm); // Pass parentForm to Presenter if it needs to interact with it
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void WelcomeName_Click(object sender, EventArgs e) { }
    }

    // Interface for the View
    public interface IAddmemberView
    {
        string MemberName { get; }
        //RoleType SelectedRole { get; } // Uncomment if roles are used
        void DisplayErrorMessage(string message);
        void CloseViewOk();
        void SetRoleOptions(string[] roles);
    }
}