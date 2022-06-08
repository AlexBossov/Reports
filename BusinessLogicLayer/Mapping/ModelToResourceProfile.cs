using System.Linq;
using AutoMapper;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Resources.EmployeeResources;
using BusinessLogicLayer.Resources.ProblemResources;
using BusinessLogicLayer.Resources.ReportResources;
using BusinessLogicLayer.Resources.TokenResources;
using BusinessLogicLayer.Security.Tokens;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Employee, EmployeeResource>()
                .ForMember(u => u.Roles,
                    opt
                        => opt.MapFrom(u => u.EmployeeRoles.Select(ur => ur.Role.Name)));
            CreateMap<Employee, EmployeeTreeResource>()
                .ForMember(u => u.Roles, 
                    opt 
                        => opt.MapFrom(u => u.EmployeeRoles.Select(ur => ur.Role.Name)));
            CreateMap<Comment, CommentResource>();
            CreateMap<Problem, ProblemResource>()
                .ForMember(
                    src => src.EProblemState, 
                    opt
                        => opt.MapFrom(src => src.EProblemState));
            CreateMap<Report, ReportResource>();
            CreateMap<AccessToken, AccessTokenResource>()
                .ForMember(a => a.AccessToken, opt => opt.MapFrom(a => a.Token))
                .ForMember(a => a.RefreshToken, opt => opt.MapFrom(a => a.RefreshToken.Token))
                .ForMember(a => a.Expiration, opt => opt.MapFrom(a => a.Expiration)); 
        }
    }
}