using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using OOP.Models;
using System.Linq;
using OOP.Services;

namespace OOP
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ.");
                return;
            }

            using (var db = new TaskManagementDBContext())
            {
                // Kiểm tra username đã tồn tại
                var existingUsernameQuery = from u in db.Users
                                            where u.Username == username
                                            select u;
                if (existingUsernameQuery.Any())
                {
                    MessageBox.Show("Username đã tồn tại.");
                    return;
                }

                // Kiểm tra email đã tồn tại
                var existingEmailQuery = from u in db.Users
                                         where u.Email == email
                                         select u;
                if (existingEmailQuery.Any())
                {
                    MessageBox.Show("Email đã tồn tại.");
                    return;
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
                MessageBox.Show("Activitlog đăng ký");
          

                NotificationManager.Instance.SendAccountNotification(newUser.ID);
                MessageBox.Show("Đăng ký thành công!");
                this.Close();

                // Chọn avatar nếu muốn
                AvatarForm avatarForm = new AvatarForm();
                if (avatarForm.ShowDialog() == DialogResult.OK)
                {
                    newUser.Avatar = avatarForm.GetAvatarBytes();

                    // Cập nhật user trong database
                    db.Users.Update(newUser);
                    db.SaveChanges();
                }



            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}