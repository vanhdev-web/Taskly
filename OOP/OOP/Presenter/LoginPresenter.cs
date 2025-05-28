using OOP.Models;
using System;
using System.Windows.Forms;
using System.Threading.Tasks; // Required for async

namespace OOP
{
    public class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly LoginService _model;

        public LoginPresenter(ILoginView view, LoginService model)
        {
            _view = view;
            _model = model;

            // Subscribe to events from the View
            _view.LoginAttempt += OnLoginAttempt;
            _view.RegisterRequested += OnRegisterRequested;
            _view.UsernameKeyPress += OnUsernameKeyPress;
            _view.PasswordKeyPress += OnPasswordKeyPress;
            _view.LoginLoad += OnLoginLoad;
        }

        private async void OnLoginAttempt(object sender, EventArgs e)
        {
            string username = _view.Username;
            string password = _view.Password;

            if (string.IsNullOrEmpty(username))
            {
                _view.ShowMessage("Vui lòng nhập Username.");
                _view.FocusUsername();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                _view.ShowMessage("Vui lòng nhập Password.");
                _view.FocusPassword();
                return;
            }

            User user = await _model.AuthenticateUser(username, password);

            if (user != null)
            {
                User.LoggedInUser = user;
                _view.ShowHomeForm();
                _view.HideView();

                // activitylog đăng nhập
                await _model.LogActivity(userId: null, objectType: "User", objectId: User.LoggedInUser.ID, action: "LoggedIn", details: $"User Name : {User.LoggedInUser.Username}");
                _view.ShowMessage("Activitlog đăng nhập");
            }
            else
            {
                _view.ShowMessage("Sai tài khoản hoặc mật khẩu");
                _view.ClearUsername();
                _view.ClearPassword();
                _view.FocusUsername();
            }
        }

        private void OnRegisterRequested(object sender, EventArgs e)
        {
            _view.ShowRegisterForm();
        }

        private void OnUsernameKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(_view.Username))
                {
                    _view.ShowMessage("Vui lòng nhập Username.");
                    _view.FocusUsername();
                    e.Handled = true;
                    return;
                }

                if (string.IsNullOrEmpty(_view.Password))
                {
                    _view.ShowMessage("Vui lòng nhập Password.");
                    _view.FocusPassword();
                    e.Handled = true;
                    return;
                }

                OnLoginAttempt(sender, e); // Trigger login attempt
                e.Handled = true;
            }
        }

        private void OnPasswordKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(_view.Username))
                {
                    _view.ShowMessage("Vui lòng nhập Username.");
                    _view.FocusUsername();
                    e.Handled = true;
                    return;
                }

                if (string.IsNullOrEmpty(_view.Password))
                {
                    _view.ShowMessage("Vui lòng nhập Password.");
                    _view.FocusPassword();
                    e.Handled = true;
                    return;
                }

                OnLoginAttempt(sender, e); // Trigger login attempt
                e.Handled = true;
            }
        }

        private void OnLoginLoad(object sender, EventArgs e)
        {
            _model.EnsureDatabaseCreated();

            // Original commented-out lines from Login_Load, kept for reference
            // context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'Tasks'");
            // context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'Meetings'");
            // MessageBox.Show("Cơ sở dữ liệu đã được làm mới. Vui lòng đăng nhập lại để tiếp tục.");
        }
    }
}