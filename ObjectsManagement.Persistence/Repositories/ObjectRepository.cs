using Microsoft.EntityFrameworkCore;
using ObjectsManagement.Application.Repositories;
using ObjectsManagement.Domain.Entities;
using ObjectsManagement.Persistence.Context;

namespace ObjectsManagement.Persistence.Repositories
{
    public class ObjectRepository : BaseRepository<ObjectMainEntity>, IObjectRepository
    {
        public ObjectRepository(ObjectsContext context) : base(context)
        {
        }

        public Task<List<ObjectMainEntity>> GetAll()
        {
            return Context.Set<ObjectMainEntity>().Include(o => o.ObjectRelationships).ToListAsync();
        }

        public Task<List<ObjectRelationshipEntity>> GetAllRelationshipsWithParent()
        {
            return Context.Set<ObjectRelationshipEntity>().Include(o => o.ObjectMain).ToListAsync();
        }

        public Task<ObjectMainEntity> GetObjectById(int? id)
        {
            return Context.Set<ObjectMainEntity>().Include(p=>p.ObjectRelationships).FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async void ObjectUpdate(ObjectMainEntity objectMain)
        {
            Context.Update(objectMain);            
        }

        public async void ObjectRemove(ObjectMainEntity objectMain)
        {
            Context.Remove(objectMain);
        }

        public async void ObjectCreate(ObjectMainEntity objectMain)
        {
            Context.Add(objectMain);
        }

        public async void ObjectRelationshipUpdate(ObjectRelationshipEntity objectRelationship)
        {
            Context.Update(objectRelationship);
        }

        public async void ObjectRelationshipRemove(ObjectRelationshipEntity objectRelationship)
        {
            Context.Remove(objectRelationship);
        }

        public async void SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
