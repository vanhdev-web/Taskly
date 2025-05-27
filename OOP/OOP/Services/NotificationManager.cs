using Microsoft.EntityFrameworkCore;
using OOP;
using OOP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Linq;

[Serializable]
public class NotificationManager
{

    private static NotificationManager instance;
    private Dictionary<int, List<Notification>> notificationsByUser = new Dictionary<int, List<Notification>>();
    private Dictionary<string, List<NotiUserControl>> displayedNotiControls = new Dictionary<string, List<NotiUserControl>>();
    private List<IObserver> observers = new List<IObserver>();
    private static List<Notification> storedNotifications = new List<Notification>();

   

  

    // Sử dụng phương thức Initialize để khởi tạo instance
  

    // Property Instance
    public static NotificationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NotificationManager();
            }
            return instance;
        }
    }
    public void LoadNotifications()
    {
        try
        {
            using (var _dbContext = new TaskManagementDBContext())
            {
                notificationsByUser = _dbContext.Notifications
                .GroupBy(n => n.UserID)
                .ToDictionary(g => g.Key, g => g.ToList());

            }
                // Tải thông báo từ cơ sở dữ liệu theo UserID

            Console.WriteLine("Đã tải thông báo từ cơ sở dữ liệu.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi tải thông báo từ cơ sở dữ liệu: {ex.Message}");
            notificationsByUser = new Dictionary<int, List<Notification>>();
        }
    }

    public void SaveNotifications()
    {
        try
        {
            using (var _dbContext = new TaskManagementDBContext())
            {
                    foreach (var userNotifications in notificationsByUser.Values)
                {
                    _dbContext.Notifications.AddRange(userNotifications);
                }
                _dbContext.SaveChanges();

            }
                // Duyệt qua tất cả thông báo và thêm vào cơ sở dữ liệu

            Console.WriteLine($"Đã lưu {notificationsByUser.Values.Sum(n => n.Count)} thông báo vào cơ sở dữ liệu.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi lưu thông báo vào cơ sở dữ liệu: {ex.Message}");
        }
    }

    public void AddNotification(int userId, Notification notification)
    {
        try
        {
            using (var _dbContext = new TaskManagementDBContext())
            {

                _dbContext.Notifications.Add(notification);
                _dbContext.SaveChanges();
            }
                // Thêm thông báo vào cơ sở dữ liệu

            Console.WriteLine($"Đã thêm thông báo: {notification.Content}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi thêm thông báo vào cơ sở dữ liệu: {ex.Message}");
        }
    }

    public List<Notification> GetNotifications(int userId)
    {
        try
        {
            using (var _dbContext = new TaskManagementDBContext())
            {
                return _dbContext.Notifications
                .Where(n => n.UserID == userId)
                .OrderByDescending(n => n.CreateAt)
                .ToList();

            }
                // Lấy thông báo từ cơ sở dữ liệu
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi lấy thông báo từ cơ sở dữ liệu: {ex.Message}");
            return new List<Notification>();
        }
    }

    //ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
    // Lưu danh sách NotiUserControl đã hiển thị
    public void SaveDisplayedNotifications(string userId, List<NotiUserControl> controls)
    {
        if (!displayedNotiControls.ContainsKey(userId))
        {
            displayedNotiControls[userId] = new List<NotiUserControl>();
        }
        displayedNotiControls[userId] = controls;
    }

    public List<NotiUserControl> GetDisplayedNotifications(string userId)
    {
        return displayedNotiControls.ContainsKey(userId) ? displayedNotiControls[userId] : new List<NotiUserControl>();
    }

    public void Subscribe(IObserver observer)
    {
        observers.Add(observer);
        foreach (var notification in storedNotifications)
        {
            observer.Update(notification);
        }
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(Notification notification)
    {
        storedNotifications.Add(notification);
        foreach (var observer in observers)
        {
            observer.Update(notification);
        }
    }

    public void SendAccountNotification(int userid)
    {
        try
        {
            Notification notification = new Notification
            {
                Title = "THÔNG BÁO TÀI KHOẢN",
                CreateAt = DateTime.Now,
                Content = $"Tài khoản  đã đăng ký thành công!",
                UserID = userid,  // Hoặc bạn có thể dùng UserID thay vì username
            };

            AddNotification(userid, notification);
            Notify(notification);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi gửi thông báo tài khoản: {ex.Message}");
        }
    }

    public void SendProjectNotification(int userid, string projectName)
    {
        try
        {
            Notification notification = new Notification
            {
                Title = "THÔNG BÁO DỰ ÁN",
                CreateAt = DateTime.Now,
                Content = $"Dự án đã được tạo!",
                UserID = userid,  // Hoặc bạn có thể dùng UserID thay vì username
            };

            AddNotification(userid, notification);
            Notify(notification);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi gửi thông báo dự án: {ex.Message}");
        }
    }

    public void SendTaskUpdateNotification(int userid, string taskName, string status)
    {
        try
        {
            if (status == "Finished")
            {
                Notification notification = new Notification
                {
                    Title = "CẬP NHẬT TRẠNG THÁI",
                    CreateAt = DateTime.Now,
                    Content = $"'{taskName}' của bạn đã hoàn thành!",
                    UserID = userid,  // Hoặc bạn có thể dùng UserID thay vì username
                };

                AddNotification(userid, notification);
                Notify(notification);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi gửi thông báo cập nhật task: {ex.Message}");
        }
    }
}