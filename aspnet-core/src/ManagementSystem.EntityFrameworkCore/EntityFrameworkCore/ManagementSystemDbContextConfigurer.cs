using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.EntityFrameworkCore
{
    public static class ManagementSystemDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ManagementSystemDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ManagementSystemDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
