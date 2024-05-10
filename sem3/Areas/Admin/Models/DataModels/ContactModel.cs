using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Models.DataModels
{
    [Table("Contact")]
    public class ContactModel
    {
        [Key]
        public int ContactId { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime DateOfMessage { get; set; }

        public string Reply { get; set; }

        [ForeignKey("UserId")]

        public UsersModel User { get; set; }
    }
}
