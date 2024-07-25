using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MeusMedicamentos.Shared;
using MeusMedicamentos.Application.DTOs;

namespace MeusMedicamentos.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Categoria> _validator;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper, IValidator<Categoria> validator)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<IEnumerable<CategoriaDTO>>> ObterTodosAsync()
        {
            var categorias = await _categoriaRepository.ObterTodosAsync();
            var categoriaDTOs = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
            return new ApiResponse<IEnumerable<CategoriaDTO>>(categoriaDTOs);
        }

        public async Task<ApiResponse<CategoriaDTO>> ObterPorIdAsync(int id)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(id);
            if (categoria == null)
            {
                return new ApiResponse<CategoriaDTO>("Categoria não encontrada");
            }

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            return new ApiResponse<CategoriaDTO>(categoriaDTO);
        }

        public async Task<ApiResponse<CategoriaDTO>> AdicionarAsync(CriarCategoriaDTO categoriaDTO)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            var existente = await _categoriaRepository.ObterPorCondicaoAsync(c => c.Nome == categoria.Nome);
            if (existente.Any())
            {
                return new ApiResponse<CategoriaDTO>("Já existe uma categoria com o mesmo nome.");
            }

            var validationResult = await _validator.ValidateAsync(categoria);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiResponse<CategoriaDTO>(errors);
            }

            await _categoriaRepository.AdicionarAsync(categoria);
            await _categoriaRepository.SalvarAlteracoesAsync();
            var categoriaDtoRetorno = _mapper.Map<CategoriaDTO>(categoria);
            return new ApiResponse<CategoriaDTO>(categoriaDtoRetorno);
        }

        public async Task<ApiResponse<string>> AtualizarAsync(EditarCategoriaDTO categoriaDTO)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(categoriaDTO.Id);
            if (categoria == null)
            {
                return new ApiResponse<string>("Categoria não encontrada");
            }

            _mapper.Map(categoriaDTO, categoria);

            var validationResult = await _validator.ValidateAsync(categoria);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiResponse<string>(errors);
            }

            await _categoriaRepository.AtualizarAsync(categoria);
            await _categoriaRepository.SalvarAlteracoesAsync();
            return new ApiResponse<string>("Categoria atualizada com sucesso");
        }

        public async Task<ApiResponse<string>> RemoverAsync(int id)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(id);
            if (categoria == null)
            {
                return new ApiResponse<string>("Categoria não encontrada");
            }

            await _categoriaRepository.RemoverAsync(categoria);
            await _categoriaRepository.SalvarAlteracoesAsync();
            return new ApiResponse<string>("Categoria removida com sucesso");
        }
    }
}
