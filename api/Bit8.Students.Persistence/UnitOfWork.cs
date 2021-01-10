using System.Data;
using System.Data.SqlClient;
using Bit8.Students.Common;

namespace Bit8.Students.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork(IBConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        
        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }
        }
    }
}