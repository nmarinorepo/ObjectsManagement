using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ObjectsManagement.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [Required]
        public string Type { get; set; }
    }
}
