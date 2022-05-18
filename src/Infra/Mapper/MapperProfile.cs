using AutoMapper;
using Core.Models;
using Infra.Entities;
namespace Infra.Mapper;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<Request, RequestDTO>().ReverseMap();
        CreateMap<Servant, ServantDTO>().ReverseMap();
        CreateMap<Vehicle, VehicleDTO>().ReverseMap();
        CreateMap<VehicleDetail, VehicleDetailDTO>().ReverseMap();
        CreateMap<Member, MemberDTO>().ReverseMap();
        CreateMap<AreaDepartmentDTO, AreaDepartment>().ReverseMap();
        CreateMap<Department, DepartmentDTO>().ReverseMap();
        CreateMap<Employee, EmployeeDTO>().ReverseMap();
        CreateMap<Language, LanguageDTO>().ReverseMap();
        CreateMap<Role, RoleDTO>().ReverseMap();
        CreateMap<RoleUserDTO, RoleUser>().ReverseMap();
        CreateMap<ServantWorkDay, ServantWorkDayDTO>().ReverseMap();
        CreateMap<Request, RequestDTO>().ReverseMap();
        CreateMap<Member, MemberDTO>().ReverseMap();
        CreateMap<AreaInfo, AreaInfoDTO>().ReverseMap();
        CreateMap<ServantScoreDTO, ServantScore>().ReverseMap();
        CreateMap<UsageDTO, Usage>().ReverseMap();
        CreateMap<GenderTranslation, GenderTranslationDTO>().ReverseMap();


    }

}
