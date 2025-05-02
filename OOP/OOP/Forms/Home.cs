// Home.cs
using Microsoft.VisualBasic.ApplicationServices;
using Taskly;
using Taskly.Models;
using Taskly.Services;
using Taskly.Usercontrols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using User = Taskly.Models.User;

namespace Taskly
{
    public partial class Home : BaseForm, IHomeView // Kế thừa BaseForm và triển khai IHomeView
    {
        private HomePresenter _presenter;

        // Các sự kiện mà View cung cấp cho Presenter
        public event Action<AbaseTask> TaskItemClicked;
        public event Action<Project> ProjectItemClicked;
        public event Action HomeButtonClicked;
        public event Action TaskButtonClicked;
        public event Action UserButtonClicked;
        public event Action ProjectButtonClicked;
        public event Action ExitButtonClicked;

        public Home()
        {
            InitializeComponent();
            _presenter = new HomePresenter(this);

            // Gán sự kiện Click cho các nút điều hướng chính
            // Các sự kiện này sẽ kích hoạt các Action đã khai báo ở trên để Presenter lắng nghe
            btnHome.Click += (s, e) => HomeButtonClicked?.Invoke();
            btnTask.Click += (s, e) => TaskButtonClicked?.Invoke();
            btnUser.Click += (s, e) => UserButtonClicked?.Invoke();
            btnProject.Click += (s, e) => ProjectButtonClicked?.Invoke();
            btnExit.Click += (s, e) => ExitButtonClicked?.Invoke();

            // Khởi tạo Presenter để nó load dữ liệu và các thành phần khác
            _presenter.InitializeHome();

            // Áp dụng các sự kiện chuột ban đầu cho các Panel chính (hiệu ứng hover, v.v.)
            // Logic này vẫn thuộc về View
            //ApplyMouseEvents(TopPanel);
            //ApplyMouseEvents(projectPanel);
            //ApplyMouseEvents(taskPanel);
        }

        // Triển khai các phương thức từ IHomeView
        public void SetTimeDetail(string time)
        {
            timeDetail.Text = time;
        }

        public void SetWelcomeName(string name)
        {
            WelcomeName.Text = name;
        }

        public void SetAvatarImage(Image image)
        {
            // View tự xử lý ảnh mặc định nếu image là null
            avatar.Image = image ?? Properties.Resources.DefaultAvatar;
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ClearTaskContainer()
        {
            taskContainer.Controls.Clear();
        }

        public void AddTaskItem(AbaseTask task)
        {
            HomeTaskUserControl taskItem = new HomeTaskUserControl(task);
            taskItem.Dock = DockStyle.Top;
            taskContainer.Controls.Add(taskItem);

            // Gán sự kiện Click cho Panel bên trong HomeTaskUserControl
            // Khi Panel này được click, kích hoạt sự kiện TaskItemClicked của View
            // để Presenter lắng nghe và xử lý.
            if (taskItem.TaskPanel != null) // Đảm bảo TaskPanel tồn tại
            {
                taskItem.TaskPanel.Click += (s, e) => TaskItemClicked?.Invoke(task);
                //ApplyMouseEvents(taskItem.TaskPanel); // Áp dụng hiệu ứng hover nếu có
            }
        }

        public void ClearProjectContainer()
        {
            projectContainer.Controls.Clear();
        }

        public void AddProjectItem(Project project)
        {
            HomeProjectUserControl projectItem = new HomeProjectUserControl(project);
            projectItem.Dock = DockStyle.Top;
            projectContainer.Controls.Add(projectItem);

            // Gán sự kiện Click cho Panel bên trong HomeProjectUserControl
            if (projectItem.ProjectPanel != null) // Đảm bảo ProjectPanel tồn tại
            {
                projectItem.ProjectPanel.Click += (s, e) => ProjectItemClicked?.Invoke(project);
                //ApplyMouseEvents(projectItem.ProjectPanel); // Áp dụng hiệu ứng hover nếu có
            }
        }

        // Phương thức ApplyMouseEvents: Nơi gán hiệu ứng hover (Mouse EnteR/Leave)
        // và xử lý click chung cho các Panel mà không cần truyền tham số quá cụ thể.
        // Logic này vẫn giữ nguyên trong View.
        public void ApplyMouseEvents(Control control)
        {
            control.MouseEnter += (s, e) => {
                if (s is Control c)
                {
                    c.BackColor = Color.LightGray; // Hoặc màu bạn mong muốn khi hover
                }
            };
            control.MouseLeave += (s, e) => {
                if (s is Control c)
                {
                    c.BackColor = Color.Transparent; // Trả lại màu nền ban đầu
                }
            };

           
        }

        public void SwitchForm(Form newForm)
        {
            newForm.StartPosition = FormStartPosition.Manual;
            newForm.Location = this.Location;  // Lưu vị trí form hiện tại

            this.Hide();
            newForm.Show();  // hoặc ShowDialog() nếu cần
        }


        public void ExitApplication()
        {
            Application.Exit();
        }

        public void StartSidebarTransition()
        {
            sidebarTransition.Start();
        }

        // Event handler cho Timer của Sidebar (vẫn ở View vì nó điều khiển UI trực tiếp)
        private void sidebarTransition_Tick(Object sender, EventArgs e)
        {
            // View gọi Presenter để xử lý logic thay đổi kích thước sidebar
            _presenter.ToggleSidebar(sidebar, sidebarTransition);
        }

        // Các event handler khác của View (nếu không có logic nghiệp vụ, giữ lại)
        private void Home_Load(object sender, EventArgs e) { /* Presenter đã lo phần khởi tạo */ }
        private void btnHam_Click(object sender, EventArgs e) { StartSidebarTransition(); }
        private void panel6_Paint(object sender, PaintEventArgs e) { }
        private void WelcomeName_Click(object sender, EventArgs e) { }

     
    }
    public interface IHomeView
    {
        void SetTimeDetail(string time);
        void SetWelcomeName(string name);
        void SetAvatarImage(Image image);
        void ShowErrorMessage(string message);
        void ClearTaskContainer();
        void AddTaskItem(AbaseTask task); // View sẽ tự biết cách tạo UserControl và gán sự kiện cho nó
        void ClearProjectContainer();
        void AddProjectItem(Project project); // View sẽ tự biết cách tạo UserControl và gán sự kiện cho nó

        // Các phương thức điều hướng và thoát ứng dụng cũng được chuyển lên View
        void SwitchForm(Form newForm);
        void ExitApplication();

        // Phương thức để View bắt đầu hiệu ứng sidebar (nếu Presenter yêu cầu)
        void StartSidebarTransition();

        // Phương thức để View thông báo cho Presenter khi một Task item được click
        event Action<AbaseTask> TaskItemClicked;
        // Phương thức để View thông báo cho Presenter khi một Project item được click
        event Action<Project> ProjectItemClicked;

        // Phương thức để View thông báo cho Presenter khi một nút điều hướng được click
        event Action HomeButtonClicked;
        event Action TaskButtonClicked;
        event Action UserButtonClicked;
        event Action ProjectButtonClicked;
        event Action ExitButtonClicked;
    }
}