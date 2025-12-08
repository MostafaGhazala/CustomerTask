using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerTask.Core.Entites;

namespace CustomerTask.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Governorate> Governorates { get; }
        IGenericRepository<District> Districts { get; }
        IGenericRepository<Village> Villages { get; }
        IGenericRepository<Gender> Genders { get; }
        IGenericRepository<Numbers> Numbers  { get; }
        IGenericRepository<ReservedNumbers> ReservedNumbers { get; }

        Task<int> SaveAsync();
    }
}
