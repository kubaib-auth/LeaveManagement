using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ManagementSystem.Authorization.Roles;
using ManagementSystem.Authorization.Users;
using ManagementSystem.MultiTenancy;
using ManagementSystem.Leaves;

namespace ManagementSystem.EntityFrameworkCore
{
    public class ManagementSystemDbContext : AbpZeroDbContext<Tenant, Role, User, ManagementSystemDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ManagementSystemDbContext(DbContextOptions<ManagementSystemDbContext> options)
            : base(options)
        {

        }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveCategory> LeaveCategorys { get; set; }
        public DbSet<LeaveQuota> LeaveQuotas { get; set; }

        
    }
}
