using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bit8.Students.Domain.Models;
using Dapper;

namespace Bit8.Students.Persistence.Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly IDbTransaction _transaction;
        
        public SemesterRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task AddAsync(Semester entity)
        {
            entity.Id = await _transaction.Connection.ExecuteScalarAsync<int>(
                "insert into semester(name) values(@Name); select LAST_INSERT_ID()",
                new {Name = entity.Name},
                _transaction);
        }
        public async Task<IEnumerable<Semester>> AllAsync()
        {
            return await _transaction.Connection.QueryAsync<Semester>(
                "select * from semester",
                _transaction);
        }

        public async Task DeleteAsync(Semester entity)
        {
            await DeleteAsync(entity.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await _transaction.Connection.ExecuteAsync(
                "delete from semester where id = @Id", 
                new {Id = id}
            );
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var sql = @"select count(1) from semester where Id=@Id";
            return await _transaction.Connection.ExecuteScalarAsync<bool>(sql, new { Id = id});
        }

        public async Task<bool> HasStudentsAsync(int id)
        {
            var sql = @"select 1 where exists (
                    select 1 from semester s
                        join discipline_semester ds on s.id = ds.student_id
                        join student_assignment sa on ds.id = sa.discipline_semester_id
                    where d.id = @Id
                );";
            
            var result = await _transaction.Connection.QueryAsync(sql, new {Id = id});
            return result.Any();
        }
    }
}