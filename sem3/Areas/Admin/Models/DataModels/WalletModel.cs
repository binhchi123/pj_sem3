using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Models.DataModels
{
    [Table("Wallet")]
    public class WalletModel
    {
        [Key]
        public int WalletId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        [Required]
        public int Amount { get; set; }

        public UsersModel Users { get; set; }
    }
}
