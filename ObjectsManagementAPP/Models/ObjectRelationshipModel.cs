using System.ComponentModel.DataAnnotations;

namespace ObjectsManagementAPP.Models
{
    public class ObjectRelationshipModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

        public ObjectMainModel? ObjectMain { get; set; }

        public int? ObjectMainId { get; set; }
    }
}
