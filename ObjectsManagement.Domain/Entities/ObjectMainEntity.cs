using ObjectsManagement.Domain.Common;

namespace ObjectsManagement.Domain.Entities
{
    public class ObjectMainEntity : BaseEntity
    {
        public ICollection<ObjectRelationshipEntity>? ObjectRelationships { get; set; }
    }
}
