using AutoMapper;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
namespace Infra.Mapper;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<Request, RequestDto>().ReverseMap();
        CreateMap<Servant, ServantDto>().ReverseMap();
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
        CreateMap<VehicleDetailDto, CreateVehicleRequest>().ReverseMap();
        CreateMap<VehicleDto, CreateVehicleRequest>().ReverseMap();
        CreateMap<ServantDto, CreateServantRequest>().ReverseMap();
        CreateMap<VehicleDetail, VehicleDetailDto>().ReverseMap();
        CreateMap<Member, MemberDto>().ReverseMap();
        CreateMap<AreaDepartmentDto, AreaDepartment>().ReverseMap();
        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Language, LanguageDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<RoleUserDto, RoleUser>().ReverseMap();
        CreateMap<ServantWorkDay, ServantWorkDayDto>().ReverseMap();
        CreateMap<Request, RequestDto>().ReverseMap();
        CreateMap<Member, MemberDto>().ReverseMap();
        CreateMap<AreaInfo, AreaInfoDto>().ReverseMap();
        CreateMap<ServantScoreDto, ServantScore>().ReverseMap();
        CreateMap<UsageDto, Usage>().ReverseMap();
        CreateMap<GenderTranslation, GenderTranslationDto>().ReverseMap();
        CreateMap<VehicleOwner, VehicleOwnerDto>().ReverseMap();
        CreateMap<DiscountCode, DiscountCodeDto>().ReverseMap();
        CreateMap<DiscountCodeUser, DiscountCodeUserDto>().ReverseMap();
        CreateMap<DiscountCodeDto,CreateDiscountCodeRequest>().ReverseMap();
        CreateMap<DiscountCodeDto,UpdateDiscountCodeRequest>().ReverseMap();
        CreateMap<ServantStatusDto, ServantDto>().ReverseMap();
    }

}
