using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Models.DataModels
{
    [Table("Role")]
    public class RoleModel
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        [DefaultValue("getdate()")]
        public DateTime Created_Date { get; set; }

        public ICollection<UsersModel> Users { get; set; }
    }
}
