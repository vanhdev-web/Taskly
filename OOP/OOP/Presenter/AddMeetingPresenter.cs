using Taskly.Forms; // IAddMeetingView
using Taskly.Models; // ModelUser, Meeting, Project
using Taskly.Services; // MeetingService, ProjectManager
using System;
using System.Collections.Generic;
using System.ComponentModel;
using ModelUser = Taskly.Models.User;
using System.Linq;

namespace Taskly.Presenter
{
    public class AddMeetingPresenter
    {
        private IAddMeetingView _view;
        private MeetingService _meetingService;
        private ProjectManager _projectManager;
        private ActivityLogService _activityLogService;
        public AddMeetingPresenter(IAddMeetingView view)
        {
            _view = view;
            _meetingService = new MeetingService();
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
                foreach (string member in project.members)
                {
                    string memberUsername = member.Split('(')[0].Trim(); // Lấy username trước dấu "(" và Trim()
                    if (memberUsername == ModelUser.LoggedInUser.Username)
                    {
                        isMember = true;
                        break;
                    }
                }

                if (project.AdminID == ModelUser.LoggedInUser.ID || isMember)
                {
                    projectNames.Add(project.projectName);
                }
            }
            _view.SetProjects(projectNames);
        }

        public async void ConfirmMeeting()
        {
            string location = _view.MeetingLocation;
            DateTime meetingTime = _view.MeetingTime;
            string hour = _view.MeetingHour;
            string taskName = _view.MeetingName;
            string status = "Unfinished";
            string projectName = _view.SelectedProjectName;

            int projectId = _meetingService.GetProjectIdByName(projectName);

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

            List<ModelUser> members = _meetingService.GetProjectMembers(projectId);

            if (members.Count == 0)
            {
                _view.DisplayErrorMessage("Không có thành viên nào thuộc dự án này.");
                return;
            }

            // Create the meeting
            Meeting newMeeting = _meetingService.CreateMeeting(taskName, status, meetingTime, location, hour, projectId);

            // Add meeting members
            _meetingService.AddMeetingMembers(newMeeting.taskID, members);
            // Ghi Activity Log sau khi tạo cuộc họp thành công
            if (ModelUser.LoggedInUser != null && project != null)
            {
                await _activityLogService.LogActivityAsync(
                    userId: ModelUser.LoggedInUser.ID,
                    objectType: "Meeting",
                    objectId: newMeeting.taskID,
                    action: "Create Meeting",
                    details: $"{ModelUser.LoggedInUser.Username} đã tạo một cuộc họp {newMeeting.taskName} cho dự án {project.projectName}"
                );
            }

            // Hiển thị thông báo thành công (thay thế MessageBox.Show)
            _view.DisplaySuccessMessage("Activitlog thêm meeting"); // Thêm phương thức này vào IAddMeetingView

            _view.CloseView(newMeeting);
        }

        public void ValidateMeetingName(string meetingName, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(meetingName))
            {
                _view.CancelValidation(e, true);
                _view.SetMeetingNameError("Please enter task name!");
            }
            else
            {
                _view.CancelValidation(e, false);
                _view.SetMeetingNameError(null);
            }
        }
    }
}