using AutoMapper;
using MiniRedditCloneApi.DTOs.Insight;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Mapping
{
    public class InsightProfile : Profile
    {
        public InsightProfile()
        {
            CreateMap<Insight, InsightDTO>()
                .ForMember(dto => dto.IsNSFW, opt => opt.Ignore())
                .ForMember(dto => dto.MediaUrl, opt => opt.Ignore())
                .ForMember(dto => dto.Kudos, opt => opt.Ignore())
                .ForMember(dto => dto.Crits, opt => opt.Ignore())
                .ForMember(dto => dto.Notes, opt => opt.Ignore());
        }
    }
}# File update Fri, Jan  9, 2026  9:16:04 PM
# Update Fri, Jan  9, 2026  9:25:44 PM
# Update Fri, Jan  9, 2026  9:34:30 PM
// Logic update: YUvYc1zJuzET
// Logic update: tDtaLpF2MwQd
// Logic update: moBgYkWyvUBD
// Logic update: 4WERaJ7r4WEe
// Logic update: clhnHcSiHqXI
// Logic update: 7mYHReEBIGVB
// Logic update: kXnOREk2m6jJ
