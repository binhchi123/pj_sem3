using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Models.DataModels
{
    [Table("Users")]
    public class UsersModel
    {
        [Key]
        public int UserId { get; set; }

        [ForeignKey("ServiceProviderId")]
        public int ServiceProviderId { get; set; }

        [ForeignKey("RechargePlanId")]
        public int RechargePlanId { get; set; }

        [Required, Column(TypeName = "varchar(32)")]
        public string UserName { get; set; }

        [Required, Column(TypeName = "varchar(128)"), EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required, Column(TypeName = "varchar(10)"), RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be a string of 10 digits.")]
        public string Phone { get; set; }

        [Required, Column(TypeName = "varchar(256)"), MinLength(8, ErrorMessage = "Password must be at least 8 characters long."), RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Password should only contain letters and numbers.")]
        public string Password { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]

        public RoleModel Role { get; set; }
        public ServiceProviderModel ServiceProvider { get; set; }
        public RechargePlansModel RechargePlans { get; set; }
        public ICollection<RechargeLogsModel> RechargeLogs { get; set; }
        public WalletModel Wallet { get; set; }
        public ICollection<ContactModel> Contact { get; set; }
        public ICollection<RechargeReportModel> RechargeReports { get; set; }

    }
}
