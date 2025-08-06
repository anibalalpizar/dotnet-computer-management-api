using ComputerApi.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ComputerApi.Domain.Entities
{
    public class Computer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Brand { get; set; } = string.Empty;

        public ComputerType Type { get; set; }

        [Range(1990, 2030)]
        public int ManufacturingYear { get; set; }

        public virtual ICollection<InstalledSoftware> InstalledSoftwares { get; set; } = new List<InstalledSoftware>();
    }
}
