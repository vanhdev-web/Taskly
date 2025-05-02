using Taskly.Services;
using Taskly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using Taskly.Usercontrols;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskly.Models
{
   
    [Table("Projects")]

    public class Project
    {
        [Key]
        public  int projectID { get; set; }

        [StringLength(100)]
        [Column("Project Name", TypeName = "nvarchar")]
        [Required]
        public string projectName { get; set; }

        [Required]
        public string projectDescription { get; set; }


        public List<AbaseTask> tasks = new List<AbaseTask>();


        public int AdminID { get; set; }
        [ForeignKey("AdminID")]
        public virtual User Admin { get; set; }

        [NotMapped]
        public List<string> members { get; set; } = new List<string>();
        public Project() { }

        public Project(int projectID, string projectName, string projectDescription)
        {
            this.projectID = projectID;
            this.projectName = projectName;
            this.projectDescription = projectDescription;
      

        }
        public Project(int projectID, string projectName, string projectDescription,  int adminID, string createdBy)
        {
            this.projectID = projectID;
            this.projectName = projectName;
            this.projectDescription = projectDescription;
        
            this.AdminID = adminID;
 
            //this.members = new List<User  > { $"{createdBy} (Admin)" };
        }


        
        //public void AddTask(Task task)
        //{
        //    if (UserRole == RoleType.Admin || UserRole == RoleType.Member)
        //    {
        //        tasks.Add(task);
        //    }
        //    else
        //    {
        //        throw new UnauthorizedAccessException("Only Admins and Members can add tasks.");
        //    }

        //}
        


        //public void RemoveTask(Task task)
        //{
        //    if (UserRole == RoleType.Admin)
        //    {
        //        tasks.Remove(task);
        //    }
        //    else
        //    {
        //        throw new UnauthorizedAccessException("Only Admins can remove tasks.");
        //    }
        //}
        ////operator +
        //public static Project operator +(Project project, string memberInfo)
        //{
        //    if (project != null && !string.IsNullOrWhiteSpace(memberInfo) && !project.members.Contains(memberInfo))
        //    {
        //        project.members.Add(memberInfo);
        //    }
        //    return project; // Trả về chính đối tượng Project
        //}


    }
}