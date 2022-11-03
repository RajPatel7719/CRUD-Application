using AutoMapper;
using CRUD_Application.Models.ModelsDTO;
using CRUD_Application.Models;

namespace CRUD_Application.Profiles
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<Register, RegisterDTO>().ReverseMap();
        }
    }
}
