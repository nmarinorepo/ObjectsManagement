using ObjectsManagement.Domain.Entities;

namespace ObjectsManagementAPP.Models
{
    public class ObjectViewModel
    {
        public string ObjectMainId { get; set; }
        public ObjectRelationshipModel objectRelationship { get; set; }
    }
}
