using ObjectsManagement.Domain.Entities;

namespace ObjectsManagement.Application.Repositories
{
    public interface IObjectRepository : IBaseRepository<ObjectMainEntity>
    {
        Task<List<ObjectMainEntity>> GetAll();

        Task<List<ObjectRelationshipEntity>> GetAllRelationshipsWithParent();

        Task<ObjectMainEntity> GetObjectById(int? id);

        void ObjectUpdate(ObjectMainEntity objectMain);

        void ObjectRemove(ObjectMainEntity objectMain);

        void ObjectCreate(ObjectMainEntity objectMain);

        void ObjectRelationshipUpdate(ObjectRelationshipEntity objectRelationship);

        void ObjectRelationshipRemove(ObjectRelationshipEntity objectRelationship);

        void SaveChangesAsync();
    }
}
