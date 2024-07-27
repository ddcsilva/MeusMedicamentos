using AutoMapper;
using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.DTOs.Usuario;
using MeusMedicamentos.Domain.Entities;

namespace MeusMedicamentos.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<CriarCategoriaDTO, Categoria>();
            CreateMap<EditarCategoriaDTO, Categoria>();

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<CriarUsuarioDTO, Usuario>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()));
            CreateMap<EditarUsuarioDTO, Usuario>();
        }
    }
}
