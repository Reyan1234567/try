using AutoMapper;
using MiniRedditCloneApi.DTOs.Response;

namespace MiniRedditCloneApi.Mapping
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<string?, MessageDTO>()
                .ForMember(dto => dto.Message, opt => opt.MapFrom(message => message));
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:04 PM
# Update Fri, Jan  9, 2026  9:25:45 PM
# Update Fri, Jan  9, 2026  9:34:31 PM
// Logic update: Do1oGwvs2a1S
// Logic update: QkzKB5N93k01
// Logic update: BYNNJvs76zzD
// Logic update: kXCENPvYw3Sk
// Logic update: cJi5OAviWgGA
// Logic update: HWthUDoDlZ22
