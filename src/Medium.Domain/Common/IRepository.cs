using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Domain.Common
{
    public interface IRepository<T>
    {
        Guid NextIdentifier();
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token);
        Task<T> GetByIdAsync(Guid Id, CancellationToken token);
        Task<bool> Exists(Guid Id, CancellationToken token);
        Task AddAsync(T Entity, CancellationToken token);
        Task RemoveAsync(T Entity, CancellationToken token);
        Task SaveAsync(CancellationToken token);
    }
}
