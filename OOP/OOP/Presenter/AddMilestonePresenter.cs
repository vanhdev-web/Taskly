using OOP.Forms; // IAddMilestoneView
using OOP.Models; // ModelUser, Milestone, Project
using OOP.Services; // MilestoneService, ProjectManager
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ModelUser = OOP.Models.User;

namespace OOP.Presenters
{
    public class AddMilestonePresenter
    {
        private IAddMilestoneView _view;
        private MilestoneService _milestoneService;
        private ProjectManager _projectManager; // [cite: 28]
        private ActivityLogService _activityLogService;

        public AddMilestonePresenter(IAddMilestoneView view)
        {
            _view = view;
            _milestoneService = new MilestoneService();
            _projectManager = new ProjectManager(); // [cite: 28]
            _activityLogService = new ActivityLogService(new TaskManagementDBContext());
        }

        public void InitializeProjects()
        {
            List<string> projectNames = new List<string>();

            if (ModelUser.LoggedInUser == null) return; // Kiểm tra user đăng nhập [cite: 30]

            foreach (Project project in _projectManager.Projects) // [cite: 30]
            {
                if (project == null || project.members == null) continue; // Kiểm tra null tránh lỗi [cite: 31]

                Console.WriteLine($"Project: {project.projectID} - {project.projectName}, AdminID: {project.AdminID}, Members: {string.Join(", ", project.members)}"); // [cite: 31, 32]

                bool isMember = false; // [cite: 32]
                // Original code had a commented out loop to check if logged-in user is a member of the project
                // For MVP, we'll keep the logic as it was (commented out) or implement it if required.
                // For now, based on the provided code, it appears 'isMember' would always be false
                // if the loop remains commented out in the original 'UpdateComboBox' logic.
                /*
                foreach (string member in project.members)
                {
                    string memberUsername = member.Split('(')[0].Trim(); // Lấy username trước dấu "(" và Trim() [cite: 32]
                    if (memberUsername == ModelUser.LoggedInUser.Username) // [cite: 32, 33]
                    {
                        isMember = true; // [cite: 33]
                        break;
                    }
                }
                */

                if (project.AdminID == ModelUser.LoggedInUser.ID || isMember) // [cite: 34]
                {
                    projectNames.Add($"{project.projectName}"); // [cite: 34]
                }
            }
            _view.SetProjects(projectNames);
        }

        public async void ConfirmMilestone()
        {
            string taskName = _view.MilestoneName; // [cite: 7]
            DateTime deadline = _view.MilestoneDeadline; // [cite: 8]
            string projectName = _view.SelectedProjectName; // [cite: 8]

            int projectId = _milestoneService.GetProjectIdByName(projectName); // [cite: 9, 10]

            if (projectId == 0) // [cite: 10]
            {
                _view.DisplayErrorMessage("Dự án không tồn tại."); // [cite: 10]
                return; // [cite: 11]
            }
            // Lấy thông tin dự án để ghi log
            Project project = null;
            using (var dbcontext = new TaskManagementDBContext())
            {
                project = (from p in dbcontext.Projects
                           where p.projectID == projectId
                           select p).FirstOrDefault();
            }
            List<ModelUser> members = _milestoneService.GetProjectMembers(projectId); // [cite: 20]

            if (members.Count == 0) // [cite: 21]
            {
                _view.DisplayErrorMessage("Không có thành viên nào thuộc dự án này."); // [cite: 21]
                return; // [cite: 22]
            }

            // Create the milestone
            Milestone newMilestone = _milestoneService.CreateMilestone(taskName, deadline, projectId); // [cite: 23, 24]

            // Add milestone members
            _milestoneService.AddMilestoneMembers(newMilestone.taskID, members); // [cite: 25, 26, 27]
            if (ModelUser.LoggedInUser != null && project != null)
            {
                await _activityLogService.LogActivityAsync(
                    userId: ModelUser.LoggedInUser.ID,
                    objectType: "MileStone",
                    objectId: newMilestone.taskID,
                    action: "Create Milestone",
                    details: $"{ModelUser.LoggedInUser.Username} đã tạo một cột mộc {newMilestone.taskName} cho dự án {project.projectName}"
                );
            }

            // Hiển thị thông báo thành công (thay thế MessageBox.Show)
            _view.DisplaySuccessMessage("Activitlog thêm Milestone"); // Thêm phương thức này vào IAddMeetingView
            _view.CloseView(newMilestone); // [cite: 28]
        }

        public void CancelMilestone()
        {
            _view.CancelValidation(new CancelEventArgs(), true); // Simulating cancel button
            _view.CloseView(null); // Or close with DialogResult.Cancel, depending on IAddMilestoneView implementation
                                   // The original code sets DialogResult = DialogResult.Cancel and Close() [cite: 35]
        }

        public void ValidateMilestoneName(string milestoneName, CancelEventArgs e) // [cite: 35]
        {
            if (string.IsNullOrWhiteSpace(milestoneName)) // [cite: 35]
            {
                _view.CancelValidation(e, true); // Chặn chuyển focus nếu input trống [cite: 36]
                _view.SetMilestoneNameError("Please enter task name!"); // [cite: 36]
            }
            else
            {
                _view.CancelValidation(e, false); // Cho phép focus rời khỏi control [cite: 38]
                _view.SetMilestoneNameError(null); // [cite: 39]
            }
        }
    }
}