using AutoMapper;
using Core.Models.Base;
using Core.Models.Requests;
using Core.Models.Responses;
using Infra.Entities;
using Task = Infra.Entities.Task;


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
        CreateMap<VehicleDto, UpdateVehicleRequest>().ReverseMap();
        CreateMap<Service, ServiceDto>().ReverseMap();
        CreateMap<Service, ListServicesResponses>().ReverseMap();
        CreateMap<ServiceAreaType, ServiceAreaTypeDto>().ReverseMap();
        CreateMap<ServiceAreaType, ServiceAreaTypeDtoResponse>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CategoryTranslation, CategoryTranslationDto>().ReverseMap();
        CreateMap<VehicleDto, VehicleDtoResponse>().ReverseMap();
        CreateMap<VehicleDetailDto, VehicleDetailDtoResponse>().ReverseMap();
        CreateMap<ServiceSubscriberDto, ServiceSubscriber>().ReverseMap();
        CreateMap<VehicleDetailDto, UpdateVehicleDetailRequest>().ReverseMap();
        CreateMap<ServantDto, CreateServantRequest>().ReverseMap();
        CreateMap<VehicleDetail, VehicleDetailDto>().ReverseMap();
        CreateMap<Member, MemberDto>().ReverseMap();
        CreateMap<MemberPaymentType, MemberPaymentTypeDto>().ReverseMap();
        CreateMap<AreaDepartmentDto, AreaDepartment>().ReverseMap();
        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Language, LanguageDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<RoleUserDto, RoleUser>().ReverseMap();
        CreateMap<ServantWorkDay, ServantWorkDayDto>().ReverseMap();
        CreateMap<Request, RequestDto>().ReverseMap();
        CreateMap<Rounder, RounderDto>().ReverseMap();
        CreateMap<Member, MemberDto>().ReverseMap();
        CreateMap<AreaInfo, AreaInfoDto>().ReverseMap();
        CreateMap<ServantScoreDto, ServantScore>().ReverseMap();
        CreateMap<UsageDto, Usage>().ReverseMap();
        CreateMap<GenderTranslation, GenderTranslationDto>().ReverseMap();
        CreateMap<VehicleOwner, VehicleOwnerDto>().ReverseMap();
        CreateMap<Task, TaskDto>().ReverseMap();
        CreateMap<Destination, DestinationDto>().ReverseMap();
    }

}
