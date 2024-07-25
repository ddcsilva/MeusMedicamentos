using AutoMapper;
using MeusMedicamentos.Application.DTOs;
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
        }
    }
}
