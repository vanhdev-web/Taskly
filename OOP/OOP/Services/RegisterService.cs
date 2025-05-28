using OOP.Models;
using OOP.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail; // Required for MailAddress

namespace OOP.Models
{
    public class RegisterModel
    {
        /// <summary>
        /// Kiểm tra tính hợp lệ của định dạng email.
        /// </summary>
        /// <param name="email">Địa chỉ email cần kiểm tra.</param>
        /// <returns>True nếu email hợp lệ, ngược lại là False.</returns>
        public bool IsValidEmail(string email)
        {
            try
            {
                // Sử dụng lớp MailAddress để kiểm tra định dạng email
                System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Đăng ký người dùng mới vào hệ thống.
        /// </summary>
        /// <param name="username">Tên người dùng.</param>
        /// <param name="password">Mật khẩu.</param>
        /// <param name="email">Email.</param>
        /// <returns>ID của người dùng mới nếu đăng ký thành công, hoặc 0 nếu có lỗi (ví dụ: trùng username/email).</returns>
        public async Task<int> RegisterUserAsync(string username, string password, string email)
        {
            using (var db = new TaskManagementDBContext())
            {
                // Kiểm tra username đã tồn tại
                var existingUsernameQuery = from u in db.Users
                                            where u.Username == username
                                            select u;
                if (existingUsernameQuery.Any())
                {
                    // Trả về 0 để báo hiệu lỗi hoặc username đã tồn tại
                    return 0;
                }

                // Kiểm tra email đã tồn tại
                var existingEmailQuery = from u in db.Users
                                         where u.Email == email
                                         select u;
                if (existingEmailQuery.Any())
                {
                    // Trả về 0 để báo hiệu lỗi hoặc email đã tồn tại
                    return 0;
                }

                // Tạo user mới
                User newUser = new User
                {
                    Username = username,
                    Password = password,
                    Email = email
                };
                db.Users.Add(newUser);
                db.SaveChanges();

                //Activitylog đăng ký
                ActivityLogService activityLogService = new ActivityLogService(db);
                await activityLogService.LogActivityAsync(userId: null, objectType: "User", objectId: newUser.ID, action: "Register", details: $"User Name :{newUser.Username} Email : {newUser.Email} ");

                NotificationManager.Instance.SendAccountNotification(newUser.ID);

                // Trả về ID của người dùng mới đăng ký thành công
                return newUser.ID;
            }
        }
    }
}