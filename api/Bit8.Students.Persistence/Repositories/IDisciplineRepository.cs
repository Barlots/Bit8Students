using System.Collections.Generic;
using Bit8.Students.Domain.Models;

namespace Bit8.Students.Persistence
{
    public interface IDisciplineRepository
    {
        void Add(Discipline entity);
        IEnumerable<Discipline> All();
        void Delete(Discipline entity);
        void Delete(int id);
    }
}