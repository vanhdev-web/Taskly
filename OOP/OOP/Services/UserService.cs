using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using OOP.Models;
using System.IO;


namespace OOP
{

    public class UserService
    {
        private static readonly string filePath = "users.json";
        public static List<User> LoadUsers()
        {
            try
            {
                using (var context = new TaskManagementDBContext())
                {
                    return context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users from database: {ex.Message}");
                return new List<User>();
            }
        }

        public static User AuthenticateUser(string username, string password)
        {
            List<User> users = LoadUsers();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == username && users[i].Password == password)
                {
                    return users[i];
                }
            }
            return null;
        }
        //public static void SaveUsers(List<User> users)
        //{
        //    try
        //    {
        //        string json = JsonSerializer.Serialize(users);
        //        File.WriteAllText(filePath, json);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error saving users: {ex.Message}");
        //    }
        //}
        public static void SaveCurrentUser()
        {
            try
            {
                if (User.LoggedInUser?.ID == null)
                {
                    Console.WriteLine("No user is logged in.");
                    return;
                }

                using (var context = new TaskManagementDBContext())
                {
                    // Tìm người dùng hiện tại theo ID hoặc Username
                    var existingUser = context.Users
                        .FirstOrDefault(u => u.ID == User.LoggedInUser.ID || u.Username == User.LoggedInUser.Username);

                    if (existingUser != null)
                    {
                        // Cập nhật thông tin
                        existingUser.Username = User.LoggedInUser.Username;
                        existingUser.Password = User.LoggedInUser.Password;
                        existingUser.Email = User.LoggedInUser.Email;
                        existingUser.Avatar = User.LoggedInUser.Avatar;

                        context.Users.Update(existingUser); // không bắt buộc nhưng rõ ràng
                    }
                    else
                    {
                        // Thêm mới nếu chưa có
                        context.Users.Add(User.LoggedInUser);
                    }

                    context.SaveChanges(); // Ghi thay đổi vào database
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving current user: {ex.Message}");
            }


        }
        }
}