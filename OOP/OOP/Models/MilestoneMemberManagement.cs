using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OOP.Models
{
    [Table("MilestoneMemberManagement")]
    public class MilestoneMemberManagement
    {
        [Key]
        public int ID { get; set; }

        public int MilestoneID { get; set; }
        [ForeignKey("MilestoneID")]
        public virtual Milestone Milestone { get; set; }


        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
