using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Models
{
    [Table("ActivityLog")]
    public class ActivityLog
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        [MaxLength(100)]
        public string ObjectType { get; set; }

        public int? ObjectId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Action { get; set; }

   
        public string Details { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
