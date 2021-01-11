using System;
using Bit8.Students.Persistence.Repositories;

namespace Bit8.Students.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IDisciplineRepository DisciplineRepository { get; }
        
        void Commit();
    }
}