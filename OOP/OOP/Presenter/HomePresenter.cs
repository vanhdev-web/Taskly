using OOP.Models;
using OOP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms; // For MessageBox and Image related operations if needed by presenter for direct UI interaction (e.g., error messages)

namespace OOP
{
    public class HomePresenter
    {
        private IHomeView _view;
        private TaskManager _taskManager;
        private ProjectManager _projectManager;
        private Timer _timer;

        public HomePresenter(IHomeView view)
        {
            _view = view;
            _taskManager = TaskManager.GetInstance();
            _projectManager = new ProjectManager();
        }

        public void LoadHomeData()
        {
            UpdateDateTime();
            InitializeTimer();
            LoadTasks();
            LoadProjects();
            UpdateWelcomeMessageAndAvatar();
        }

        private void InitializeTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000; // Cập nhật mỗi giây
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            _view.SetTimeDetail(DateTime.Now.ToString("dddd, 'ngày' dd 'tháng' M"));
        }

        public List<AbaseTask> GetUserTasks()
        {
            List<Project> userProjects = _projectManager.FindProjectsByMember(User.LoggedInUser);
            List<AbaseTask> userTasks = new List<AbaseTask>();

            if (userProjects.Count == 0)
            {
                Console.WriteLine("User không thuộc bất kỳ project nào.");
                return userTasks; // Trả về danh sách rỗng nếu user không có project
            }

            foreach (Project project in userProjects)
            {
                List<AbaseTask> projectTasks = _taskManager.GetTasksByProject(project.projectName);
                foreach (AbaseTask task in projectTasks)
                {
                    if (task.AssignedTo > 0 && task.AssignedTo == User.LoggedInUser.ID)
                    {
                        userTasks.Add(task);
                    }
                    else if (task.AssignedTo == 0) // Meeting, Milestone (không có assigned)
                    {
                        userTasks.Add(task);
                    }
                }
            }
            return userTasks;
        }

        public void LoadTasks()
        {
            _view.ClearTaskContainer();
            foreach (AbaseTask task in GetUserTasks())
            {
                _view.AddTaskItem(task);
            }
        }

        public void LoadProjects()
        {
            _view.ClearProjectContainer();
            int loggedInUserId = User.LoggedInUser.ID;

            using (var context = new TaskManagementDBContext())
            {
                foreach (Project project in _projectManager.Projects)
                {
                    if (project == null)
                        continue;

                    // Kiểm tra user có phải admin của project
                    bool isAdmin = project.AdminID == loggedInUserId;

                    // Kiểm tra user có được giao task/milestone/meeting trong project này không
                    bool isAssignedInProject = (
                        (from t in context.Tasks
                         where t.ProjectID == project.projectID && t.AssignedTo == loggedInUserId
                         select t).Any()
                        ||
                        (from m in context.Milestones
                         where m.ProjectID == project.projectID &&
                         m.AssignedTo == loggedInUserId
                         select m).Any()
                        ||
                        (from mt in context.Meetings
                         where mt.ProjectID == project.projectID && mt.AssignedTo == loggedInUserId
                         select mt).Any()
                    );

                    if (isAdmin || isAssignedInProject)
                    {
                        _view.AddProjectItem(project);
                    }
                }
            }
        }

        private void UpdateWelcomeMessageAndAvatar()
        {
            if (User.LoggedInUser != null)
            {
                _view.SetWelcomeName($"Hey {User.LoggedInUser.Username}, sẵn sàng làm việc chưa? 🚀");

                if (User.LoggedInUser.Avatar != null && User.LoggedInUser.Avatar.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(User.LoggedInUser.Avatar))
                    {
                        try
                        {
                            _view.SetAvatarImage(Image.FromStream(ms));
                        }
                        catch (Exception ex)
                        {
                            _view.ShowErrorMessage($"Lỗi hiển thị ảnh đại diện: {ex.Message}");
                            _view.SetAvatarImage(Properties.Resources.DefaultAvatar); // Ảnh mặc định nếu lỗi
                        }
                    }
                }
                else
                {
                    _view.SetAvatarImage(Properties.Resources.DefaultAvatar);
                    // Ảnh mặc định nếu không có ảnh
                }
            }
        }

        public void ToggleSidebar(ref bool sidebarExpand, Panel sidebar, Timer sidebarTransition)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 72)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= 150)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }
        }
    }
}