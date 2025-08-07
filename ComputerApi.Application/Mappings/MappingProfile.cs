using AutoMapper;
using ComputerApi.Application.DTOs;
using ComputerApi.Domain.Entities;

namespace ComputerApi.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Computer mappings
            CreateMap<Computer, ComputerDto>()
                .ForMember(dest => dest.InstalledSoftwares, opt => opt.MapFrom(src =>
                    src.InstalledSoftwares.Select(ins => new SoftwareDto
                    {
                        Id = ins.Software.Id,
                        Description = ins.Software.Description,
                        Version = ins.Software.Version,
                        InstallationDate = ins.InstallationDate
                    })));

            CreateMap<CreateComputerDto, Computer>();
            CreateMap<UpdateComputerDto, Computer>();

            // Software mappings
            CreateMap<Software, SoftwareDto>();
            CreateMap<CreateSoftwareDto, Software>();
            CreateMap<UpdateSoftwareDto, Software>();
        }
    }
}
