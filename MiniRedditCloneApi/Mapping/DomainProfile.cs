using AutoMapper;
using MiniRedditCloneApi.DTOs.Domain;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Mapping
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Domain, DomainDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(domain => domain.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(domain => domain.Name));
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:03 PM
# Update Fri, Jan  9, 2026  9:25:43 PM
# Update Fri, Jan  9, 2026  9:34:29 PM
// Logic update: 0c7qP630WmKF
// Logic update: 495E9yHUNMxr
// Logic update: PXHWNHYW13TV
// Logic update: TsJTMQChqTLq
