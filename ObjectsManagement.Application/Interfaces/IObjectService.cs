using ObjectsManagement.Domain.Entities;

namespace ObjectsManagement.Application.Interfaces
{
    public interface IObjectService
    {
        Task<List<ObjectMainEntity>> GetAllObjects();
        Task<ObjectMainEntity> GetObjectById(int? id);

        Task<List<ObjectRelationshipEntity>> GetAllRelationshipsWithParent();

        void ObjectUpdate(ObjectMainEntity objectMain);

        bool ObjectMainExists(int id);

        bool ObjectRelationshipExists(int id);

        void ObjectRemove(ObjectMainEntity objectMain);
        void ObjectCreate(ObjectMainEntity objectMain);

        void ObjectRelationshipUpdate(ObjectRelationshipEntity objectRelationship);

        void ObjectRelationshipRemove(ObjectRelationshipEntity objectRelationship);

        Task SaveChangesAsync();
    }
}
