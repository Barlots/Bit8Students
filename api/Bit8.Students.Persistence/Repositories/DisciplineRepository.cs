using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bit8.Students.Domain.Models;
using Dapper;

namespace Bit8.Students.Persistence.Repositories
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly IDbTransaction _transaction;
        
        public DisciplineRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task AddAsync(Discipline entity)
        {
            entity.Id = await _transaction.Connection.ExecuteScalarAsync<int>(
                "insert into discipline(name, professor_name) values(@Name, @ProfessorName); select LAST_INSERT_ID()",
                new {Name = entity.Name, ProfessorName = entity.ProfessorName},
                _transaction);
        }
        public async Task<IEnumerable<Discipline>> AllAsync()
        {
            return await _transaction.Connection.QueryAsync<Discipline>(
                "select * from discipline",
                _transaction);
        }

        public async Task DeleteAsync(Discipline entity)
        {
            await DeleteAsync(entity.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await _transaction.Connection.ExecuteAsync(
                "delete from discipline where id = @Id", 
                new {Id = id}
                );
        }

        public async Task<bool> HasScores(int id)
        {
            var sql = @"select 1 where exists (
                    select 1 from discipline d
                        join discipline_semester ds on d.id = ds.discipline_id
                        join student_score ss on ds.id = ss.discipline_semester_id
                    where d.id = @Id
                );";
            
            var result = await _transaction.Connection.QueryAsync(sql, new {Id = id});
            return result.Any();
        }
    }
}