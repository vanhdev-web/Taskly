﻿using System;
using System.Drawing;
using System.IO;
using Taskly.Services;
using System.Windows.Forms;
using Taskly.Model; // Thêm namespace của Model

namespace Taskly.Presenter
{
    public class AvatarPresenter
    {
        private readonly IAvatarView _view;
        private readonly AvatarService _avatarService;
        private int _currentUserId; // Ví dụ: ID của người dùng hiện tại
        public AvatarPresenter(IAvatarView view, int userId)
        {
            _view = view;
            _avatarService = new AvatarService(); // Khởi tạo Model
            _currentUserId = userId; // Gán ID người dùng
        }

        public void LoadDefaultAvatar()
        {
            byte[] defaultAvatarBytes = _avatarService.GetDefaultAvatarBytes(); // Lấy ảnh mặc định từ Model
            using (MemoryStream ms = new MemoryStream(defaultAvatarBytes))
            {
                _view.AvatarImage = Image.FromStream(ms); // Cập nhật View
            }
        }

        public void LoadAvatarFromFile(string filePath)
        {
            try
            {
                byte[] avatarBytes = _avatarService.LoadAvatarFromFile(filePath); // Tải ảnh từ file qua Model
                using (MemoryStream ms = new MemoryStream(avatarBytes))
                {
                    _view.AvatarImage = Image.FromStream(ms); // Cập nhật View
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading avatar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SkipAvatarSelection()
        {
            // Có thể có logic khác ở đây nếu cần trước khi đóng form
            _view.CloseFormWithResult(DialogResult.OK);
        }
        public void SaveAvatar()
        {
            // Gọi phương thức lưu vào database từ Model
            _avatarService.SaveAvatarToDatabase(_currentUserId);
            _view.CloseFormWithResult(DialogResult.OK);
        }
        public byte[] GetCurrentAvatarBytes()
        {
            return _avatarService.GetAvatarBytes(); // Lấy byte[] hiện tại từ Model
        }
    }
}