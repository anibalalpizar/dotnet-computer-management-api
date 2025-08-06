using System.ComponentModel.DataAnnotations;

namespace ComputerApi.Domain.Entities
{
    public class Software
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Version { get; set; } = string.Empty;

        public virtual ICollection<InstalledSoftware> InstalledSoftwares { get; set; } = new List<InstalledSoftware>();
    }
}
