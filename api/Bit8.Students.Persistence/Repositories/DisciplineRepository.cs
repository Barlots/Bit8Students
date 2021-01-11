using System.Collections.Generic;
using System.Data;
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

        public void Add(Discipline entity)
        {
            entity.Id = _transaction.Connection.ExecuteScalar<int>(
                "insert into discipline(name, professor_name) values(@Name, @ProfessorName);",
                new {Name = entity.Name, ProfessorName = entity.ProfessorName},
                _transaction);
        }

        public IEnumerable<Discipline> All()
        {
            return _transaction.Connection.Query<Discipline>(
                "select * from discipline",
                _transaction);
        }

        public void Delete(Discipline entity)
        {
            Delete(entity.Id);
        }

        public void Delete(int id)
        {
            _transaction.Connection.Execute(
                "delete from discipline where id = @Id", 
                new {Id = id}
                );
        }
    }
}