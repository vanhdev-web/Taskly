using Microsoft.VisualBasic.ApplicationServices;
using OOP;
using OOP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

public interface IObserver
{
    void Update(Notification notification);
}
[Serializable]
[Table("Notifications")]
public class Notification
{
    [Key]
    public int NotiID { get; set; }
    [StringLength(100)]
    [Required]
    public string Title { get; set; }

    public int UserID { get; set; }
    [ForeignKey("UserID")]
    public virtual OOP.Models.User Owner { get; set; }

    [Required]
    public DateTime CreateAt { get; set; }
    [Required]  
    public string Content { get; set; }

    public Notification() { }   

    public Notification(string Title,int UserID, DateTime CreateAt, string Content)
    {
        this.Title = Title;
        this.UserID = UserID;
        this.CreateAt = CreateAt;
        this.Content = Content; 

    }
    public Notification(string Title,  DateTime CreateAt, string Content, string user = "Hệ thống")
    {
        this.Title = Title;
        this.UserID = UserID;
        this.CreateAt = CreateAt;
        this.Content = Content;

    }
}