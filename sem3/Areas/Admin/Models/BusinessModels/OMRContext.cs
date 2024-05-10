using Microsoft.EntityFrameworkCore;
using sem3.Areas.Admin.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Models.BusinessModels
{
    public class OMRContext : DbContext
    {
        public OMRContext()
        {
        }

        public OMRContext(DbContextOptions<OMRContext> options) : base(options)
        {

        }

        public DbSet<UsersModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<ServiceProviderModel> ServiceProviders { get; set; }
        public DbSet<RechargePlansModel> RechargePlans { get; set; }
        public DbSet<WalletModel> Wallets { get; set; }
        public DbSet<RechargeLogsModel> RechargeLogs { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<RechargeReportModel> RechargeReports { get; set; }

    }
}
