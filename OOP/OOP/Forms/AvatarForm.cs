using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taskly.Presenter; // Thêm namespace của Presenter

namespace Taskly
{
    public partial class AvatarForm : Form, IAvatarView // Kế thừa IAvatarView
    {
        private AvatarPresenter _presenter;

        public AvatarForm(int userId)
        {
            InitializeComponent();
            _presenter = new AvatarPresenter(this, userId); // Truyền userId vào Presenter
        }

        public Image AvatarImage // Thuộc tính để Presenter cập nhật ảnh
        {
            set { pbAvatar.Image = value; }
        }

        private void AvatarForm_Load(object sender, EventArgs e)
        {
            _presenter.LoadDefaultAvatar(); // Gọi Presenter để tải ảnh mặc định
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _presenter.LoadAvatarFromFile(openFileDialog.FileName); // Gọi Presenter để tải ảnh từ file
            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            _presenter.SkipAvatarSelection(); // Gọi Presenter để xử lý sự kiện Skip
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           _presenter.SaveAvatar(); // Gọi Presenter để lưu ảnh đại diện
        }

        // Phương thức để Presenter thông báo kết quả đóng form
        public void CloseFormWithResult(DialogResult result)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public byte[] GetAvatarBytesFromView() // Đổi tên để tránh trùng lặp với phương thức của Model
        {
            return null;
        }
    }
    public interface IAvatarView
    {
        Image AvatarImage { set; }
        void CloseFormWithResult(DialogResult result);
    }
}