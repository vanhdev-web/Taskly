// HomePresenter.cs
using OOP.Models;
using OOP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms; // Cần cho Timer, MessageBox

namespace OOP
{
    public class HomePresenter
    {
        private IHomeView _view;
        private TaskManager _taskManager;
        private ProjectManager _projectManager;
        private Timer _timer; // Giữ timer trong Presenter để quản lý logic thời gian

        private bool _sidebarExpand = true; // State của sidebar, được quản lý bởi Presenter

        public HomePresenter(IHomeView view)
        {
            _view = view;
            _taskManager = TaskManager.GetInstance();
            _projectManager = new ProjectManager();

            // Đăng ký các sự kiện từ View
            _view.TaskItemClicked += OnTaskItemClicked;
            _view.ProjectItemClicked += OnProjectItemClicked;
            _view.HomeButtonClicked += OnHomeButtonClicked;
            _view.TaskButtonClicked += OnTaskButtonClicked;
            _view.UserButtonClicked += OnUserButtonClicked;
            _view.ProjectButtonClicked += OnProjectButtonClicked;
            _view.ExitButtonClicked += OnExitButtonClicked;
        }

        public void InitializeHome()
        {
            // Các hoạt động khởi tạo ban đầu, được gọi từ constructor của View
            UpdateDateTime();
            InitializeTimer();
            LoadTasks();
            LoadProjects();
            UpdateWelcomeMessageAndAvatar();
        }

        private void InitializeTimer()
        {
            if (_timer == null) // Tránh tạo lại timer nếu đã có
            {
                _timer = new Timer();
                _timer.Interval = 1000; // Cập nhật mỗi giây
                _timer.Tick += Timer_Tick;
            }
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
                _view.AddTaskItem(task); // View sẽ tạo và hiển thị task item
            }
        }

        public void LoadProjects()
        {
            _view.ClearProjectContainer();
            int loggedInUserId = User.LoggedInUser.ID;

            using (var context = new TaskManagementDBContext()) // Giả định TaskManagementDBContext tồn tại
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
                        _view.AddProjectItem(project); // View sẽ tạo và hiển thị project item
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
                            // Lưu ý: Properties.Resources.DefaultAvatar cần được truy cập từ View hoặc truyền vào
                            // Vì đây là Presenter, nó không nên trực tiếp truy cập Resources.
                            // Cách tốt hơn là View sẽ tự xử lý ảnh mặc định nếu nhận được null hoặc lỗi.
                            _view.SetAvatarImage(null); // Gửi null hoặc chỉ báo lỗi để View tự quyết định ảnh mặc định
                        }
                    }
                }
                else
                {
                    _view.SetAvatarImage(null); // Gửi null để View tự quyết định ảnh mặc định
                }
            }
        }

        // Logic điều khiển Sidebar, nhận các thông số từ View
        public void ToggleSidebar(Panel sidebar, Timer sidebarTransition)
        {
            if (_sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 72)
                {
                    _sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= 150)
                {
                    _sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }
        }

        // --- Event Handlers cho các sự kiện từ View ---
        private void OnTaskItemClicked(AbaseTask task)
        {
            // Logic khi một task item được click
            MessageBox.Show($"Bạn đã click vào Task: {task.taskName}");
            // Ví dụ: Mở form chi tiết Task
            // _view.SwitchForm(new TasksDetailForm(task)); // Giả định tồn tại
        }

        private void OnProjectItemClicked(Project project)
        {
            // Logic khi một project item được click
            MessageBox.Show($"Bạn đã click vào Project: {project.projectName}");
            // Ví dụ: Mở form chi tiết Project
            // _view.SwitchForm(new ProjectsDetailForm(project)); // Giả định tồn tại
        }

        private void OnHomeButtonClicked()
        {
            _view.SwitchForm(new Home());
        }

        private void OnTaskButtonClicked()
        {
            _view.SwitchForm(new Tasks());
        }

        private void OnUserButtonClicked()
        {
            _view.SwitchForm(new MainUser());
        }

        private void OnProjectButtonClicked()
        {
            _view.SwitchForm(new Projects());
        }

        private void OnExitButtonClicked()
        {
            _view.ExitApplication();
        }
    }
}