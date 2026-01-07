using AutoMapper;
using MiniRedditCloneApi.DTOs.Herd;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Mapping
{
    public class HerdProfile : Profile
    {
        public HerdProfile()
        {
            CreateMap<Herd, HerdDTO>()
                .ForMember(dto => dto.ImageUrl, opt => opt.Ignore());

            CreateMap<string, HerdRulesDTO>()
                .ForMember(dto => dto.Rules, opt => opt.MapFrom(rules => rules));
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:04 PM
# Update Fri, Jan  9, 2026  9:25:44 PM
# Update Fri, Jan  9, 2026  9:34:30 PM
// Logic update: hxdBvF1rzaur
// Logic update: NT4aLtpNMeEk
// Logic update: 3GI25omstROA
// Logic update: 9lqazucqciql
