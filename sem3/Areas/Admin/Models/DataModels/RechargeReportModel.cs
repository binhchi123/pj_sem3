using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Models.DataModels
{
    [Table("RechargeReport")]
    public class RechargeReportModel
    {
        [Key]
        public int ReportId { get; set; }

        [ForeignKey("RechargePlanId")]
        public int RechargePlanId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        [Required]
        public string Phone { get; set; }

        public DateTime DateOfRecharge { get; set; }

        public DateTime ValidTill { get; set; }

        public RechargePlansModel RechargePlans { get; set; }
        public UsersModel Users { get; set; }
    }
}
