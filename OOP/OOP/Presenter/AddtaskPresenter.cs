using OOP.Forms;
using OOP.Models;
using ModelUser = OOP.Models.User;
using OOP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OOP.Presenter
{
    public class AddtaskPresenter
    {
        private IAddtaskView _view;
        private TaskService _taskService;
        private ProjectManager _projectManager;
        private UserService _userService;

        public AddtaskPresenter(IAddtaskView view)
        {
            _view = view;
            _taskService = new TaskService();
            _projectManager = new ProjectManager();
            _userService = new UserService();
        }

        public void InitializeData()
        {
            InitializeProjectsComboBox();
            // The original code in Addtask_Load also populated assigned users immediately
            // based on the first selected project. We'll replicate that.
            if (!string.IsNullOrEmpty(_view.SelectedProjectName))
            {
                UpdateAssignedUserComboBox(_view.SelectedProjectName);
            }
        }

        private void InitializeProjectsComboBox()
        {
            List<string> projectNames = new List<string>();

            if (ModelUser.LoggedInUser == null) return;

            foreach (Project project in _projectManager.Projects)
            {
                if (project == null || project.members == null) continue;

                Console.WriteLine($"Project: {project.projectID} - {project.projectName}, AdminID: {project.AdminID}, Members: {string.Join(", ", project.members)}");

                bool isMember = false;
                // Original code had a commented out loop to check if logged-in user is a member of the project
                // For MVP, we'll implement this logic properly if needed.
                // Assuming "members" is a list of strings like "username (id)".
                foreach (string member in project.members)
                {
                    string memberUsername = member.Split('(')[0].Trim();
                    if (memberUsername == ModelUser.LoggedInUser.Username)
                    {
                        isMember = true;
                        break;
                    }
                }

                if (project.AdminID == ModelUser.LoggedInUser.ID || isMember)
                {
                    projectNames.Add($"{project.projectName}");
                }
            }
            _view.SetProjectOptions(projectNames);

            // Select the first project if available, as in original logic
            if (projectNames.Count > 0)
            {
                _view.SelectedProjectName = projectNames[0];
            }
        }

        public void HandleProjectSelectionChanged(string projectName)
        {
            // Original code used `savedProjectName` field. We pass it directly now.
            UpdateAssignedUserComboBox(projectName);
        }

        private void UpdateAssignedUserComboBox(string projectName)
        {
            List<string> userNames = new List<string>();

            int selectedProjectId = _taskService.GetProjectIdByName(projectName);

            if (selectedProjectId == 0) return;

            List<ModelUser> usersInProject = _taskService.GetProjectMembers(selectedProjectId);

            foreach (var user in usersInProject)
            {
                userNames.Add(user.Username);
            }
            _view.SetAssignedUserOptions(userNames);
        }


        public void ConfirmTask()
        {
            string taskName = _view.TaskName;
            DateTime deadline = _view.TaskDeadline;
            string projectName = _view.SelectedProjectName;
            string receiverUsername = _view.SelectedAssignedUser;

            if (string.IsNullOrWhiteSpace(taskName) || string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(receiverUsername))
            {
                _view.DisplayErrorMessage("Please fill in all fields.");
                return;
            }

            int projectId = _taskService.GetProjectIdByName(projectName);
            if (projectId == 0)
            {
                _view.DisplayErrorMessage("Project not found.");
                return;
            }

            int receiverID = 0;
            if (receiverUsername == "Myself")
            {
                receiverID = ModelUser.LoggedInUser.ID;
            }
            else
            {
                // Retrieve user from database based on username and project membership
                ModelUser matchedUser = _taskService.GetUserByUsernameAndProject(receiverUsername, projectId);

                if (matchedUser == null)
                {
                    _view.DisplayErrorMessage($"{receiverUsername} is not a member of this project.");
                    return;
                }
                receiverID = matchedUser.ID;
            }

            // Create and save the new task using the service
            Task newTask = _taskService.CreateTask(taskName, deadline, receiverID, projectId);

            _view.CloseView(newTask);
        }

        public void CancelTask()
        {
            _view.CancelValidation(new CancelEventArgs(), true); // Simulating cancel button behavior
            _view.CloseView(null); // Close with a null task or specific cancel result
        }

        public void ValidateTaskName(string taskName, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(taskName))
            {
                _view.CancelValidation(e, true); // Chặn chuyển focus nếu input trống
                _view.SetTaskNameError("Please enter task name!");
            }
            else
            {
                _view.CancelValidation(e, false); // Cho phép focus rời khỏi control
                _view.SetTaskNameError(null);
            }
        }
    }
}