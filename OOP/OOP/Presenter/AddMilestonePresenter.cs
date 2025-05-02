using Taskly.Forms; // IAddMilestoneView
using Taskly.Models; // ModelUser, Milestone, Project
using Taskly.Services; // MilestoneService, ProjectManager
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ModelUser = Taskly.Models.User;

namespace Taskly.Presenters
{
    public class AddMilestonePresenter
    {
        private IAddMilestoneView _view;
        private MilestoneService _milestoneService;
        private ProjectManager _projectManager;
        private ActivityLogService _activityLogService;

        public AddMilestonePresenter(IAddMilestoneView view)
        {
            _view = view;
            _milestoneService = new MilestoneService();
            _projectManager = new ProjectManager();
            _activityLogService = new ActivityLogService(new TaskManagementDBContext());
        }

        public void InitializeProjects()
        {
            List<string> projectNames = new List<string>();

            if (ModelUser.LoggedInUser == null) return; // Kiểm tra user đăng nhập

            foreach (Project project in _projectManager.Projects)
            {
                if (project == null || project.members == null) continue; // Kiểm tra null tránh lỗi

                Console.WriteLine($"Project: {project.projectID} - {project.projectName}, AdminID: {project.AdminID}, Members: {string.Join(", ", project.members)}");

                bool isMember = false;
                // Original code had a commented out loop to check if logged-in user is a member of the project
                // For MVP, we'll keep the logic as it was (commented out) or implement it if required.
                // For now, based on the provided code, it appears 'isMember' would always be false
                // if the loop remains commented out in the original 'UpdateComboBox' logic.
                /*
                foreach (string member in project.members)
                {
                    string memberUsername = member.Split('(')[0].Trim(); // Lấy username trước dấu "(" và Trim()
                    if (memberUsername == ModelUser.LoggedInUser.Username)
                    {
                        isMember = true;
                        break;
                    }
                }
                */

                if (project.AdminID == ModelUser.LoggedInUser.ID || isMember)
                {
                    projectNames.Add($"{project.projectName}");
                }
            }
            _view.SetProjects(projectNames);
        }

        public async void ConfirmMilestone()
        {
            string taskName = _view.MilestoneName;
            DateTime deadline = _view.MilestoneDeadline;
            string projectName = _view.SelectedProjectName;

            int projectId = _milestoneService.GetProjectIdByName(projectName);

            if (projectId == 0)
            {
                _view.DisplayErrorMessage("Dự án không tồn tại.");
                return;
            }
            // Lấy thông tin dự án để ghi log
            Project project = null;
            using (var dbcontext = new TaskManagementDBContext())
            {
                project = (from p in dbcontext.Projects
                           where p.projectID == projectId
                           select p).FirstOrDefault();
            }
            List<ModelUser> members = _milestoneService.GetProjectMembers(projectId);

            if (members.Count == 0)
            {
                _view.DisplayErrorMessage("Không có thành viên nào thuộc dự án này.");
                return;
            }

            // Create the milestone
            Milestone newMilestone = _milestoneService.CreateMilestone(taskName, deadline, projectId);

            // Add milestone members
            _milestoneService.AddMilestoneMembers(newMilestone.taskID, members);
            if (ModelUser.LoggedInUser != null && project != null)
            {
                await _activityLogService.LogActivityAsync(
                    userId: ModelUser.LoggedInUser.ID,
                    objectType: "Milestone",
                    objectId: newMilestone.taskID,
                    action: "Create Milestone",
                    details: $"{ModelUser.LoggedInUser.Username} đã tạo một cột mốc {newMilestone.taskName} cho dự án {project.projectName}"
                );
            }

            // Hiển thị thông báo thành công (thay thế MessageBox.Show)
            _view.CloseView(newMilestone);
        }

        public void CancelMilestone()
        {
            _view.CancelValidation(new CancelEventArgs(), true); // Simulating cancel button
            _view.CloseView(null); // Or close with DialogResult.Cancel, depending on IAddMilestoneView implementation
                                   // The original code sets DialogResult = DialogResult.Cancel and Close()
        }

        public void ValidateMilestoneName(string milestoneName, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(milestoneName))
            {
                _view.CancelValidation(e, true); // Chặn chuyển focus nếu input trống
                _view.SetMilestoneNameError("Please enter task name!");
            }
            else
            {
                _view.CancelValidation(e, false); // Cho phép focus rời khỏi control
                _view.SetMilestoneNameError(null);
            }
        }
    }
}