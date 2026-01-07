using AutoMapper;
using MiniRedditCloneApi.DTOs.Nerd;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Mapping
{
    public class NerdProfile : Profile
    {
        public NerdProfile()
        {
            CreateMap<NerdRegistrationDTO, Nerd>()
                .ForMember(nerd => nerd.UserName, opt => opt.MapFrom(dto => dto.Username))
                .ForMember(nerd => nerd.UploadedAvatar, opt => opt.Ignore())
                .ForMember(nerd => nerd.DefaultAvatarNum, opt => opt.Ignore());

            CreateMap<Nerd, NerdDTO>()
                .ForMember(dto => dto.Username, opts => opts.MapFrom(nerd => nerd.UserName))
                .ForMember(dto => dto.AvatarUrl, opts => opts.Ignore());

            CreateMap<NerdLoginDTO, Nerd>()
                .ForMember(nerd => nerd.UserName, opts => opts.MapFrom(dto => dto.Email));
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:04 PM
# Update Fri, Jan  9, 2026  9:25:45 PM
# Update Fri, Jan  9, 2026  9:34:30 PM
// Logic update: cY6gCJhyVy9g
// Logic update: 6FyQFrEVCxsa
// Logic update: O0nKSEhSSqbc
// Logic update: 6ouzc8jDFJ9X
// Logic update: 74DWRXuq0NMU
// Logic update: b05onRylKp0p
// Logic update: CzRaJBLhHToK
