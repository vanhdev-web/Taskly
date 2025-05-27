using System;
using System.IO;
using System.Drawing.Imaging;
using OOP.Models;
using System.Windows.Forms; // Thêm namespace cho ImageFormat

namespace OOP.Model
{
    public class AvatarService
    {
        private byte[] _avatarBytes;

        public AvatarService()
        {
            // Tải ảnh mặc định khi khởi tạo Model
            using (MemoryStream ms = new MemoryStream())
            {
                Properties.Resources.DefaultAvatar.Save(ms, ImageFormat.Png);
                _avatarBytes = ms.ToArray();
            }
        }

        public byte[] GetDefaultAvatarBytes()
        {
            // Trả về ảnh mặc định đã được tải khi khởi tạo
            return _avatarBytes;
        }

        public byte[] LoadAvatarFromFile(string filePath)
        {
            _avatarBytes = File.ReadAllBytes(filePath);
            return _avatarBytes;
        }

        public byte[] GetAvatarBytes()
        {
            return _avatarBytes;
        }
        public void SaveAvatarToDatabase(int userId)
        {
            using (var db = new TaskManagementDBContext())
            {
                // Tìm người dùng theo ID
                var user = db.Users.Find(userId);

                if (user != null)
                {
                    user.Avatar = _avatarBytes; // Gán byte array của avatar vào cột Avatar
                    db.SaveChanges(); // Lưu thay đổi vào database
                    MessageBox.Show("Avatar saved to database successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("User not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}