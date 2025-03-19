using AutoMapper;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.Dtos;

namespace NZWalksAPI.Mappings
{
    public class AutoMappingsProfiles : Profile
    {
        public AutoMappingsProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();

            CreateMap<Walk, AddWalkRequestDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
        }
    }
}
