using AutoMapper;
using FluentValidation;
using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Interfaces;
using MeusMedicamentos.Domain.Notifications;
using MeusMedicamentos.Domain.Validations;
using MeusMedicamentos.Shared;

namespace MeusMedicamentos.Application.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<Categoria> validator, INotificadorErros notificador)
            : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<CategoriaDTO>>> ObterTodosAsync()
        {
            var categorias = await _categoriaRepository.ObterTodosAsync();
            var categoriaDTOs = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
            return new ApiResponse<IEnumerable<CategoriaDTO>>(categoriaDTOs, 200);
        }

        public async Task<ApiResponse<CategoriaDTO>> ObterPorIdAsync(int id)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(id);
            if (categoria == null)
            {
                Notificar("Categoria não encontrada");
                return new ApiResponse<CategoriaDTO>("Categoria não encontrada", 404);
            }

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            return new ApiResponse<CategoriaDTO>(categoriaDTO, 200);
        }

        public async Task<ApiResponse<CategoriaDTO>> AdicionarAsync(CriarCategoriaDTO categoriaDTO)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            var existente = await _categoriaRepository.ObterPorCondicaoAsync(c => c.Nome == categoria.Nome);
            if (existente.Any())
            {
                Notificar("Já existe uma categoria com o mesmo nome.");
                return new ApiResponse<CategoriaDTO>("Já existe uma categoria com o mesmo nome.", 400);
            }

            var validationResult = await new CategoriaValidator().ValidateAsync(categoria);
            if (!validationResult.IsValid)
            {
                Notificar(validationResult);
                return new ApiResponse<CategoriaDTO>(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), 400);
            }

            await _categoriaRepository.AdicionarAsync(categoria);
            var sucesso = await _unitOfWork.SalvarAlteracoesAsync();
            if (!sucesso)
            {
                Notificar("Erro ao salvar categoria.");
                return new ApiResponse<CategoriaDTO>("Erro ao salvar categoria.", 500);
            }

            var categoriaDtoRetorno = _mapper.Map<CategoriaDTO>(categoria);
            return new ApiResponse<CategoriaDTO>(categoriaDtoRetorno, 201);
        }

        public async Task<ApiResponse<CategoriaDTO>> AtualizarAsync(EditarCategoriaDTO categoriaDTO)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(categoriaDTO.Id);
            if (categoria == null)
            {
                Notificar("Categoria não encontrada");
                return new ApiResponse<CategoriaDTO>("Categoria não encontrada", 404);
            }

            _mapper.Map(categoriaDTO, categoria);

            var validationResult = await new CategoriaValidator().ValidateAsync(categoria);
            if (!validationResult.IsValid)
            {
                Notificar(validationResult);
                return new ApiResponse<CategoriaDTO>(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), 400);
            }

            await _categoriaRepository.AtualizarAsync(categoria);
            var sucesso = await _unitOfWork.SalvarAlteracoesAsync();
            if (!sucesso)
            {
                Notificar("Erro ao atualizar categoria.");
                return new ApiResponse<CategoriaDTO>("Erro ao atualizar categoria.", 500);
            }

            var categoriaDtoRetorno = _mapper.Map<CategoriaDTO>(categoria);
            return new ApiResponse<CategoriaDTO>(categoriaDtoRetorno, 200);
        }

        public async Task<ApiResponse<bool>> RemoverAsync(int id)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(id);
            if (categoria == null)
            {
                Notificar("Categoria não encontrada");
                return new ApiResponse<bool>("Categoria não encontrada", 404);
            }

            await _categoriaRepository.RemoverAsync(categoria);
            var sucesso = await _unitOfWork.SalvarAlteracoesAsync();
            if (!sucesso)
            {
                Notificar("Erro ao remover categoria.");
                return new ApiResponse<bool>("Erro ao remover categoria.", 500);
            }

            return new ApiResponse<bool>(true, 204);
        }
    }
}
