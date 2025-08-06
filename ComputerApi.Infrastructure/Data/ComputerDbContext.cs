using ComputerApi.Domain.Entities;
using ComputerApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ComputerApi.Infrastructure.Data
{
    public class ComputerDbContext : DbContext
    {
        public ComputerDbContext(DbContextOptions<ComputerDbContext> options) : base(options)
        {
        }

        public DbSet<Computer> Computers { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<InstalledSoftware> InstalledSoftware { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Computer configuration
            modelBuilder.Entity<Computer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Brand).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Type).HasConversion<int>();
            });

            // Software configuration
            modelBuilder.Entity<Software>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Version).IsRequired().HasMaxLength(50);
            });

            // InstalledSoftware configuration (Many-to-Many)
            modelBuilder.Entity<InstalledSoftware>(entity =>
            {
                entity.HasKey(e => new { e.ComputerId, e.SoftwareId });

                entity.HasOne(e => e.Computer)
                      .WithMany(c => c.InstalledSoftwares)
                      .HasForeignKey(e => e.ComputerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Software)
                      .WithMany(s => s.InstalledSoftwares)
                      .HasForeignKey(e => e.SoftwareId)
                      .OnDelete(DeleteBehavior.Cascade);

                // CORREGIDO: Quitar HasDefaultValueSql para InMemory
                entity.Property(e => e.InstallationDate);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Computers
            modelBuilder.Entity<Computer>().HasData(
                new Computer { Id = 1, Brand = "Dell", Type = ComputerType.Laptop, ManufacturingYear = 2022 },
                new Computer { Id = 2, Brand = "HP", Type = ComputerType.Desktop, ManufacturingYear = 2021 },
                new Computer { Id = 3, Brand = "Lenovo", Type = ComputerType.Laptop, ManufacturingYear = 2023 }
            );

            // Seed Software
            modelBuilder.Entity<Software>().HasData(
                new Software { Id = 1, Description = "Microsoft Office", Version = "2021" },
                new Software { Id = 2, Description = "Mozilla Firefox", Version = "108.0" },
                new Software { Id = 3, Description = "Google Chrome", Version = "108.0.5359" },
                new Software { Id = 4, Description = "Adobe Acrobat Reader", Version = "2022.003.20282" },
                new Software { Id = 5, Description = "Visual Studio Code", Version = "1.74.0" },
                new Software { Id = 6, Description = "Notepad++", Version = "8.4.7" }
            );

            // Seed InstalledSoftware - CORREGIDO: Usar fechas fijas
            modelBuilder.Entity<InstalledSoftware>().HasData(
                new InstalledSoftware { ComputerId = 1, SoftwareId = 1, InstallationDate = new DateTime(2024, 1, 15) },
                new InstalledSoftware { ComputerId = 1, SoftwareId = 2, InstallationDate = new DateTime(2024, 1, 20) },
                new InstalledSoftware { ComputerId = 2, SoftwareId = 3, InstallationDate = new DateTime(2024, 2, 1) },
                new InstalledSoftware { ComputerId = 2, SoftwareId = 4, InstallationDate = new DateTime(2024, 2, 10) }
            );
        }
    }
}