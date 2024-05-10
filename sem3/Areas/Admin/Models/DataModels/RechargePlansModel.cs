using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Models.DataModels
{
    [Table("RechargePlans")]
    public class RechargePlansModel
    {
        [Key]
        public int RechargePlanId { get; set; }

        [ForeignKey("ServiceProviderId")]
        public int ServiceProviderId { get; set; }

        [Required]
        public string RechargePlanName { get; set; }

        [Required]
        public string RechargePlanValidity { get; set; }

        [Required]
        public int RechargePlanPrice { get; set; }

        [Required]
        public int RechargePlanData { get; set; }

        public ServiceProviderModel ServiceProvider { get; set; }
        public ICollection<UsersModel> Users { get; set;  }
        public ICollection<RechargeLogsModel> RechargeLogs { get; set; }
        public ICollection<RechargeReportModel> RechargeReport { get; set; } 
    }
}
