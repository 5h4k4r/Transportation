using AutoMapper;
using Core.Models;
using Infra.Entities;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Request, RequestDTO>().ReverseMap();
        CreateMap<Servant, ServantDTO>().ReverseMap();
        CreateMap<Member, MemberDTO>().ReverseMap();
    }

}
