using SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Interfaces
{
    public interface IGenericRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : BaseEntity
    {
    }
}
