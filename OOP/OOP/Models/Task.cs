using System;
using System.Collections.Generic;
using System.Data;
using OOP.Models;
using OOP.Services;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOP.Models
{
    public abstract class AbaseTask : IComparable<AbaseTask>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int taskID { get; set; }

        [StringLength(100)]
        [Column("TaskName",TypeName = "nvarchar")]
        [Required]
        public string taskName { get; set; }

        [Required]
        public string status { get; set; }

        [Required]
        public DateTime deadline { get; set; }


        public int AssignedTo { get; set; } // ✅ Moved AssignedTo to AbaseTask
        [ForeignKey("AssignedTo")]
        public virtual User UserAssignedTo { get; set; } 

        public int ProjectID { get; set; } // ✅ Moved ProjectID to AbaseTask
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; } 

     

      
       

      

        public AbaseTask( string taskName, string status, DateTime deadline,  int assignedTo)
        {
    
            this.taskName = taskName;
            this.status = status;
            this.deadline = deadline;
       
            this.AssignedTo = assignedTo; // ✅ Now all tasks have AssignedTo
        }
        public AbaseTask() { }

        public int CompareTo(AbaseTask other)
        {
            if (this.deadline < other.deadline) return -1;
            if (this.deadline > other.deadline) return 1;
            return 0;
        }

   

        public virtual void Message()
        {
            MessageBox.Show("");
        }
        public abstract void UpdateStatus(string newStatus);
    }



    public class Task : AbaseTask
    {


        public Task( string taskName, string status, DateTime deadline,  int assignedTo)
            : base( taskName, status, deadline,  assignedTo)
        {
           
        }
        public Task() : base()
        {
        }


        public override void UpdateStatus(string newStatus)
        {
            status = newStatus;
        }
        public override void Message()
        {
            MessageBox.Show("Task đã được tạo");
        }
    }

    public class Meeting : AbaseTask
    {
        [Required]
        public string Location { get; set; }
    

        [Required]  
        public string Hour { get; set; }

        public virtual ICollection<MeetingMemberManagement> MeetingUsers { get; set; }

        public Meeting( string taskName, string status, DateTime deadline, string hour,  string location, int assignedTo)
            : base( taskName, status, deadline,  assignedTo)
        {
            Location = location;
    
            Hour = hour;
        }

        public Meeting() : base()
        {
        }


        public override void UpdateStatus(string newStatus)
        {
            status = newStatus;
        }
        public override void Message()
        {
            MessageBox.Show("Meeting đã được tạo");
        }
    }


    public class Milestone : AbaseTask
    {

        [Required]
        public string Description { get; set; }
        public virtual ICollection<MilestoneMemberManagement> MilestoneUsers { get; set; }

        public Milestone( string taskName, string status, DateTime deadline, string description,  int assignedTo)
            : base( taskName, status, deadline,  assignedTo)
        {
            Description = description;
        }
        public Milestone() : base()
        {
        }


        public override void UpdateStatus(string newStatus)
        {
            status = newStatus;
        }
        public override void Message()
        {
            MessageBox.Show("Milestone đã được tạo");
        }
    }


}

