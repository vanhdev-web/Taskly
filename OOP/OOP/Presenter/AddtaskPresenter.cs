using OOP.Forms; // IAddtaskView
using OOP.Models; // User, Project, Task (using Models.Task explicitly)
using ModelUser = OOP.Models.User; // Alias for clarity
using OOP.Services; // TaskService, ProjectManager, UserService
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
        private ProjectManager _projectManager; // [cite: 43]
        private UserService _userService; // Used for User.LoggedInUser, etc.

        public AddtaskPresenter(IAddtaskView view)
        {
            _view = view;
            _taskService = new TaskService();
            _projectManager = new ProjectManager(); // [cite: 43]
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

            if (ModelUser.LoggedInUser == null) return; // [cite: 45]

            foreach (Project project in _projectManager.Projects) // [cite: 45]
            {
                if (project == null || project.members == null) continue; // [cite: 45]

                Console.WriteLine($"Project: {project.projectID} - {project.projectName}, AdminID: {project.AdminID}, Members: {string.Join(", ", project.members)}"); // [cite: 46]

                bool isMember = false; // [cite: 47]
                // Original code had a commented out loop to check if logged-in user is a member of the project
                // For MVP, we'll implement this logic properly if needed.
                // Assuming "members" is a list of strings like "username (id)".
                foreach (string member in project.members) // [cite: 47]
                {
                    string memberUsername = member.Split('(')[0].Trim(); // [cite: 47]
                    if (memberUsername == ModelUser.LoggedInUser.Username) // [cite: 47]
                    {
                        isMember = true; // [cite: 48]
                        break; // [cite: 48]
                    }
                }

                if (project.AdminID == ModelUser.LoggedInUser.ID || isMember) // [cite: 49]
                {
                    projectNames.Add($"{project.projectName}"); // [cite: 49]
                }
            }
            _view.SetProjectOptions(projectNames);

            // Select the first project if available, as in original logic
            if (projectNames.Count > 0) // [cite: 10]
            {
                _view.SelectedProjectName = projectNames[0]; // [cite: 10]
            }
        }

        public void HandleProjectSelectionChanged(string projectName) // [cite: 49]
        {
            // Original code used `savedProjectName` field. We pass it directly now.
            UpdateAssignedUserComboBox(projectName);
        }

        private void UpdateAssignedUserComboBox(string projectName) // [cite: 50]
        {
            List<string> userNames = new List<string>();

            int selectedProjectId = _taskService.GetProjectIdByName(projectName); // [cite: 51, 52]

            if (selectedProjectId == 0) return; // [cite: 52]

            List<ModelUser> usersInProject = _taskService.GetProjectMembers(selectedProjectId); // [cite: 52, 53, 54, 55, 56, 57, 58]

            foreach (var user in usersInProject) // [cite: 59]
            {
                userNames.Add(user.Username); // [cite: 59]
            }
            _view.SetAssignedUserOptions(userNames);
        }


        public void ConfirmTask()
        {
            string taskName = _view.TaskName; // [cite: 23]
            DateTime deadline = _view.TaskDeadline; // [cite: 24]
            string projectName = _view.SelectedProjectName; // [cite: 24]
            string receiverUsername = _view.SelectedAssignedUser; // [cite: 24]

            if (string.IsNullOrWhiteSpace(taskName) || string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(receiverUsername)) // [cite: 25]
            {
                _view.DisplayErrorMessage("Please fill in all fields."); // [cite: 25]
                return; // [cite: 26]
            }

            int projectId = _taskService.GetProjectIdByName(projectName); // [cite: 26]
            if (projectId == 0) // [cite: 27]
            {
                _view.DisplayErrorMessage("Project not found."); // [cite: 27]
                return; // [cite: 28]
            }

            int receiverID = 0; // [cite: 28]
            if (receiverUsername == "Myself") // [cite: 29]
            {
                receiverID = ModelUser.LoggedInUser.ID; // [cite: 29]
            }
            else
            {
                // Retrieve user from database based on username and project membership
                ModelUser matchedUser = _taskService.GetUserByUsernameAndProject(receiverUsername, projectId); // [cite: 30, 31, 32, 33, 34, 35, 36, 37]

                if (matchedUser == null) // [cite: 38]
                {
                    _view.DisplayErrorMessage($"{receiverUsername} is not a member of this project."); // [cite: 38]
                    return; // [cite: 39]
                }
                receiverID = matchedUser.ID; // [cite: 39]
            }

            // Create and save the new task using the service
            Task newTask = _taskService.CreateTask(taskName, deadline, receiverID, projectId); // [cite: 40, 41]

            _view.CloseView(newTask); // [cite: 42, 43]
        }

        public void CancelTask()
        {
            _view.CancelValidation(new CancelEventArgs(), true); // Simulating cancel button behavior [cite: 22, 23]
            _view.CloseView(null); // Close with a null task or specific cancel result [cite: 22, 23]
        }

        public void ValidateTaskName(string taskName, CancelEventArgs e) // [cite: 61]
        {
            if (string.IsNullOrWhiteSpace(taskName)) // [cite: 61]
            {
                _view.CancelValidation(e, true); // Chặn chuyển focus nếu input trống [cite: 61, 62]
                _view.SetTaskNameError("Please enter task name!"); // [cite: 62]
            }
            else
            {
                _view.CancelValidation(e, false); // Cho phép focus rời khỏi control [cite: 63, 64]
                _view.SetTaskNameError(null); // [cite: 64]
            }
        }
    }
}