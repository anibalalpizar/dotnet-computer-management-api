namespace ComputerApi.Domain.Entities
{
    public class InstalledSoftware
    {
        public int ComputerId { get; set; }
        public int SoftwareId { get; set; }
        public DateTime InstallationDate { get; set; }

        public virtual Computer Computer { get; set; } = null!;
        public virtual Software Software { get; set; } = null!;
    }
}
