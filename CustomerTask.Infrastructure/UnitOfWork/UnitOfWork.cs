using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerTask.Core.Entites;
using CustomerTask.Core.Interfaces;
using CustomerTask.Infrastructure.Data;
using CustomerTask.Infrastructure.Repositories;

namespace CustomerTask.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Customer> Customers { get; }
        public IGenericRepository<Governorate> Governorates { get; }
        public IGenericRepository<District> Districts { get; }
        public IGenericRepository<Village> Villages { get; }
        public IGenericRepository<Gender> Genders { get; }
        public IGenericRepository<Numbers> Numbers{ get; }
        public IGenericRepository<ReservedNumbers> ReservedNumbers { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Customers = new GenericRepository<Customer>(_context);
            Governorates = new GenericRepository<Governorate>(_context);
            Districts = new GenericRepository<District>(_context);
            Villages = new GenericRepository<Village>(_context);
            Genders = new GenericRepository<Gender>(_context);
            Numbers = new GenericRepository<Numbers>(_context);
            ReservedNumbers = new GenericRepository<ReservedNumbers>(_context);
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
