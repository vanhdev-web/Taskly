using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOP.Models
{
    [Table("MeetingMemberManagement")]
    public class MeetingMemberManagement
    {
        [Key]
        public int ID { get; set; }  

        public int MeetingID { get; set; }
        [ForeignKey("MeetingID")]
        public virtual Meeting Meeting { get; set; }


        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
