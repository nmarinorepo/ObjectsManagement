using System.ComponentModel.DataAnnotations;

namespace ObjectsManagementAPP.Models
{
    public class ObjectMainModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

        public ICollection<ObjectRelationshipModel>? ObjectRelationships { get; set; }
    }
}
