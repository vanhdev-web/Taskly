using OOP.Models;
using System.Linq;
using System.Windows.Forms; // Required for MessageBoxButtons and DialogResult

namespace OOP.Presenters
{
    public class RegisterPresenter
    {
        private readonly IRegisterView _view;
        private readonly RegisterModel _model;

        public RegisterPresenter(IRegisterView view)
        {
            _view = view;
            _model = new RegisterModel();
            // Đăng ký lắng nghe sự kiện từ View
            _view.RegisterButtonClick += OnRegisterButtonClick;
        }

        private async void OnRegisterButtonClick(object sender, System.EventArgs e)
        {
            string username = _view.Username.Trim();
            string password = _view.Password.Trim();
            string email = _view.Email.Trim();

            // Kiểm tra các trường thông tin rỗng
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                _view.ShowMessage("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Kiểm tra tính hợp lệ của email thông qua Model
            if (!_model.IsValidEmail(email))
            {
                _view.ShowMessage("Email không hợp lệ.");
                return;
            }

            // Gọi Model để thực hiện logic đăng ký và lấy ID người dùng mới
            int newUserId = await _model.RegisterUserAsync(username, password, email);

            if (newUserId > 0) // Đăng ký thành công
            {
                _view.ShowMessage("Activitlog đăng ký");
                _view.ShowMessage("Đăng ký thành công!");

                // Chọn avatar nếu muốn
                // Yêu cầu View hiển thị form Avatar và chờ kết quả
                if (_view.ShowAvatarForm(newUserId) == DialogResult.OK)
                {
                    _view.ShowMessage("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.CloseView(); // Đóng form Register
                }
                else
                {
                    // Xử lý trường hợp người dùng đóng AvatarForm mà không chọn Save (ví dụ: nhấn Cancel)
                    _view.ShowMessage("Avatar selection skipped or cancelled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.CloseView(); // Bạn có thể chọn đóng form Register hoặc tiếp tục mà không có avatar
                }
            }
            else // Đăng ký thất bại (username hoặc email đã tồn tại)
            {
                // Dựa vào việc `RegisterUserAsync` trả về 0 khi có lỗi, Presenter sẽ xử lý thông báo phù hợp
                // Trong thực tế, bạn có thể muốn Model trả về một Enum hoặc một đối tượng kết quả cụ thể hơn
                // để Presenter có thể phân biệt lỗi chi tiết hơn (ví dụ: UserExists, EmailExists, DatabaseError).
                // Ở đây, vì `RegisterUserAsync` trong Model đã kiểm tra trùng lặp và trả về 0,
                // chúng ta cần một cơ chế để xác định lỗi cụ thể.
                // Để giữ nguyên logic thông báo cũ, Presenter sẽ gọi Model để kiểm tra lại tình trạng.

                // Lưu ý: Việc gọi lại database để kiểm tra tồn tại username/email ở đây
                // là để giữ nguyên logic thông báo chi tiết như code gốc.
                // Một cách tối ưu hơn là `RegisterModel.RegisterUserAsync` trả về
                // một enum hoặc string chỉ rõ lỗi (e.g., "UsernameExists", "EmailExists").
                using (var db = new TaskManagementDBContext()) // Tái sử dụng TaskManagementDBContext cho việc kiểm tra
                {
                    var existingUsernameQuery = from u in db.Users where u.Username == username select u;
                    if (existingUsernameQuery.Any())
                    {
                        _view.ShowMessage("Username đã tồn tại.");
                        return;
                    }

                    var existingEmailQuery = from u in db.Users where u.Email == email select u;
                    if (existingEmailQuery.Any())
                    {
                        _view.ShowMessage("Email đã tồn tại.");
                        return;
                    }
                }
            }
        }
    }
}