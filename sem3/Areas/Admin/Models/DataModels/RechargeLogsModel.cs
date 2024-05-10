using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Models.DataModels
{
    [Table("RechargeLogs")]
    public class RechargeLogsModel
    {
        [Key]
        public int RechargeLogId { get; set; }

        [ForeignKey("RechargePlanId")]
        public int RechargePlanId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UsersId")]

        [Required]
        public DateTime RechargeDate { get; set; }

        [Required]
        public DateTime RechargeValidity { get; set; }

        public RechargePlansModel RechargePlans { get; set; }
        public UsersModel Users { get; set; }
    }
}
