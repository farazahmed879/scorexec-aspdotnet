using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;

namespace ScoringAppReact.EntityFrameworkCore
{
    public static class ScoringAppReactDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ScoringAppReactDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ScoringAppReactDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
            //builder.UseLoggerFactory(new LoggerFactory(new[] { new ConsoleLoggerProvider());
            //builder.EnableDetailedErrors();
            //builder.EnableSensitiveDataLogging();
        }
    }
}
