using System;

namespace Bit8.Students.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}