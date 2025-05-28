using System;
using System.Windows.Forms;
using OOP.Presenters; // Import Presenter namespace
using OOP.Models;    // Still needed for AvatarForm's constructor if it expects OOP.Models.User ID

namespace OOP
{
    public partial class Register : Form, IRegisterView
    {
        private RegisterPresenter _presenter; // Thêm một thể hiện của Presenter

        // Giữ lại userID nếu nó có ý nghĩa trong các phần khác của form hoặc khi khởi tạo
        private int userID;

        public Register()
        {
            InitializeComponent();
            // Khởi tạo Presenter và truyền View (chính form Register này) vào
            _presenter = new RegisterPresenter(this);
        }

        // Triển khai các thuộc tính của IRegisterView để Presenter lấy dữ liệu
        public string Username => txtUsername.Text;
        public string Password => txtPassword.Text;
        public string Email => txtEmail.Text;

        // Triển khai sự kiện RegisterButtonClick của IRegisterView
        public event EventHandler RegisterButtonClick;

        // Triển khai phương thức ShowMessage của IRegisterView
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessageBox.Show(message, caption, buttons, icon);
        }

        // Triển khai phương thức ShowAvatarForm của IRegisterView
        public DialogResult ShowAvatarForm(int userId)
        {
            AvatarForm avatarForm = new AvatarForm(userId);
            return avatarForm.ShowDialog();
        }

        // Triển khai phương thức CloseView của IRegisterView
        public void CloseView()
        {
            this.Close();
        }

        // Xử lý sự kiện click của nút Đăng ký
        private async void btnRegister_Click(object sender, EventArgs e)
        {
            // Chỉ cần gọi sự kiện để Presenter xử lý logic
            RegisterButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            // Không có logic nghiệp vụ ở đây, chỉ là sự kiện UI
        }
    }
    public interface IRegisterView
    {
        // Thuộc tính để lấy dữ liệu từ các điều khiển UI
        string Username { get; }
        string Password { get; }
        string Email { get; }

        // Sự kiện để thông báo hành động của người dùng đến Presenter
        event EventHandler RegisterButtonClick;

        // Phương thức để Presenter hiển thị thông báo
        void ShowMessage(string message);
        void ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);

        // Phương thức để Presenter yêu cầu View mở form Avatar
        DialogResult ShowAvatarForm(int userId);

        // Phương thức để Presenter yêu cầu View đóng form
        void CloseView();
    }
}