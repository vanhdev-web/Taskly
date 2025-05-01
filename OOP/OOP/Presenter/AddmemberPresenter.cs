using OOP.Forms;
using OOP.Models;
using OOP.Services;
using System;
using System.Windows.Forms;

namespace OOP.Presenters
{
    public class AddmemberPresenter
    {
        private IAddmemberView _view;
        private UserService _userService;

        public AddmemberPresenter(IAddmemberView view)
        {
            _view = view;
            _userService = new UserService();
        }

        public void InitializeRoles()
        {
            // If you decide to re-introduce roles, uncomment this
            // _view.SetRoleOptions(new string[] { RoleType.Member.ToString() });
        }

        public void AddMember(Projects parentForm) // Accept parentForm if interaction with it is needed
        {
            string username = _view.MemberName;
            if (string.IsNullOrWhiteSpace(username))
            {
                _view.DisplayErrorMessage("Please enter a username.");
                return;
            }

            // In the original code, there was no explicit database add call here.
            // It just prepared the data and signaled OK.
            // The responsibility for adding the user to the database might be handled by the calling form (e.g., Projects form)
            // or a different part of the application, after Addmember form closes with DialogResult.OK.

            // Simulate the old behavior: just close the form with OK result
            _view.CloseViewOk();
        }
    }
}