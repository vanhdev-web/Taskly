using Taskly.Models;
using System;
using System.Windows.Forms;
using System.Threading.Tasks; // Required for async
using Taskly.Services;

namespace Taskly.Presenter
{
    public class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly LoginService _model;
        private bool isLoggingIn = false;

        public LoginPresenter(ILoginView view, LoginService model)
        {
            _view = view;
            _model = model;

            // Unsubscribe before subscribing to avoid double-binding
            _view.LoginAttempt -= OnLoginAttempt;
            _view.LoginAttempt += OnLoginAttempt;

            _view.RegisterRequested -= OnRegisterRequested;
            _view.RegisterRequested += OnRegisterRequested;

            _view.UsernameKeyPress -= OnUsernameKeyPress;
            _view.UsernameKeyPress += OnUsernameKeyPress;

            _view.PasswordKeyPress -= OnPasswordKeyPress;
            _view.PasswordKeyPress += OnPasswordKeyPress;

            _view.LoginLoad -= OnLoginLoad;
            _view.LoginLoad += OnLoginLoad;
        }

        private async void OnLoginAttempt(object sender, EventArgs e)
        {
            if (isLoggingIn) return;
            isLoggingIn = true;

            try
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
                    await _model.LogActivity(userId: null, objectType: "User", objectId: user.ID, action: "LoggedIn", details: $"User Name : {user.Username}");

                }
                else
                {
                    _view.ShowMessage("Sai tài khoản hoặc mật khẩu");
                    _view.ClearUsername();
                    _view.ClearPassword();
                    _view.FocusUsername();
                }
            }
            finally
            {
                isLoggingIn = false;
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

                OnLoginAttempt(sender, e);
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

                OnLoginAttempt(sender, e);
                e.Handled = true;
            }
        }

        private void OnLoginLoad(object sender, EventArgs e)
        {
            _model.EnsureDatabaseCreated();
            // Optional debug commands (commented out):
            // context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'Tasks'");
            // context.Database.ExecuteSqlRaw("delete from sqlite_sequence where name = 'Meetings'");
            // MessageBox.Show("Cơ sở dữ liệu đã được làm mới. Vui lòng đăng nhập lại để tiếp tục.");
        }
    }
}
