using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskly.Models
{
    [Table("Users")] // Đặt tên bảng là "Users"
    [Serializable] // Thêm attribute Serializable để có thể serialize object
    public class User
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        [Column("User Name", TypeName = "nvarchar")]
        [Required]
        public string Username { get; set; } // Thêm username

        [Required]
        public string Password { get; set; } // Thêm password
        [Required]
        public string Email { get; set; } // Thêm email

        
        public byte[] Avatar { get; set; }


        public virtual ICollection<MeetingMemberManagement> MeetingUsers { get; set; }
        public virtual ICollection<MilestoneMemberManagement> MilestoneUsers { get; set; }


        public virtual List<Task> Tasks { get; set; } = new List<Task>();

        [NotMapped]       
        
        public static User LoggedInUser { get; set; } // Đây là người dùng đã đăng nhập, ví dụ "admin"

        public User() { }

        public User(int id, string username, string password, string email)
        {
            ID = id;
            Username = username;
            Password = password;
            Email = email;
        }
        public static void Login(string username)
        {
            // Tạo một đối tượng User và gán cho LoggedInUser
            LoggedInUser = new User(1, username, "123", "Taskly@.com"); // Ví dụ, ID = 1, bạn có thể thay bằng cách tạo ID tự động
        }

        // Phương thức để lấy thông tin người dùng đăng nhập
        public static int GetLoggedInUserName()
        {
            return LoggedInUser?.ID ?? -1;
        }

        // Phương thức để kiểm tra xem người dùng có đăng nhập không
        public static bool IsLoggedIn()
        {
            return LoggedInUser != null;
        }
       
    }
}