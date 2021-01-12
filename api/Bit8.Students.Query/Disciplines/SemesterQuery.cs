using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Common;
using Bit8.Students.Services.Disciplines;
using Dapper;

namespace Bit8.Students.Query.Semesters
{
    public class DisciplineQuery : QueryBase, IDisciplineQuery
    {
        protected DisciplineQuery(IBConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<GetAllQuery>> GetAllAsync()
        {
            var sql = @"select * from discipline";
            var result = await Connection.QueryAsync<GetAllQuery>(sql);

            return result;
        }
    }
}