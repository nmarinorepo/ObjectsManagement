using ObjectsManagement.Application.Repositories;
using ObjectsManagement.Persistence.Context;

namespace ObjectsManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ObjectsContext _context;
        private IObjectRepository _objectRepository;

        public UnitOfWork(ObjectsContext context)
        {
            _context = context;
        }

        public IObjectRepository ObjectRepository
        {
            get
            {
                if(_objectRepository == null)
                {
                    _objectRepository = new ObjectRepository(_context);
                }
                return _objectRepository;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync(true);
        }
    }
}
