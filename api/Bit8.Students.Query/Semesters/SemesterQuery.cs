using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bit8.Students.Common;
using Dapper;

namespace Bit8.Students.Query.Semesters
{
    public class SemesterQuery : QueryBase, ISemesterQuery
    {
        protected SemesterQuery(IBConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<GetAllWithDisciplinesQuery>> GetAllWithDisciplinesAsync()
        {
            var sql = @"select s.name semester, d.name discipline from semester s
                            join discipline_semester ds on s.id = ds.semester_id
                            join discipline d on ds.discipline_id = d.id";

            var result = await Connection.QueryAsync(sql,
                (string semester, string discipline) => new {semester, discipline}, 
                splitOn: "semester");

            return result.GroupBy(x => x.semester,
                (key, group) => new GetAllWithDisciplinesQuery
                {
                    SemesterName = key, 
                    DisciplineNames = group.Select(x => x.discipline)
                });
        }
    }
}