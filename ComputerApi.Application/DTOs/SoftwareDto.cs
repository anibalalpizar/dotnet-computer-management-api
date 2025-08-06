namespace ComputerApi.Application.DTOs
{
    public class SoftwareDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public DateTime? InstallationDate { get; set; }
    }

    public class CreateSoftwareDto
    {
        public string Description { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
    }

    public class UpdateSoftwareDto
    {
        public string Description { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
    }
}
