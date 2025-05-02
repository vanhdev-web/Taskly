using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Taskly.Models;
using Taskly.Services;
using Taskly.Presenter;

namespace Taskly
{
    public partial class Login : Form, ILoginView
    {
        private LoginPresenter _presenter;

        public string Username => txtUsername.Text;
        public string Password => txtPassword.Text;

        public event EventHandler LoginAttempt;
        public event EventHandler RegisterRequested;
        public event KeyPressEventHandler UsernameKeyPress;
        public event KeyPressEventHandler PasswordKeyPress;
        public event EventHandler LoginLoad;

        public Login()
        {
            InitializeComponent();
            _presenter = new LoginPresenter(this, new LoginService()); // Initialize presenter with view and model

            // Attach event handlers
            btnLogin.Click += (sender, e) => LoginAttempt?.Invoke(sender, e);
            btnRegister.Click += (sender, e) => RegisterRequested?.Invoke(sender, e);
            txtUsername.KeyPress += (sender, e) => UsernameKeyPress?.Invoke(sender, e);
            txtPassword.KeyPress += (sender, e) => PasswordKeyPress?.Invoke(sender, e);
            this.Load += (sender, e) => LoginLoad?.Invoke(sender, e);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ClearUsername()
        {
            txtUsername.Clear();
        }

        public void ClearPassword()
        {
            txtPassword.Clear();
        }

        public void FocusUsername()
        {
            txtUsername.Focus();
        }

        public void FocusPassword()
        {
            txtPassword.Focus();
        }

        public void HideView()
        {
            this.Hide();
        }

        public void ShowHomeForm()
        {
            Home mainForm = new Home();
            mainForm.Show();
        }

        public void ShowRegisterForm()
        {
            Register register = new Register();
            register.Show();
        }

        // Keep the original event handlers for design-time and direct access if needed,
        // but the core logic will be handled by the presenter.
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // This will now trigger the LoginAttempt event, handled by the presenter
            //LoginAttempt?.Invoke(sender, e);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // This will now trigger the RegisterRequested event, handled by the presenter
          //  RegisterRequested?.Invoke(sender, e);
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
           // UsernameKeyPress?.Invoke(sender, e);
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
           // PasswordKeyPress?.Invoke(sender, e);
        }

        private void Logintext_Click(object sender, EventArgs e)
        {
            // Original empty method, keep as is
        }

        private void Login_Load(object sender, EventArgs e)
        {
          //  LoginLoad?.Invoke(sender, e);
        }

        // Original SeedData method
        
    }

    // Interface for the View
    public interface ILoginView
    {
        string Username { get; }
        string Password { get; }

        event EventHandler LoginAttempt;
        event EventHandler RegisterRequested;
        event KeyPressEventHandler UsernameKeyPress;
        event KeyPressEventHandler PasswordKeyPress;
        event EventHandler LoginLoad;

        void ShowMessage(string message);
        void ClearUsername();
        void ClearPassword();
        void FocusUsername();
        void FocusPassword();
        void HideView();
        void ShowHomeForm();
        void ShowRegisterForm();
    }
}