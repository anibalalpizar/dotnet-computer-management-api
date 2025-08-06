using ComputerApi.Domain.Enums;

namespace ComputerApi.Application.DTOs
{
    public class ComputerDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public ComputerType Type { get; set; }
        public int ManufacturingYear { get; set; }
        public List<SoftwareDto> InstalledSoftwares { get; set; } = new();
    }

    public class CreateComputerDto
    {
        public string Brand { get; set; } = string.Empty;
        public ComputerType Type { get; set; }
        public int ManufacturingYear { get; set; }
    }

    public class UpdateComputerDto
    {
        public string Brand { get; set; } = string.Empty;
        public ComputerType Type { get; set; }
        public int ManufacturingYear { get; set; }
    }
}
