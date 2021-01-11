using System;
using System.Data;
using Bit8.Students.Common;
using MySql.Data.MySqlClient;

namespace Bit8.Students.Query
{
    public class QueryBase : IDisposable
    {
        protected IDbConnection Connection { get; }
        
        protected QueryBase(IBConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}