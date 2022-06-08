using AutoMapper;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Resources.EmployeeResources;
using BusinessLogicLayer.Resources.ProblemResources;
using BusinessLogicLayer.Resources.ReportResources;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveEmployeeResource, Employee>();
            CreateMap<SaveReportResource, Report>()
                .ForMember(src => src.EReportState, 
                    opt
                        => opt.MapFrom(src => src.EReportState));
            CreateMap<SaveProblemResource, Problem>();
                // .ForMember(
                //     src => src.EProblemState,
                //     opt
                //         => opt.MapFrom(src => src.EProblemState));
            CreateMap<CommentResource, Comment>();
        }
    }
}