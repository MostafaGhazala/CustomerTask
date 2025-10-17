using AutoMapper;
using CustomerTask.Core.Entites;
using CustomerTask.Core.Dtos;

namespace CustomerTask.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.GenderName,
                       opt => opt.MapFrom(src => src.Gender.Name))
            .ForMember(dest => dest.FullLocation,
                       opt => opt.MapFrom(src => $"{src.Governorate.Name} - {src.District.Name} - {src.Village.Name}"));

            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.Governorate, opt => opt.Ignore()) 
                .ForMember(dest => dest.District, opt => opt.Ignore())
                .ForMember(dest => dest.Village, opt => opt.Ignore())
                .ForMember(dest => dest.Gender, opt => opt.Ignore());
        }
    }

}
