using AutoMapper;
using Core.Models;
using Infra.Entities;
namespace Infra.Mapper;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDTO, User>();
        CreateMap<User, UserDTO>();
        CreateMap<Request, RequestDTO>().ReverseMap();
        CreateMap<Servant, ServantDTO>().ReverseMap();
        CreateMap<Member, MemberDTO>().ReverseMap();
        CreateMap<AreaDepartmentDTO, AreaDepartment>().ReverseMap();
        CreateMap<Department, DepartmentDTO>().ReverseMap();
        CreateMap<Employee, EmployeeDTO>().ReverseMap();
        CreateMap<Language, LanguageDTO>().ReverseMap();
        CreateMap<Role, RoleDTO>().ReverseMap();
        CreateMap<RoleUserDTO, RoleUser>();
        CreateMap<RoleUser, RoleUserDTO>();
        CreateMap<ICollection<RoleUserDTO>, ICollection<RoleUser>>();
        CreateMap<ICollection<RoleUser>, ICollection<RoleUserDTO>>();
        CreateMap<ServantWorkDay, ServantWorkDayDTO>().ReverseMap();
        CreateMap<Request, RequestDTO>().ReverseMap();
        CreateMap<Member, MemberDTO>().ReverseMap();
        CreateMap<AreaInfo, AreaInfoDTO>().ReverseMap();



    }

}
