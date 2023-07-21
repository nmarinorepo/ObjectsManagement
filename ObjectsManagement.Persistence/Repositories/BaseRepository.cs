using ObjectsManagement.Application.Repositories;
using ObjectsManagement.Domain.Common;
using ObjectsManagement.Persistence.Context;

namespace ObjectsManagement.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ObjectsContext Context;

        public BaseRepository(ObjectsContext context)
        {
            Context = context;
        }

        public void Create(T entity)
        {
            Context.Add(entity);
        }

        public void Update(T entity)
        {
            Context.Update(entity);
        }

        public void Delete(T entity)
        {
            //entity.DateCreated = DateTimeOffset.UtcNow;
            Context.Update(entity);
        }

        public Task<T> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public Task<T> Get(Guid id, CancellationToken cancellationToken)
        //{
        //    return Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        //}


    }
}
