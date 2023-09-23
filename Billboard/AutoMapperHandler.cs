using AutoMapper;
using Billboard.Models;
using Billboard.Models.DTO;

namespace Billboard
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<Companies, CompaniesDto>().ReverseMap();
        }
    }
}
