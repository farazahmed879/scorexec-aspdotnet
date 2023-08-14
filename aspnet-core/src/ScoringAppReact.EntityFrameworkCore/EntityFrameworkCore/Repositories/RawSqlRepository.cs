using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Abp.Data;
using Abp.EntityFrameworkCore;
using Abp.Organizations;
using Microsoft.EntityFrameworkCore;
using ScoringAppReact.EntityFrameworkCore;
using ScoringAppReact.EntityFrameworkCore.Repositories;

namespace Rhithm.EntityFrameworkCore.Repositories
{
    public interface IRawSqlRepository
    {
        DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters);
        void EnsureConnectionOpen();
    }


    public class RawSqlRepository : ScoringAppReactRepositoryBase<OrganizationUnit, long>, IRawSqlRepository
    {
        private readonly IActiveTransactionProvider _transactionProvider;

        private readonly DbContext _context; // Replace with the actual DbContext type

       

        public RawSqlRepository(IDbContextProvider<ScoringAppReactDbContext> dbContextProvider, DbContext context,
            IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider)
        {
            _transactionProvider = transactionProvider;
            _context = context;
        }

        public DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = _context.Database.GetDbConnection().CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = GetActiveTransaction();

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }
        public void EnsureConnectionOpen()
        {
            var connection = _context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }
        private DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs
            {
                {"ContextType", typeof(ScoringAppReactDbContext) },
                {"MultiTenancySide", MultiTenancySide }
            });
        }
    }
}
