using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Models.DataModels
{
    [Table("ServiceProvider")]
    public class ServiceProviderModel
    {
        [Key]
        public int ServiceProviderId { get; set; }

        [Required]
        public string ServiceName { get; set; }

        public ICollection<UsersModel> Users { get; set; }
        public ICollection<RechargePlansModel> RechargePlans { get; set; }
    }
}
