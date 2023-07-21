using ObjectsManagement.Domain.Common;

namespace ObjectsManagement.Domain.Entities
{
    public class ObjectRelationshipEntity : BaseEntity
    {
        public ObjectMainEntity? ObjectMain { get; set; }

        public int? ObjectMainId { get; set; }
    }
}
